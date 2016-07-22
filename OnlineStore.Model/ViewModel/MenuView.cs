using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class MenuView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string Target { get; set; }
        public Nullable<int> Type { get; set; }
        public string Icon { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
