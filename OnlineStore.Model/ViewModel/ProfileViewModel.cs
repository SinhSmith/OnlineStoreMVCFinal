using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Model.ViewModel
{
    public class ProfileViewModel
    {
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Emaill { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int? Status { get; set; }
    }
}
