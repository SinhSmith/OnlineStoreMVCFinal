using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Repository
{
    public class ProductRepository : Repository<ecom_Products>
    {
        #region Constructures

        public ProductRepository(OnlineStoreMVCEntities context)
            : base(context)
        {

        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Products> GetAllProducts()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Get products except which one have status is Delete(Just Active and Deactive)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ecom_Products> GetAllProductsWithoutDelete()
        {
            return dbSet.Include("share_Images").Include("CoverImage").Where(c => c.Status != (int)Define.Status.Delete).ToList();
        }

        /// <summary>
        /// Find product by id with status not equal to Delete
        /// </summary>
        /// <returns></returns>
        public ecom_Products GetProductById(int id)
        {
            return dbSet.Include("share_Images").Include("CoverImage").Where(c => c.Id == id && c.Status != (int)Define.Status.Delete).FirstOrDefault();
        }

        #endregion
    }
}
