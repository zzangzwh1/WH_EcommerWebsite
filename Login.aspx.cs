using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Diagnostics;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _mWizard.SetActiveView(_vLogin);
        }

    }


    protected void _signUp_Click(object sender, EventArgs e)
    {
        _mWizard.SetActiveView(_vSignUP);
    }
   
    protected void _SuccessSingIn_Click(object sender, EventArgs e)
    {
        //using regix length of id ? password restriction? 
      

        bool singup = EcommerceRegister.Registeration(_SfirstName.Text, _SlastName.Text, _Spassword.Text, _Saddress.Text, _SphoneNum.Text, _SpostCode.Text, 'u', _Semail.Text, _SuserID.Text);

        if(!singup)
        {
            Response.Write("<Script>alert('Username already exist!!')</Script>");
            Close();
        }
        else
        {
            Response.Write("<Script>alert('Succesful to Sign up')</Script>");
            Close();
            _mWizard.SetActiveView(_vLogin);
        }
    }
    public void Close()
    {
        _SfirstName.Text = "";
        _SlastName.Text = "";
        _Spassword.Text = "";
        _SconfirmPassword.Text = "";
        _Saddress.Text = "";
        _SphoneNum.Text = "";
        _SpostCode.Text = "";
        _Semail.Text = "";
        _SuserID.Text = "";
    }


    protected void _Llogin_Click(object sender, EventArgs e)
    {
        var login = EcommerceRegister.LogIn(_LloginID.Text, _Lpassword.Text);
        _lbl_failLogin.Text = "";
        bool blogIn = login.Item1;
        if (!blogIn)
        {
            _LloginID.Text = "";
            _Lpassword.Text = "";
            _lbl_failLogin.Text = "Input  Invalid ID or Password";
         
            _lbl_failLogin.ForeColor = Color.Red;
        }
        else
        {
            Session["user"] = login.Item2;
            Response.Redirect("Default.aspx");
        }
    }
}
