using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IDisplayProductService:IDisposable
    {
        void RefreshAll();
        GetProductsByCategoryResponse GetProductsByCategory(GetProductsByCategoryRequest request);
        ProductDetailsView GetProductDetails(int id);
        IEnumerable<ProductSummaryView> GetAllNewProduct();
        IEnumerable<ProductSummaryView> GetAllBestSellProduct();
        IEnumerable<ProductSummaryView> GetListHighPriorityOrderProduct();
        IEnumerable<ProductSummaryView> GetListClassicStyleProduct();
        IEnumerable<ProductSummaryView> GetListModernStyleProduct();
        IEnumerable<SummaryCategoryViewModel> GetTopCategories();
        SearchProductResponse SearchByProductName(SearchProductRequest request, SearchType searchType);
    }
}
