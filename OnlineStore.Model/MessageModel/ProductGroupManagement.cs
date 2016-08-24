using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.MessageModel
{
    public class CreateProductGroupPostRequest
    {
        public int Id { get; set; }
        [DisplayName("TÊN NHÓM SẢN PHẨM")]
        public string Name { get; set; }
        [DisplayName("MÔ TẢ")]
        public string Description { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public Nullable<int> Status { get; set; }
    }
}
