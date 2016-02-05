using System;
using System.Data;
using System.Linq;

using System.Web.Mvc;
using System.Web.Security;

using ForumDisscussion.Models;





namespace ForumDisscussion.Controllers
{

    public class AccountController : Controller
    {
        readonly ForumDataBaseConnectionContext _dbCon = new ForumDataBaseConnectionContext();


        [AllowAnonymous]
        public ActionResult Login(int? id)
        {


            return View();
        }



        [HttpPost]
        [AllowAnonymous]

        public ActionResult Login(UserLogin users, string returnUrl,int? id)
        {
            try
            {



                if (ModelState.IsValid)
                {

                    var u = _dbCon.ForumUser.SingleOrDefault(
                                    m => m.UserName.Equals(users.UserName) && m.Password.Equals(users.Password));


                    if (u != null)
                    {

                        FormsAuthentication.SetAuthCookie(users.UserName, users.RememberMe);
                        UpdateLoginTime(users.UserName);
                        if (TempData["UQC"] != null)
                        {
                            return RedirectToAction("PostQuestion", "UserQuestions");
                        }
                        if (TempData["operation"] != null)
                        {

                            return RedirectToAction("PostAnswer", "Home",new{id=id.Value});
                        }
                        if (TempData["HPostController"] != null)
                        {

                            return RedirectToAction("PostComment", "Home", new { id = id.Value });
                        }
                        if (TempData["AreaFQAC"] != null)
                        {

                            return RedirectToAction("PostComment", "ForumQuestionAns");
                        }
                        if (TempData["AreaFQAnsPostController"] != null)
                        {
                            return RedirectToAction("PostAnswerForAnsweredQuestion", "ForumQuestionAns");
                        }
                        if (TempData["HomeAnsPostController"] != null)
                        {
                            return RedirectToAction("PostAnswer", "Home", new { id = id.Value });
                        }

                        if (TempData["FQACPostAnsUnAnsweredQuestion"] != null)
                        {
                            return RedirectToAction("PostAnswer", "ForumQuestionAns");
                        }




                    }
                    else
                    {
                        ViewBag.LoginFailedMessageId = 1;
                        return View();
                    }
                    return RedirectToAction("UserAnsQuestionList", "UserHomePage");
                }

            }
            catch (Exception message)
            {

             
            }
            return View();
        }

        private  void UpdateLoginTime(string username)
        {

            var user = _dbCon.ForumUser.Single(x => x.UserName.Equals(username));
            user.LastLogin = DateTime.Now;
            _dbCon.Entry(user).State=EntityState.Modified;
            _dbCon.SaveChanges();
        }


    
        public ActionResult LogOff()
        {
            TempData["UQC"] = null;
            TempData["AreaFQAC"] = null;
            TempData["HPostController"] = null;
            TempData["operation"] = null;
            TempData["HomeAnsPostController"] = null;
            TempData["AreaFQAnsPostController"] = null;
            TempData["FQACPostAnsUnAnsweredQuestion"] = null;
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

      
    }
}
