﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ENETCareWebForm
{
    public partial class UserListPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisableMasterPageButtons();
        }

        public void DisableMasterPageButtons()
        {
            HtmlContainerControl navDiv = (HtmlContainerControl)this.Master.FindControl("nav");
            navDiv.Visible = false;
        }
    }
}