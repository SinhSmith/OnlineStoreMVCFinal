using OnlineStore.Infractructure.Helper;
using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Model.MessageModel;
using OnlineStore.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Mapper
{
    public static class CategoryMapper
    {
        public static SummaryCategoryViewModel ConvertToIndexCategoryView(this ecom_Categories category)
        {
            SummaryCategoryViewModel categoryView = new SummaryCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                SortOrder = category.SortOrder,
                Status = EnumHelper.GetDescriptionFromEnum((Define.Status)category.Status)
            };

            return categoryView;
        }

        public static IEnumerable<SummaryCategoryViewModel> ConvertToIndexCategoryViews(this IEnumerable<ecom_Categories> categories)
        {
            foreach (ecom_Categories category in categories)
            {
                yield return category.ConvertToIndexCategoryView();
            }
        }

        /// <summary>
        /// Convert Category context model to full detail category model
        /// </summary>
        /// <param name="category"></param>
        /// <param name="parentName"></param>
        /// <param name="createBy"></param>
        /// <param name="modifiredBy"></param>
        /// <returns></returns>
        public static DetailCategoryViewModel ConvertToDetailCategoryViewModel(this ecom_Categories category, string parentName, system_Profiles createBy, system_Profiles modifiredBy)
        {
            DetailCategoryViewModel detailCategory = new DetailCategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategory = parentName,
                Description = category.Description,
                Url = category.Url,
                SortOrder = category.SortOrder,
                Status = EnumHelper.GetDescriptionFromEnum((Define.Status)category.Status),
                CreatedBy = createBy!=null?createBy.UserName:"",
                CreatedDate = string.Format("{0:yyyy-MM-dd}", category.CreatedDate),
                ModifiedBy = modifiredBy != null ? createBy.UserName : "",
                ModifiedDate = string.Format("{0:yyyy-MM-dd}", category.ModifiedDate)
            };

            return detailCategory;
        }

        public static ecom_Categories ConvertToCategoryModel(this CreateCategoryPostRequest viewModel)
        {
            ecom_Categories category = new ecom_Categories()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Url = viewModel.Url,
                SortOrder = viewModel.SortOrder,
                ParentId = viewModel.ParentId,
                Status = viewModel.Status != null? (int)viewModel.Status:(int)Define.Status.Deactive,
                CreatedBy = null,
                CreatedDate = DateTime.Now,
                ModifiedBy = null,
                ModifiedDate = null
            };

            return category;
        }

        public static CategoryViewModel ConvertToCategoryViewModel(this ecom_Categories category)
        {
            CategoryViewModel viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Url = category.Url,
                SortOrder = category.SortOrder,
                ParentId = category.ParentId,
                Status = category.Status
            };

            return viewModel;
        } 
    }
}
