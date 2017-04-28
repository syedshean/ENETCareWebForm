﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENETCareBusinessLogic;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ENET
{
    public partial class Login : System.Web.UI.Page
    {
        LoginValidation checkLogin = new LoginValidation();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserName"] = "";
            //InsertUser(sender, e);

        }
        protected void loginEventMethod(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(usernameTextBox.Text, passwordTextBox.Text);

            if (user != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                var logedUser = userIdentity.GetUserId().ToString();
                var role = userManager.GetRoles(logedUser);

                LoginRegardingRole(role[0]);

            }
            else
            {
                Response.Write("Wrong Username or Password");
                //StatusText.Text = "Invalid username or password.";
                //LoginStatus.Visible = true;
            }

            //string page = checkLogin.LoginCheck(usernameTextBox.Text, passwordTextBox.Text);
            //if (page.Equals("Wrong"))
            //{
            //    Response.Write("Wrong Username or Password");
            //}
            //else
            //{
            //    Response.Redirect(page);
            //}
        }

        public void InsertUser(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = "Diana" };
            IdentityResult result = manager.Create(user, "12345678");

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);
                //Response.Redirect("~/Login.aspx");
            }
            else
            {
                StatusMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        public void LoginRegardingRole(string role)
        {
            if (role.Equals("Accountant"))
                Response.Redirect("~/AccountantHomePage.aspx");
            else if (role.Equals("SiteEng"))
                Response.Redirect("~/SiteEngineerHomePage.aspx");
            else if (role.Equals("manager"))
                Response.Redirect("~/ManagerHomePage.aspx");

        }
    }
}