using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Mapper
{
    public static class BrandMapper
    {
        public static DetailsBrandManagementView ConvertToDetailsBrandView(this ecom_Brands brand,string createBy,string modifiredBy)
        {
            DetailsBrandManagementView returnView = new DetailsBrandManagementView()
            {
                Id = brand.Id,
                Name = brand.Name,
                Status = EnumHelper.GetDescriptionFromEnum((Define.Status)brand.Status),
                Description = brand.Description,
                CreatedBy = createBy,
                CreatedDate = string.Format("{0:yyyy-MM-dd}",brand.CreatedDate),
                ModifiedBy = modifiredBy,
                ModifiedDate = string.Format("{0:yyyy-MM-dd}", brand.ModifiedDate)
            };

            return returnView;
        }
        public static ecom_Brands ConvertToBrandModel(this CreateBrandPostRequest brandRequest)
        {
            ecom_Brands brand = new ecom_Brands()
            {
                Id = brandRequest.Id,
                Name = brandRequest.Name,
                Status = brandRequest.Status,
                Description = brandRequest.Description
            };

            return brand;
        }
        public static EditBrandManagementGetResponse ConvertToEditBrandManagementGetResponse(this ecom_Brands brand)
        {
            EditBrandManagementGetResponse returnModel = new EditBrandManagementGetResponse()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Status = (int)brand.Status,
                    Description = brand.Description
                };

            return returnModel;
        }
        public static ecom_Brands ConvertToBrandModel(this EditBrandManagementPostRequest brandView)
        {
            ecom_Brands returnModel = new ecom_Brands()
            {
                Id = brandView.Id,
                Name = brandView.Name,
                Status = brandView.Status,
                Description = brandView.Description
            };

            return returnModel;
        }
        public static BrandSummaryView ConvertToBrandSummaryView(this ecom_Brands brand)
        {
            BrandSummaryView brandSummaryView = new BrandSummaryView()
            {
               Id = brand.Id,
               Name = brand.Name
            };

            return brandSummaryView;
        }
        public static IEnumerable<BrandSummaryView> ConvertToBrandSummaryViews(this IEnumerable<ecom_Brands> brands)
        {
            ICollection<BrandSummaryView> brandSummaryViews = new List<BrandSummaryView>();
            foreach (var brand in brands)
            {
                brandSummaryViews.Add(brand.ConvertToBrandSummaryView());
            }

            return brandSummaryViews;
        }
    }
}
