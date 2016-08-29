using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class BannerViewModel
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> Status { get; set; }
        public string StartEndDate { get; set; }
    }
}
