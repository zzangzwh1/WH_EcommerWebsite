using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class WebUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // check if user login
            if (Session["user"] != null)
            {
                // EcommerceRegister.GetFullName - return (fname, lname) as tuple
                // show welcome message with user's fname
                _login.Text = "Welcome " + EcommerceRegister.GetFullName(Session["user"].ToString()).Item1;
                _logout.Visible = true;

                // reassign href of login label (before - Login.aspx)
                // after user log in, don't need to go to login page again
                // send to myinfo page
                _alink.HRef = "MyInfo.aspx";

                // ShowItemsInCart - return number of items in cart for log in user as int
                int iItemsInCart = EcommerceRegister.ShowItemsInCart(Session["user"].ToString());

                // only update cart count if there is any items in cart
                if(iItemsInCart > 0)
                    _cart.Text = _cart.Text + " " + iItemsInCart;


                if (EcommerceRegister.IsVendorOrAdmin(Session["user"].ToString()))
                {
                    productUpload.Visible = true;
                }
                else
                {
                    productUpload.Visible = false;
                }
            }
            // user not login yet
            else
            {
                _login.Text = "Login";
                productUpload.Visible = false;
            }
        }
    }

    protected void BrandFilterClick(object sender, EventArgs e)
    {
        // debugging purpose - check id of button
        //Debug.WriteLine(((Control)(sender)).ID);

        // get clicked brand header id
        //string s = "Shop.aspx?qrystring=" + ((Control)(sender)).ID.ToString();
        string s = "Shop.aspx?BrandFilter=" + ((Control)(sender)).ID.ToString();

        // redirect to shop page and pass header id value(brand name)
        Response.Redirect(s, false);
    }
}