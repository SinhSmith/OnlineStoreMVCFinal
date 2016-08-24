using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using OnlineStore.Model.Repository;
using OnlineStore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Implements
{
    public class ProductGroupService : IProductGroupService
    {
        #region Properties

        private ProductGroupRepository db = new ProductGroupRepository(new OnlineStoreMVCEntities());

        #endregion

        #region Constructures

        public ProductGroupService()
        {
            db = new ProductGroupRepository(new OnlineStoreMVCEntities());
        }

        #endregion

        #region Public functions

        public IEnumerable<Model.Context.ecom_ProductGroups> GetProductGroups(int pageNumber, int pageSize, out int totalItems)
        {
            IEnumerable<ecom_ProductGroups> groups = db.GetAllAvailableGroups();
            totalItems = groups.Count();
            IEnumerable<ecom_ProductGroups> returnBrandList = groups.OrderBy(b => b.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return returnBrandList;
        }

        public bool AddProductGroup(Model.Context.ecom_ProductGroups group)
        {
            try
            {
                db.Insert(group);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Model.Context.ecom_ProductGroups GetProductGroupById(int id)
        {
            ecom_ProductGroups group = db.GetByID(id);
            return group;
        }

        public bool UpdateProductGroup(ecom_ProductGroups group)
        {
            try
            {
                db.Update(group);
                db.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProductGroup(int id)
        {
            try
            {
                ecom_ProductGroups group = db.GetByID(id);
                group.Status = (int)Define.Status.Delete;
                db.Update(group);
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
        }

        #endregion
    }
}
