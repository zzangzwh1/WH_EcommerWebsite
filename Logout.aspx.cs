using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            Session.Clear();
            Response.Write("<Script>alert('You are successfully Signed out!'); window.location = 'Default.aspx'</Script>");

        }
    }
}