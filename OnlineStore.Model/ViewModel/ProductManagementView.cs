using OnlineStore.Model.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class ProductSummaryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string ProductCode { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [DisplayName("Giá bán")]
        public decimal Price { get; set; }
        [DisplayName("Ảnh đại diện")]
        public share_Images CoverImage { get; set; }
        [DisplayName("Ưu tiên sắp xếp")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng thái")]
        public string Status { get; set; }
    }

    public class ProductFullView
    {
        public int Id { get; set; }
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
        [DisplayName("Số lượt xem")]
        public Nullable<int> TotalView { get; set; }
        [DisplayName("Số lượt mua")]
        public Nullable<int> TotalBuy { get; set; }
        [DisplayName("Từ khóa tìm kiếm")]
        public string Tags { get; set; }
        [DisplayName("Sản phẩm mới")]
        public bool IsNewProduct { get; set; }
        [DisplayName("Sản phẩm bán chạy nhất")]
        public bool IsBestSellProduct { get; set; }
        [DisplayName("Ưu tiên sắp xếp")]
        public Nullable<int> SortOrder { get; set; }
        [DisplayName("Trạng thái")]
        public Nullable<int> Status { get; set; }
        [DisplayName("Người tạo")]
        public string CreatedBy { get; set; }
        [DisplayName("Ngày tạo")]
        public string CreatedDate { get; set; }
        [DisplayName("Người cập nhật")]
        public string ModifiedBy { get; set; }
        [DisplayName("Ngày cập nhật")]
        public string ModifiedDate { get; set; }
        [DisplayName("Danh mục sản phẩm")]
        public int[] CategoryId { get; set; }
        [DisplayName("Nhóm sản phẩm")]
        public int[] ProductGroupId { get; set; }

        [DisplayName("Danh mục sản phẩm")]
        public IEnumerable<ecom_Categories> Categories { get; set; }
        [DisplayName("Nhóm sản phẩm")]
        public IEnumerable<ecom_ProductGroups> ProductGroups { get; set; }
        public IEnumerable<ImageProductViewModel> share_Images { get; set; }
    }

    public class LoadListImageProductPartialViewModels
    {
        public int ProductId;
        public IEnumerable<share_Images> Images;
    }

    public class ListImageProductPartialViewModels
    {
        public int ProductId;
        public IEnumerable<ImageProductViewModel> Images;
        public Nullable<int> CoverImageId;
    }

    public class ImageProductViewModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; } 

    }
}
