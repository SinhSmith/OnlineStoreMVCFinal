using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class DetailsBrandManagementView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }
    public class EditBrandManagementPostRequest
    {
        public int Id { get; set; }
         [DisplayName("TÊN THƯƠNG HIỆU")]
        public string Name { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public Nullable<int> Status { get; set; }
        [DisplayName("THÔNG TIN VỀ THƯƠNG HIỆU")]
        public string Description { get; set; }
    }

    public class EditBrandManagementGetResponse
    {
        public int Id { get; set; }
        [DisplayName("TÊN THƯƠNG HIỆU")]
        public string Name { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public Nullable<int> Status { get; set; }
        [DisplayName("THÔNG TIN VỀ THƯƠNG HIỆU")]
        public string Description { get; set; }
    }
}
