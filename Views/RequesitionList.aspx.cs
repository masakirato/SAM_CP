﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_RequesitionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Unnamed3_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequesitionNew");
    }
    protected void Unnamed4_Click(object sender, EventArgs e)
    {
        Response.Redirect("RequesitionTemplateList");
    }
}