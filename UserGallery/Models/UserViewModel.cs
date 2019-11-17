using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserGallery.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address address { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Body { get; set; }
    }
}