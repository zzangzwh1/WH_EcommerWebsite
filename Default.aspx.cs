using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GoSHOP_Click(object sender, EventArgs e)
    {
        // redirect to shop page when 
        Response.Redirect("Shop.aspx", false);
    }


}