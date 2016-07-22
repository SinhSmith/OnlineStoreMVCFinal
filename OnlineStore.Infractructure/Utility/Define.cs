using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infractructure.Utility
{
    public class Define
    {
        public const int PAGE_SIZE = 10;
        public enum Status
        {
            [Description("Ngưng hoạt động")]
            Deactive = 0,

            [Description("Đang hoạt động")]
            Active = 1,

            [Description("Xóa")]
            Delete = 2
        }

        public enum MenuEnum
        {
            [Description("Admin")]
            Admin = 1,

            [Description("User")]
            User = 2,
        }

        public enum BannerTypes
        {
            [Description("Banner mùa Xuân")]
            SpringSeason = 1,

            [Description("Banner mùa Hạ")]
            SummerSeason = 2,

            [Description("Banner mùa Thu")]
            AutumnSeason = 3,

            [Description("Banner mùa Đông")]
            WinterSeason = 4
        }
    }
}
