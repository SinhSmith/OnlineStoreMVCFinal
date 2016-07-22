using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.MessageModel
{
    public class CreateProductPostRequest{
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayName("Giá bán")]
        public decimal Price { get; set; }
        [DisplayName("Số lượng")]
        public Nullable<int> Quantity { get; set; }
        [DisplayName("Đơn vị")]
        public Nullable<int> Unit { get; set; }
        [DisplayName("Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        [DisplayName("Ảnh đại diện")]
        public Nullable<int> CoverImageId { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Mô tả chi tiết")]
        public string Description2 { get; set; }
        [DisplayName("Từ khóa tìm kiếm")]
        public string Tags { get; set; }
        [DisplayName("Sản phẩm mới?")]
        public bool IsNewProduct { get; set; }
        [DisplayName("Sản phẩm bán chạy?")]
        public bool IsBestSellProduct { get; set; }
        [DisplayName("Ưu tiên sắp xếp")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng thái")]
        public Nullable<int> Status { get; set; }
    }
    public class UpdateProductImageRequest
    {
        public int productId{get;set;}
        public int ImageId{get;set;}
        public string Name{get;set;}
        public bool IsActive{get;set;}
        public bool IsCoverImage{get;set;}
    }
}
