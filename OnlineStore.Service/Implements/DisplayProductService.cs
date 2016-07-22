using LinqKit;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Model.Repository;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Interfaces;
using OnlineStore.Service.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Model.Mapper;

namespace OnlineStore.Service.Implements
{
    public class DisplayProductService : IDisplayProductService
    {
        #region Properties

        private static OnlineStoreMVCEntities context = new OnlineStoreMVCEntities();
        private ProductRepository db = new ProductRepository(context);
        private CategoryRepository categoryRepository = new CategoryRepository(context);

        #endregion

        #region Functions

        /// <summary>
        /// Get list product with selected category, seach string, order, filter after price range, filter after brands, paging
        /// </summary>
        /// <param name="request">conditions for filter</param>
        /// <returns>list product matching with conditions</returns>
        private IEnumerable<ecom_Products> GetAllProductsMatchingQueryAndSort(GetProductsByCategoryRequest request)
        {
            var searchQuery = PredicateBuilder.True<ecom_Products>();

            if (request.BrandIds.Count() > 0)
            {
                searchQuery = searchQuery.And(p => request.BrandIds.Contains((int)p.BrandId));
            }
            if (request.BeginPrice != null)
            {
                searchQuery = searchQuery.And(p => p.Price >= request.BeginPrice);
            }
            if (request.EndPrice != null)
            {
                searchQuery = searchQuery.And(p => p.Price <= request.EndPrice);
            }
            if (request.SearchString != null && request.SearchString != string.Empty)
            {
                searchQuery = searchQuery.And(p => p.Name.Contains(request.SearchString));
            }
            searchQuery = searchQuery.And(p => p.ecom_Categories.Select(c => c.Id).Contains(request.CategoryId));

            IEnumerable<ecom_Products> productsMatchingRefinement = db.Get(
                filter: searchQuery, includeProperties: "ecom_Brands,ecom_Categories,share_Images");
            switch (request.SortBy)
            {
                case ProductsSortBy.PriceLowToHigh:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderBy(p => p.Price);
                    break;
                case ProductsSortBy.PriceHighToLow:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderByDescending(p => p.Price);
                    break;
                case ProductsSortBy.ProductNameAToZ:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderBy(p => p.Name);
                    break;
                case ProductsSortBy.ProductNameZToA:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderByDescending(p => p.Name);
                    break;
            }

            return productsMatchingRefinement.ToList();

        }

        private IEnumerable<ecom_Products> GetAllProductsMatchingSearchString(SearchProductRequest request, SearchType searchType = SearchType.SearchString)
        {
            var searchQuery = PredicateBuilder.True<ecom_Products>();

            if (request.SearchString != null && request.SearchString != string.Empty && searchType != SearchType.AllProduct)
            {
                searchQuery = searchQuery.And(p => p.Name.Contains(request.SearchString));
            }

            if (searchType == SearchType.NewProducts)
            {
                searchQuery = searchQuery.And(p => p.IsNewProduct == true);
            }

            if (searchType == SearchType.BestSellProducts)
            {
                searchQuery = searchQuery.And(p => p.IsBestSellProduct == true);
            }

            IEnumerable<ecom_Products> productsMatchingRefinement = db.Get(
                filter: searchQuery, includeProperties: "ecom_Brands,ecom_Categories");
            switch (request.SortBy)
            {
                case ProductsSortBy.PriceLowToHigh:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderBy(p => p.Price);
                    break;
                case ProductsSortBy.PriceHighToLow:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderByDescending(p => p.Price);
                    break;
                case ProductsSortBy.ProductNameAToZ:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderBy(p => p.Name);
                    break;
                case ProductsSortBy.ProductNameZToA:
                    productsMatchingRefinement = productsMatchingRefinement
                    .OrderByDescending(p => p.Name);
                    break;
            }

            return productsMatchingRefinement.ToList();
        }

        /// <summary>
        /// Crop list product result to satisfy current page
        /// </summary>
        /// <param name="productsFound"></param>
        /// <param name="pageIndex"></param>
        /// <param name="numberOfResultsPerPage"></param>
        /// <returns></returns>
        private IEnumerable<ecom_Products> CropProductListToSatisfyGivenIndex(IEnumerable<ecom_Products> productsFound, int pageIndex, int numberOfResultsPerPage)
        {
            if (pageIndex > 1)
            {
                int numToSkip = (pageIndex - 1) * numberOfResultsPerPage;
                return productsFound.Skip(numToSkip)
                .Take(numberOfResultsPerPage).ToList();
            }
            else
            {
                return productsFound.Take(numberOfResultsPerPage).ToList();
            }
        }

