using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStoreMVC.Models.ImageModels
{
    public class ImageResult
    {
        public bool Success { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string ErrorMessage { get; set; }
    }
}