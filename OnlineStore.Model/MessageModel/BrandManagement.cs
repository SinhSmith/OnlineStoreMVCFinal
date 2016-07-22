using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.MessageModel
{
    public class CreateBrandPostRequest
    {
        public int Id { get; set; }
        [DisplayName("TÊN THƯƠNG HIỆU")]
        public string Name { get; set; }
        [DisplayName("MÔ TẢ")]
        public string Description { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public Nullable<int> Status { get; set; }
    }
}
