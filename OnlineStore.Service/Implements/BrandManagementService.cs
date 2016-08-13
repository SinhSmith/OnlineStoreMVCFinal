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
using OnlineStore.Infractructure.Utility;

namespace OnlineStore.Service.Implements
{
    public class BrandManagementService : IBrandManagementService
    {
        #region Properties

        private BrandRepository db = new BrandRepository(new OnlineStoreMVCEntities());
        private Repository<system_Profiles> systemProfiles = new Repository<system_Profiles>(new OnlineStoreMVCEntities());

        #endregion

        #region Constructures

        public BrandManagementService()
        {
            db = new BrandRepository(new OnlineStoreMVCEntities());
            systemProfiles = new Repository<system_Profiles>(new OnlineStoreMVCEntities());
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get brand with conditions(sort, filter, paging, search)
        /// </summary>
        /// <param name="pageNumber">current page index</param>
        /// <param name="pageSize">number of product per page</param>
        /// <param name="totalItems">return total products</param>
        /// <returns>list product of current page</returns>
        public IEnumerable<ecom_Brands> GetBrands(int pageNumber, int pageSize, out int totalItems)
        {
            IEnumerable<ecom_Brands> brands = db.GetAllAvailableBrands();
            totalItems = brands.Count();
            IEnumerable<ecom_Brands> returnBrandList = brands.OrderBy(b => b.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return returnBrandList;
        }
        /// <summary>
        /// Get information of a brand
        /// </summary>
        /// <param name="id">id of brand</param>
        /// <returns>return a view model to display on client side</returns>
        public Model.ViewModel.DetailsBrandManagementView GetDetailBrand(int id)
        {
            ecom_Brands brand = db.GetByID(id);
            if (brand == null) { throw new ArgumentNullException(); }
            // Get user create brand and user last time modified brand 
            system_Profiles createBy = systemProfiles.GetByID(brand.CreatedBy);
            system_Profiles modifiredBy = systemProfiles.GetByID(brand.CreatedBy);

            return brand.ConvertToDetailsBrandView(createBy != null ? createBy.UserName : "", modifiredBy != null ? modifiredBy.UserName : "");
        }
        /// <summary>
        /// Add new brand
        /// </summary>
        /// <param name="brand">information of new brand</param>
        /// <returns></returns>
        public bool AddBrand(ecom_Brands brand)
        {
            try
            {
                db.Insert(brand);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Get information of brand 
        /// </summary>
        /// <param name="id">id of brand</param>
        /// <returns></returns>
        public ecom_Brands GetBrandById(int id)
        {
            ecom_Brands brand = db.GetByID(id);
            return brand;
        }
        /// <summary>
        /// Update information of brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool UpdateBrand(ecom_Brands brand)
        {
            try
            {
                db.Update(brand);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Delete brand
        /// </summary>
        /// <param name="id">id of brand</param>
        /// <returns></returns>
        public bool DeleteBrand(int id)
        {
            try
            {
                ecom_Brands brand = db.GetByID(id);
                brand.Status = (int)Define.Status.Delete;
                db.Update(brand);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
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