        /// <summary>
        /// Get list product after category
        /// </summary>
        /// <param name="request">condition for filter</param>
        /// <returns>matched condition products</returns>
        public GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request)
        {
            string categoryName = categoryRepository.GetByID(request.CategoryId).Name;
            IEnumerable<ecom_Products> foundProducts = GetAllProductsMatchingQueryAndSort(request);

            GetProductsByCategoryResponse reponse = new GetProductsByCategoryResponse(){
                SelectedCategoryName = categoryName,
                SelectedCategory = request.CategoryId,
                BeginPrice = request.BeginPrice,
                EndPrice = request.EndPrice,
                NumberOfTitlesFound = foundProducts.Count(),
                TotalNumberOfPages = (int)Math.Ceiling((double)foundProducts.Count() / request.NumberOfResultsPerPage),
                CurrentPage = request.Index,
                SearchString = request.SearchString,
                SortBy = (int)request.SortBy,
                BrandIds = request.BrandIds,
                Products = CropProductListToSatisfyGivenIndex(foundProducts, request.Index, request.NumberOfResultsPerPage).ConvertToProductSummaryViews(),
                Brands = foundProducts.Select(p => p.ecom_Brands).Where(b => b!=null).Distinct().ToList().ConvertToBrandSummaryViews()// return list Brand exist in group product belong to selected category
            };

            return reponse;
        }

        /// <summary>
        /// Get details product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDetailsView GetProductDetails(int id)
        {
            ecom_Products product = db.GetProductById(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                ProductDetailsView productViewModel = new ProductDetailsView()
                {
                    Id = product.Id,
                    ProductCode = product.ProductCode,
                    Name = product.Name,
                    Price = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", product.Price),
                    BrandName = product.ecom_Brands!=null? product.ecom_Brands.Name:"",
                    CoverImageUrl = product.CoverImage!=null?product.CoverImage.ImagePath:"/Content/Images/no-image.png",
                    Description = product.Description,
                    Description2 = product.Description2,
                    Tags = product.Tags,
                    IsNewProduct = product.IsNewProduct,
                    IsBestSellProduct = product.IsBestSellProduct,
                    share_Images = product.share_Images.Select(i => i.ImagePath).ToArray()
                };

                return productViewModel;
            }
        }

        /// <summary>
        /// Get list new product in system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductSummaryView> GetAllNewProduct()
        {
            IEnumerable<ecom_Products> products = db.Get(filter: p => p.IsNewProduct == true && p.Status == (int)Define.Status.Active).Take(10);

            return products.ConvertToProductSummaryViews();
        }

        /// <summary>
        /// Get list best sell product
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductSummaryView> GetAllBestSellProduct()
        {
            IEnumerable<ecom_Products> products = db.Get(filter: p => p.IsBestSellProduct == true && p.Status == (int)Define.Status.Active).Take(10);

            return products.ConvertToProductSummaryViews();
        }

        /// <summary>
        /// Get list product have high priority order
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductSummaryView> GetListHighPriorityOrderProduct()
        {
            IEnumerable<ecom_Products> products = db.Get(filter: p => p.Status == (int)Define.Status.Active, orderBy:p => p.OrderBy(x =>x.SortOrder) ).Take(10);

            return products.ConvertToProductSummaryViews();
        }

        /// <summary>
        /// Get list summary category, which have status is Active or Deactive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SummaryCategoryViewModel> GetTopCategories()
        {
            return categoryRepository.GetAllCategoriesWithoutDelete().ConvertToIndexCategoryViews();
        }

        /// <summary>
        /// Search product by name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SearchProductResponse SearchByProductName(SearchProductRequest request, SearchType searchType)
        {
            IEnumerable<ecom_Products> foundProducts = GetAllProductsMatchingSearchString(request, searchType);
            SearchProductResponse response = new SearchProductResponse()
            {
                CategoryIds = request.CategoryIds,
                NumberOfTitlesFound = foundProducts.Count(),
                TotalNumberOfPages = (int)Math.Ceiling((double)foundProducts.Count() / request.NumberOfResultsPerPage),
                CurrentPage = request.Index,
                SearchString = request.SearchString,
                SortBy = (int)request.SortBy,
                BrandIds = request.BrandIds,
                Products = CropProductListToSatisfyGivenIndex(foundProducts, request.Index, request.NumberOfResultsPerPage).ConvertToProductSummaryViews(),
                Brands = foundProducts.Select(p => p.ecom_Brands).Where(b => b != null).Distinct().ToList().ConvertToBrandSummaryViews()// return list Brand exist in group product belong to selected category
            };
            return response;
        }

        #endregion
    }
}

