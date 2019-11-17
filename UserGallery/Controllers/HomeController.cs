using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using UserGallery.Models;
using System.Net.Http.Headers;

namespace UserGallery.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(string title,string name)
        {
            IEnumerable<UserViewModel> usermodel;
            IEnumerable<UserViewModel> users = GetDataList("users");
            IEnumerable<UserViewModel> photos = GetDataList("photos");
            if (!(String.IsNullOrEmpty(title)) && !(String.IsNullOrEmpty(name)))
            {
                usermodel = from usr in users
                            join phts in photos
                            on usr.Id equals phts.Id
                            where phts.Title == title && usr.Name == name
                            select new UserViewModel
                            {
                                Id = usr.Id,
                                Name = usr.Name,
                                Email = usr.Email,
                                Phone = usr.Phone,
                                address = usr.address,
                                Title = phts.Title,
                                ThumbnailUrl = phts.ThumbnailUrl
                            };
            }
            else if ((!(String.IsNullOrEmpty(title))))
            {
                usermodel = from usr in users
                            join phts in photos
                            on usr.Id equals phts.Id
                            where phts.Title == title
                            select new UserViewModel
                            {
                                Id = usr.Id,
                                Name = usr.Name,
                                Email = usr.Email,
                                Phone = usr.Phone,
                                address = usr.address,
                                Title = phts.Title,
                                ThumbnailUrl = phts.ThumbnailUrl
                            };
            }
            else if ((!(String.IsNullOrEmpty(name))))
            {
                usermodel = from usr in users
                            join phts in photos
                            on usr.Id equals phts.Id
                            where usr.Name == name
                            select new UserViewModel
                            {
                                Id = usr.Id,
                                Name = usr.Name,
                                Email = usr.Email,
                                Phone = usr.Phone,
                                address = usr.address,
                                Title = phts.Title,
                                ThumbnailUrl = phts.ThumbnailUrl
                            };
            }
            else
            {
                usermodel = from usr in users
                            join phts in photos
                                on usr.Id equals phts.Id
                            select new UserViewModel
                            {
                                Id = usr.Id,
                                Name = usr.Name,
                                Email = usr.Email,
                                Phone = usr.Phone,
                                address = usr.address,
                                Title = phts.Title,
                                ThumbnailUrl = phts.ThumbnailUrl
                            };
            }
            return View(usermodel);
          
            
        }

        public ActionResult UserDetails(int Id)
        {
            
          return View(GetDetails("users/",Id));
                
        }

        public ActionResult AlbumDetails(int Id)
        {
            return View(GetDetails("photos/", Id));
        }
        public IEnumerable<UserViewModel> GetDataList(string type)
        {
            IEnumerable<UserViewModel> users = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                //HTTP GET
                var responseTask = client.GetAsync(type);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<UserViewModel>>();
                    readTask.Wait();

                    users = readTask.Result.ToList();

                }
                else 
                {
               
                    users = Enumerable.Empty<UserViewModel>();

                }
            }
            return users;
        }
        public UserViewModel GetDetails(string type,int Id)
        {
            UserViewModel user = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(type + Id, HttpCompletionOption.ResponseContentRead).Result;
                if (response.IsSuccessStatusCode)
                {
                    var aa = response.Content.ReadAsAsync<object>().Result;
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(aa.ToString());
                }

                return user;
            }
        }
    }
}