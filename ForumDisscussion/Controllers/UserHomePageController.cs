

using System;

using System.Data;
using System.Linq;

using System.Web.Mvc;

using ForumDisscussion.Models;

namespace ForumDisscussion.Controllers
{
    public class UserHomePageController : Controller
    {
        readonly ForumDataBaseConnectionContext _db = new ForumDataBaseConnectionContext();



        [Authorize]
        public ActionResult UserAnsQuestionList()
        {


            int authUserId = _db.ForumUser.Single(m => m.UserName.Equals(User.Identity.Name)).UserId;
            ViewData["ProfileInfo"] = from u in _db.ForumUser
                                      where u.UserId == authUserId
                                      select new ForumUser()
                                          {
                                              FirstName = u.FirstName,
                                              LastName = u.LastName,
                                              UserName = u.UserName,
                                              Country = u.Country,
                                              Email = u.Email
                                          };
       
                                           
            return View(_db.ForumUser.Where(m=>m.UserName.Equals(User.Identity.Name)).ToList());




        }

     
    
  

       

               public ActionResult UpdateImage()
               {

                   return View();
               }
                [HttpPost]
               public ActionResult UpdateImage(ImageUpdateViewModel m)
               {
                    if (!ModelState.IsValid) return View();
                    var c = _db.ForumUser.SingleOrDefault(u => u.UserName.Equals(User.Identity.Name));
                    if (c != null)
                    {
                        var authUserId = c.UserId;
                        var usr = _db.ForumUser.Find(authUserId);
                        var data = new byte[m.File.ContentLength];
                        m.File.InputStream.Read(data, 0, m.File.ContentLength);
                        m.Image = data;

                        usr.Image = m.Image;
                        _db.Entry(usr).State=EntityState.Modified;
                    }
                    _db.SaveChanges();
                    ViewBag.imgUpSuccessId = 1;
                    return View();
               }

        public ActionResult UpdatePassword()
        {
          

            return View();

        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdatePassword(UserPasswordUpdateModel model)
        {
            if (!ModelState.IsValid) return View();
            var v = _db.ForumUser.SingleOrDefault(m => m.UserName.Equals(User.Identity.Name));
            if (v == null) return View();
            var id = v.UserId;
            var checkOldPassword = _db.ForumUser.SingleOrDefault(m=>m.UserId.Equals(id)&&m.Password.Equals(model.OldPassword));
            if (checkOldPassword!=null)
            {
                var u = _db.ForumUser.Find(id);
                u.Password = model.NewPassword;
                _db.Entry(u).State=EntityState.Modified;
                _db.SaveChanges();
                ViewBag.successMs =1;
            }
            else
            {
                ModelState.AddModelError("","* Sorry,Your entered password do not match with your old password!");
            }

            return View();

            }
        }
    }

