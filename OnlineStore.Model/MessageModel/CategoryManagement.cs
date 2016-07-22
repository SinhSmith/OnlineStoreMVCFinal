using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.MessageModel
{
    public class CreateCategoryPostRequest
    {
        public int Id { get; set; }
        [DisplayName("TÊN DANH MỤC")]
        public string Name { get; set; }
        [DisplayName("TÊN DANH MỤC CHA")]
        public Nullable<int> ParentId { get; set; }
        [DisplayName("MÔ TẢ")]
        public string Description { get; set; }
        [DisplayName("ĐƯỜNG DẪN")]
        public string Url { get; set; }
        [DisplayName("ƯU TIÊN SẮP XẾP")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public Nullable<int> Status { get; set; }
    }
}
