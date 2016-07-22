using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class SummaryCategoryViewModel
    {
        public int Id { get; set; }
        [DisplayName("TÊN DANH MỤC")]
        public string Name { get; set; }
        [DisplayName("ƯU TIÊN SẮP XẾP")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public string Status { get; set; }
    }

    public class DetailCategoryViewModel
    {
        public int Id { get; set; }
        [DisplayName("TÊN DANH MỤC")]
        public string Name { get; set; }
        [DisplayName("TÊN DANH MỤC CHA")]
        public string ParentCategory { get; set; }
        [DisplayName("MÔ TẢ")]
        public string Description { get; set; }
        [DisplayName("ĐƯỜNG DẪN")]
        public string Url { get; set; }
        [DisplayName("ƯU TIÊN SẮP XẾP")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("TRẠNG THÁI")]
        public string Status { get; set; }
        [DisplayName("NGƯỜI TẠO")]
        public string CreatedBy { get; set; }
        [DisplayName("NGÀY TẠO")]
        public string CreatedDate { get; set; }
        [DisplayName("NGƯỜI CẬP NHẬT GẦN NHẤT")]
        public string ModifiedBy { get; set; }
        [DisplayName("NGÀY CẬP NHẬT GẦN NHẤT")]
        public string ModifiedDate { get; set; }
    }

    public class CategoryViewModel
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
