using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AapkiMagazine.Models;
using AapkiMagazine.DataLayer;
using System.Text;
using AapkiMagazine.Helper;
using System.Net.Mail;
using Recaptcha;

namespace AapkiMagazine.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/Authenticate

        public ActionResult Authenticate()
        {
            LogOnModel VM = new LogOnModel();
            return View(VM);
        }

        [HttpPost]
        [RecaptchaControlMvc.CaptchaValidator]        
        public ActionResult Authenticate(LogOnModel model, string returnUrl, string command, bool captchaValid, string captchaErrorMessage)
        {
            DataAccessLayer datalayer = null;
            StringBuilder script = null; ;
            UserProfileItem userProfileItem = null;
            if (command == "Sign in")
            {
                ModelState["Email_New"].Errors.Clear();
                ModelState["Password_New"].Errors.Clear();
                ModelState["ConfirmPassword_New"].Errors.Clear();
                ModelState["FirstName"].Errors.Clear();
                ModelState["LastName"].Errors.Clear();
                ModelState["Email_Reset"].Errors.Clear();

                if (ModelState.IsValid)
                {
                    datalayer = new DataAccessLayer();
                    userProfileItem = datalayer.ValidateAndGetUserProfile(model.UserEmail, model.Password);
                    if (userProfileItem != null)
                    {
                        Session["UserProfile"] = userProfileItem;
                        FormsAuthentication.SetAuthCookie(userProfileItem.First_Name + " " + userProfileItem.Last_Name, false);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Manage");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("$('#validationerror').addClass('alert-warning').show();$('.login-form').show();$('.register-form').hide();$('.forgotpwd-form').hide();");
                        script.Append("</script>");
                        ViewBag.StartScript = script.ToString();
                        model.Password = string.Empty;
                        return View(model);
                    }
                }
                else
                {
                    //show validation error

                    script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('.loginvalidation').show();$('#validationerror').addClass('alert-warning').show();$('.login-form').show();$('.register-form').hide();$('.forgotpwd-form').hide();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    model.Password = string.Empty;
                }
            }
            else if (command == "Register")
            {
                ModelState["UserEmail"].Errors.Clear();
                ModelState["Password"].Errors.Clear();
                ModelState["Email_Reset"].Errors.Clear();


                if (ModelState.IsValid)
                {
                    if (captchaValid)
                    {                        

                        //check if Password and confirm password donot match
                        if (model.Password_New != model.ConfirmPassword_New)
                        {
                            ModelState.AddModelError("", "Password and confirm password donot match.");
                            script = new StringBuilder();
                            script.Append("<script type='text/javascript'>");
                            script.Append("$('#validationerror').addClass('alert-warning').show();$('.register-form').show();$('.login-form').hide();$('.forgotpwd-form').hide();");
                            script.Append("</script>");
                            ViewBag.StartScript = script.ToString();
                            model.Password_New = string.Empty;
                            model.ConfirmPassword_New = string.Empty;
                            return View(model);
                        }

                        datalayer = new DataAccessLayer();
                        //check if User is already registered with मैं हूँ आम आदमी.
                        if (!datalayer.UserEmailExist(model.Email_New))
                        {
                            //create new account
                            bool isNewUserCreated = datalayer.CreateNewUser(model.Email_New, model.Password_New, model.FirstName, model.LastName);
                            if (!isNewUserCreated)
                            {
                                ModelState.AddModelError("", "Error occured while registering your profile. You can contact our support team for more detail.");
                                model.Password_New = string.Empty;
                                model.ConfirmPassword_New = string.Empty;
                                return View(model);
                            }
                            else
                            {
                                script = new StringBuilder();
                                script.Append("<script type='text/javascript'>");
                                script.Append("$('#success').addClass('alert-success').show();$('#success').append( '<p><strong>Thank you</strong> for registring on <strong>मैं हूँ आम आदमी </strong><br />We will review and properly direct your application, and someone will be in touch with you shortly.</p>' );$('#success').show();$('#success').delay(5000).fadeOut();");
                                script.Append("</script>");
                                ViewBag.StartScript = script.ToString();
                                model = new LogOnModel();
                                return View(model);
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("", "User is already registered with मैं हूँ आम आदमी.");
                            script = new StringBuilder();
                            script.Append("<script type='text/javascript'>");
                            script.Append("$('#validationerror').addClass('alert-warning').show();$('.register-form').show();$('.login-form').hide();$('.forgotpwd-form').hide();");
                            script.Append("</script>");
                            ViewBag.StartScript = script.ToString();
                            model.Password_New = string.Empty;
                            model.ConfirmPassword_New = string.Empty;
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", captchaErrorMessage);
                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("$('#validationerror').addClass('alert-warning').show();$('.register-form').show();$('.login-form').hide();$('.forgotpwd-form').hide();");
                        script.Append("</script>");
                        ViewBag.StartScript = script.ToString();
                        model.Password_New = string.Empty;
                        model.ConfirmPassword_New = string.Empty;
                        return View(model);
                    }
                }
                else
                {
                    //show validation error

                    script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('.registervalidation').show();$('#validationerror').addClass('alert-warning').show();$('.register-form').show();$('.login-form').hide();$('.forgotpwd-form').hide();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                    model.Password_New = string.Empty;
                    model.ConfirmPassword_New = string.Empty;
                }
            }
            else if (command == "Reset Password")
            {
                ModelState["UserEmail"].Errors.Clear();
                ModelState["Password"].Errors.Clear();
                ModelState["Email_New"].Errors.Clear();
                ModelState["Password_New"].Errors.Clear();
                ModelState["ConfirmPassword_New"].Errors.Clear();
                ModelState["FirstName"].Errors.Clear();
                ModelState["LastName"].Errors.Clear();


                if (ModelState.IsValid)
                {
                    datalayer = new DataAccessLayer();
                    if (datalayer.UserEmailExist(model.UserEmail))
                    {
                        //reset the password and send new password to user email

                        //string newpassword = Helper_Magazine.CreateSalt(8);
                        //if (datalayer.ResetPassword(model.UserEmail, newpassword)) {
                        //    MailMessage messagetouser = new MailMessage();
                        //    messagetouser.Subject = "Main Hun Aam Aadmi - New Password  ---";
                        //    string msgBodyuser = "Dear " + model.UserEmail + System.Environment.NewLine

                        //        + " We have recieved your request. Find new password as below:-" + System.Environment.NewLine + newpassword
                        //        + " Thanks!" + System.Environment.NewLine
                        //        + "Note: ----system generated mail: Please don't reply----";
                        //    messagetouser.Body = msgBodyuser;
                        //    messagetouser.From = new MailAddress("info@aapkikranti.in");
                        //    messagetouser.To.Add(model.UserEmail);
                        //    messagetouser.IsBodyHtml = true;
                        //    SendMail(messagetouser);
                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("$('#success').addClass('alert-success').show();$('#success').append( ' <p>You will soon recieve password reset info on your registered email address.</p>' );$('#success').show();$('#success').delay(5000).fadeOut();");
                        script.Append("</script>");
                        ViewBag.StartScript = script.ToString();
                        model = new LogOnModel();
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "User E-mail address is not registered with मैं हूँ आम आदमी");
                        script = new StringBuilder();
                        script.Append("<script type='text/javascript'>");
                        script.Append("$('#validationerror').addClass('alert-warning').show();$('.forgotpwd-form').show();$('.login-form').hide();$('.register-form').hide();");
                        script.Append("</script>");
                        ViewBag.StartScript = script.ToString();
                        return View(model);
                    }
                }
                else
                {
                    //show validation error

                    script = new StringBuilder();
                    script.Append("<script type='text/javascript'>");
                    script.Append("$('.forgotpwdvalidation').show();$('#validationerror').addClass('alert-warning').show();$('.forgotpwd-form').show();$('.login-form').hide();$('.register-form').hide();");
                    script.Append("</script>");
                    ViewBag.StartScript = script.ToString();
                }

            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Authenticate", "Account");
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
