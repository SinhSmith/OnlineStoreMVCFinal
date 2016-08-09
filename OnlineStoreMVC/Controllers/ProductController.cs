using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Service.Interfaces;
using OnlineStore.Service.Implements;
using OnlineStore.Service.Messaging;
using OnlineStore.Infractructure.Utility;
using System.Net;
using OnlineStore.Model.ViewModel;
using OnlineStore.Infractructure.Helper;

namespace OnlineStoreMVC.Controllers
{
    public class ProductController : BaseController
    {
        #region properties

        //public IDisplayProductService service = new DisplayProductService();
        private static int productPerPage = 10;

        #endregion

        #region private functions

        /// <summary>
        /// Genarate initial Request object to get list product after category
        /// </summary>
        /// <param name="categoryId">id of selected category</param>
        /// <returns></returns>
        private GetProductsByCategoryRequest CreateInitialProductSearchRequest(int categoryId)
        {
            GetProductsByCategoryRequest request = new GetProductsByCategoryRequest()
            {
                CategoryId = categoryId,
                Index = 1,
                NumberOfResultsPerPage = productPerPage,
                SortBy = ProductsSortBy.ProductNameAToZ
            };

            return request;
        }

        /// <summary>
        /// Genarate initial Request of Search product request
        /// </summary>
        /// <param name="categoryId">id of selected category</param>
        /// <returns></returns>
        private SearchProductRequest CreateInitialSearchRequest(string searchString)
        {
            SearchProductRequest request = new SearchProductRequest()
            {
                Index = 1,
                NumberOfResultsPerPage = productPerPage,
                SortBy = ProductsSortBy.ProductNameAToZ,
                SearchString = searchString
            };

            return request;
        }

        /// <summary>
        /// Genarate request object to get list matched products from service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private GetProductsByCategoryRequest GenarateProductSeachRequest(GetProductsByCategoryRequest request){
            GetProductsByCategoryRequest productSeachRequest = new GetProductsByCategoryRequest()
            {
                CategoryId = request.CategoryId,
                BrandIds = request.BrandIds,
                SortBy = request.SortBy,
                BeginPrice = request.BeginPrice,
                EndPrice = request.EndPrice,
                Index = request.Index,
                NumberOfResultsPerPage = productPerPage,
                SearchString = request.SearchString
            };

            return productSeachRequest;
        }

        /// <summary>
        /// Genarate request object to get list matched products from service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private SearchProductRequest GenarateSeachRequest(SearchProductRequest request)
        {
            SearchProductRequest productSeachRequest = new SearchProductRequest()
            {
                CategoryIds = request.CategoryIds,
                BrandIds = request.BrandIds,
                SortBy = request.SortBy,
                Index = request.Index,
                NumberOfResultsPerPage = productPerPage,
                SearchString = request.SearchString
            };

            return productSeachRequest;
        }

        /// <summary>
        /// Create SelectList sort product options using for dropdownlist
        /// </summary>
        /// <param name="option">selected option</param>
        /// <returns></returns>
        private void PopulateStatusDropDownList(ProductsSortBy option = ProductsSortBy.ProductNameAToZ)
        {
            IEnumerable<ProductsSortBy> values = Enum.GetValues(typeof(ProductsSortBy)).Cast<ProductsSortBy>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((ProductsSortBy)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected = value == option,
                                                };

            ViewBag.SortProductOptionsSelectListItems = items;
        }

        #endregion

        #region controller actions

        /// <summary>
        /// Display product index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get ProductView contain list product of selected category
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns></returns>
        public ActionResult GetProductByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetProductsByCategoryRequest request = CreateInitialProductSearchRequest((int)id);
            GetProductsByCategoryResponse response = service.GetProductsByCategory(request);
            PopulateStatusDropDownList();
            PopulateNewProductList();
            PopulateBestSellProductList();
            PopulateCategoryList();
            PopulateTopCategoryList();
            return View("DisplayProducts",response);
        }

        /// <summary>
        /// Get list product matched conditions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductsByAjax(GetProductsByCategoryRequest request)
        {
            GetProductsByCategoryRequest productSearchRequest = GenarateProductSeachRequest(request);
            GetProductsByCategoryResponse response = service.GetProductsByCategory(productSearchRequest);
            return Json(response);
        }

        /// <summary>
        /// Get details of selected product
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns></returns>
        public ActionResult ProductDetails(int? id)
        {
            if(id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductDetailsView product = service.GetProductDetails((int)id);
            PopulateNewProductList();
            PopulateCategoryList();
            PopulateTopCategoryList();

            return View("ProductDetails", product);
        }

        /// <summary>
        /// Get ProductView contain list product of selected category
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns></returns>
        public ActionResult SearchProduct(string searchString, int? searchType)
        {
            SearchType type;
            if (searchType == null)
            {
                type = SearchType.SearchString;
            }
            else
            {
                type = (SearchType)searchType;
            }

            SearchProductRequest request = CreateInitialSearchRequest(searchString);
            SearchProductResponse response = service.SearchByProductName(request, type);
            PopulateStatusDropDownList();
            PopulateCategoryList();

            switch (type)
            {
                case SearchType.AllProduct:
                    {
                        @ViewBag.SearchTitle = "Tất cả các sản phẩm";
                        break;
                    }
                case SearchType.SearchString:
                    {
                        @ViewBag.SearchTitle = "Từ khóa : "+request.SearchString;
                        break;
                    }
                case SearchType.NewProducts:
                    {
                        @ViewBag.SearchTitle = "Sản phẩm mới";
                        break;
                    }
                case SearchType.BestSellProducts:
                    {
                        @ViewBag.SearchTitle = "Sản phẩm HOT";
                        break;
                    }
            }

            return View("SearchResult", response);
        }

        /// <summary>
        /// Get list product matched conditions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSearchProductsByAjax(SearchProductRequest request)
        {
            SearchProductRequest productSearchRequest = GenarateSeachRequest(request);
            SearchProductResponse response = service.SearchByProductName(productSearchRequest,SearchType.SearchString);
            return Json(response);
        }

        #endregion
    }
}