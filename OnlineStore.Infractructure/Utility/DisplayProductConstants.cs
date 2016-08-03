using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infractructure.Utility
{
    public static class DisplayProductConstants
    {
        public const int NumberProductPerPage = 10;
        public const string BackGroundImage = @"/Content/Images/background_image.png";
        public const string NoImagePath = @"/Content/Images/no-image.png";
        public const string LargeProductImageFolderPath = @"/Content/Images/ProductImages/LargeImages/";
        public const string SmallProductImageFolderPath = @"/Content/Images/ProductImages/SmallImages/";
    }

    public enum ProductsSortBy
    {
        [Description("Giá: từ cao xuống thấp")]
        PriceHighToLow = 1,
        [Description("Giá: từ thấp lên cao")]
        PriceLowToHigh = 2,
        [Description("Tên: từ A đến Z")]
        ProductNameAToZ = 3,
        [Description("Tên: từ Z đến A")]
        ProductNameZToA = 4
    }

    public enum SearchType
    {
        [Description("Tất cả các sản phẩm")]
        AllProduct = 1,
        [Description("Tìm kiếm theo từ khóa")]
        SearchString = 2,
        [Description("Sản phẩm mới")]
        NewProducts = 3,
        [Description("Sản phẩm bán chạy nhất")]
        BestSellProducts = 4
    }
}
