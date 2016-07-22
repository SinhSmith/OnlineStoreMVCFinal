using OnlineStore.Model.Context;
using OnlineStore.Model.Repository;
using OnlineStore.Model.ViewModel;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Model.Mapper;

namespace OnlineStore.Service.Implements
{
    public class CMSCategoryExtensions
    {
        public static string GetFormattedBreadCrumb(CMSCategoryView category, IList<CMSCategoryView> allCategories, string separator = ">>")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, allCategories);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Title;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : string.Format("{0} {1} {2}", result, separator, categoryName);
            }

            return result;
        }

        public static IList<CMSCategoryView> GetCategoryBreadCrumb(CMSCategoryView category, IList<CMSCategoryView> allCategories)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<CMSCategoryView>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(category);

                alreadyProcessedCategoryIds.Add(category.Id);

                category = (from c in allCategories
                            where c.Id == category.ParentId
                            select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }

        public static string GetFormattedBreadCrumb(CMSCategoryView category, ICMSCategoryService categoryService, string separator = ">>")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(category, categoryService);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Title;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : string.Format("{0} {1} {2}", result, separator, categoryName);
            }

            return result;
        }

        public static IList<CMSCategoryView> GetCategoryBreadCrumb(CMSCategoryView category, ICMSCategoryService categoryService)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            var result = new List<CMSCategoryView>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (category != null && //not null
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                result.Add(category);

                alreadyProcessedCategoryIds.Add(category.Id);

                category = categoryService.GetCategoryById(category.ParentId);
            }
            result.Reverse();
            return result;
        }
    }
}
