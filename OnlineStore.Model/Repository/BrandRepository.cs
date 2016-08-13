using OnlineStore.Infractructure.Utility;
using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.Repository
{
    public class BrandRepository : Repository<ecom_Brands>
    {
        #region Constructures

        public BrandRepository(OnlineStoreMVCEntities context):base(context)
        {
            
        }

        #endregion

        #region Public functions

        /// <summary>
        /// Get all brand except which one have status equal Delete
        /// </summary>
        /// <returns></returns>
        public IList<ecom_Brands> GetAllAvailableBrands()
        {
            return dbSet.Where(b => b.Status!= (int)Define.Status.Delete).ToList();
        }

        #endregion
    }
}
