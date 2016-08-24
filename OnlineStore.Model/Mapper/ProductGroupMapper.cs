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
    public static class ProductGroupMapper
    {
        public static ecom_ProductGroups ConvertToProductGroupModel(this CreateProductGroupPostRequest productGroupRequest)
        {
            ecom_ProductGroups group = new ecom_ProductGroups()
            {
                Id = productGroupRequest.Id,
                Name = productGroupRequest.Name,
                Status = productGroupRequest.Status,
                Description = productGroupRequest.Description
            };

            return group;
        }

        public static ProductGroupViewModel ConvertToProductGroupViewModel(this ecom_ProductGroups group)
        {
            ProductGroupViewModel returnModel = new ProductGroupViewModel()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Status = (int)group.Status,
                    Description = group.Description
                };

            return returnModel;
        }
        public static ecom_ProductGroups ConvertToProductGroupViewModel(this EditProductGroupManagementPostRequest groupView)
        {
            ecom_ProductGroups returnModel = new ecom_ProductGroups()
            {
                Id = groupView.Id,
                Name = groupView.Name,
                Status = groupView.Status,
                Description = groupView.Description
            };

            return returnModel;
        }
        
    }
}
