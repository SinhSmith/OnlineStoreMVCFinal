using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Service.Messaging
{
    public class UpdateProductImage
    {
        public int ImageId { get; set; }
        public string Name{get;set;}
        public bool IsActive{get;set;}
    }
}
