using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class CMSNewsView
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int? CoverImageId { get; set; }
        public string CoverImageName { get; set; }
        public string CoverImagePath { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ContentNews { get; set; }
        public string Authors { get; set; }
        public string Tags { get; set; }
        public Nullable<int> TotalView { get; set; }
        public Nullable<Boolean> DisplayHomePage { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}
