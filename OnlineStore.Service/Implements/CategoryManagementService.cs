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
using OnlineStore.Model.MessageModel;
using OnlineStore.Infractructure.Utility;

namespace OnlineStore.Service.Implements
{
    public class CategoryManagementService : ICategoryManagementService
    {
        #region Properties

        private CategoryRepository db = new CategoryRepository(new OnlineStoreMVCEntities());
        private Repository<system_Profiles> systemProfiles = new Repository<system_Profiles>(new OnlineStoreMVCEntities());

        #endregion

        #region Constructures

        public CategoryManagementService()
        {
            db = new CategoryRepository(new OnlineStoreMVCEntities());
            systemProfiles = new Repository<system_Profiles>(new OnlineStoreMVCEntities());
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get list summary category, which have status is Active or Deactive
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SummaryCategoryViewModel> GetListCategories()
        {
            return db.GetAllCategoriesWithoutDelete().ConvertToIndexCategoryViews();
        }

        /// <summary>
        /// Get all available category 
        /// </summary>
        /// <returns>list category</returns>
        public IEnumerable<ecom_Categories> GetAllCategories()
        {
            return db.GetAllCategoriesWithoutDelete();
        }

        /// <summary>
        /// Get list categories with paging, sort, filter
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SummaryCategoryViewModel> GetCategories(int pageNumber, int pageSize, out int totalItems)
        {
            IEnumerable<ecom_Categories> categories = db.GetAllCategoriesWithoutDelete();
            totalItems = categories.Count();
            IEnumerable<ecom_Categories> returnCategoryList = categories.OrderBy(b => b.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return returnCategoryList.ConvertToIndexCategoryViews();
        }

        /// <summary>
        /// Get detail category after id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DetailCategoryViewModel GetDetailCategory(int id)
        {
            string parentCategory = "";
            ecom_Categories category = db.GetByID(id);
            if (category == null)
            {
                return null;
            }
            else
            {
                // Get user create brand and user last time modified brand 
                system_Profiles createBy = systemProfiles.GetByID(category.CreatedBy);
                system_Profiles modifiredBy = systemProfiles.GetByID(category.CreatedBy);
                // get parent category
                if (category.ParentId != null)
                {
                    ecom_Categories parent = db.GetByID(category.ParentId);
                    parentCategory = parent != null ? parent.Name : "";
                }

                return category.ConvertToDetailCategoryViewModel(parentCategory, createBy, modifiredBy);
            }
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ecom_Categories GetCategoryById(int id)
        {
            ecom_Categories category = db.GetCategoryById(id);
            if (category != null && category.Status != (int)Define.Status.Delete)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Add a new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool AddCategory(CreateCategoryPostRequest category)
        {
            try
            {
                db.Insert(category.ConvertToCategoryModel());
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="viewModel">information of category need to update</param>
        /// <returns>if success return true or if fail return false</returns>
        public bool UpdateCategory(CategoryViewModel viewModel)
        {
            try
            {
                ecom_Categories category = db.GetByID(viewModel.Id);
                category.Name = viewModel.Name;
                category.Description = viewModel.Description;
                category.Url = viewModel.Url;
                category.SortOrder = viewModel.SortOrder;
                category.Status = (int)viewModel.Status;
                category.ParentId = viewModel.ParentId;

                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a category by change their status to delete
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns>return true if delete success and return false if delete fail</returns>
        public bool DeleteCategory(int id)
        {
            try
            {
                ecom_Categories category = db.GetByID(id);
                category.Status = (int)Define.Status.Delete;
                db.Update(category);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get category by id and return category view model object to show in client side
        /// </summary>
        /// <param name="id">id of category</param>
        /// <returns>list category view model</returns>
        public CategoryViewModel getCategoryViewModel(int id)
        {
            ecom_Categories category = db.GetCategoryById(id);
            if (category != null)
            {
                return category.ConvertToCategoryViewModel();
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Release resources

        /// <summary>
        /// Dispose database connection using in repositories, which used in this service
        /// </summary>
        public void Dispose()
        {
            db.Dispose();
            systemProfiles.Dispose();
        }

        #endregion
    }
}
