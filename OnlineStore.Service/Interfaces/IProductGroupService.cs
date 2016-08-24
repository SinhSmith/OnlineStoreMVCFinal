using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Interfaces
{
    public interface IProductGroupService : IDisposable
    {
        /// <summary>
        /// Get group with conditions(sort, filter, paging, search)
        /// </summary>
        /// <param name="pageNumber">current page index</param>
        /// <param name="pageSize">number of product per page</param>
        /// <param name="totalItems">return total products</param>
        /// <returns>list product group of current page</returns>
        IEnumerable<ecom_ProductGroups> GetProductGroups(int pageNumber, int pageSize, out int totalItems);
        /// <summary>
        /// Add new group
        /// </summary>
        /// <param name="group">information of new group</param>
        /// <returns></returns>
        bool AddProductGroup(ecom_ProductGroups brand);
        /// <summary>
        /// Get information of group 
        /// </summary>
        /// <param name="id">id of group</param>
        /// <returns></returns>
        ecom_ProductGroups GetProductGroupById(int id);
        /// <summary>
        /// Update information of group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        bool UpdateProductGroup(ecom_ProductGroups request);
        /// <summary>
        /// Delete group
        /// </summary>
        /// <param name="id">id of group</param>
        /// <returns></returns>
        bool DeleteProductGroup(int id);
    }
}
