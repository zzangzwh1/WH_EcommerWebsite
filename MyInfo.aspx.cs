using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Drawing;
using System.Data;

public partial class MyInfo : System.Web.UI.Page
{
    Color disable = Color.FromArgb(233, 235, 227);
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {
         
      
            dl_userInfo.DataSource = EcommerceRegister.GetDataTable("select * from Member where username = '" + Session["user"] + "'");
            dl_userInfo.DataBind();
            for (int i = 0; i < dl_userInfo.Items.Count; ++i)
            {
                string pass = ((TextBox)dl_userInfo.Items[i].FindControl("_Spassword")).Text;
                ((TextBox)dl_userInfo.Items[i].FindControl("_SconfirmPassword")).Text = pass;

              
            }

        }
    }


    protected void _update_Click1(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox pass = (TextBox)item.FindControl("_Spassword");
       
        TextBox addr = (TextBox)item.FindControl("_SAddr");
        TextBox phone = (TextBox)item.FindControl("_SphoneNum");
        TextBox post = (TextBox)item.FindControl("_SpostCode");
        TextBox email = (TextBox)item.FindControl("_Semail");
        //string password, string address, string phone, string post, string email,string username
        string user = Session["user"].ToString();
        
        bool validUpdateUserInfo = EcommerceRegister.MyInfoUpdate(pass.Text, addr.Text, phone.Text, post.Text, email.Text,user );

        if (validUpdateUserInfo)
        {
            ResetUI(pass, addr, phone, post, email);
            Response.Write("<Script>alert('Your information is successfully edited!')</Script>");
        }
        else
        {
            Response.Write("<Script>alert('fail to edit')</Script>");
        }
      
    }

    protected void ResetUI(TextBox pass, TextBox addr, TextBox phone, TextBox post, TextBox email)
    {
        pass.Enabled = false;
        pass.BackColor = disable;
        addr.Enabled = false;
        addr.BackColor = disable;
        phone.Enabled = false;
        phone.BackColor = disable;
        post.Enabled = false;
        post.BackColor = disable;
        email.Enabled = false;
        email.BackColor = disable;
    }

    protected void UI_btn_pass_Click(object sender, EventArgs e)
    {       
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox pass = (TextBox)item.FindControl("_Spassword");
        TextBox cPass = (TextBox)item.FindControl("_SconfirmPassword");
        pass.BackColor = Color.White;
        cPass.BackColor = Color.White;    
        if(pass.Enabled == true)
        {
            pass.Enabled = false;
            cPass.Enabled = false;
            pass.BackColor = disable;
            cPass.BackColor = disable;         
        }      
        else
        {
            pass.Enabled = true;
            cPass.Enabled = true;
        }
  
    }

    protected void UI_btn_addr_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox addr = (TextBox)item.FindControl("_SAddr");
        addr.BackColor = Color.White;
        if (addr.Enabled == true)
        {
            addr.Enabled = false;
            addr.BackColor = disable;
        }
        else
        {
            addr.Enabled = true;
            addr.BackColor = Color.White;
        }
    }

    protected void UI_btn_phone_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox phone = (TextBox)item.FindControl("_SphoneNum");

        if (phone.Enabled == true)
        {
            phone.Enabled = false;
            phone.BackColor = disable;
        }
        else
        {
            phone.Enabled = true;
            phone.BackColor = Color.White;
        }
     
    }

    protected void UI_btn_post_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox post = (TextBox)item.FindControl("_SpostCode");

        if (post.Enabled == true)
        {
            post.Enabled = false;
            post.BackColor = disable;
        }
        else
        {
            post.Enabled = true;
            post.BackColor = Color.White;
        }
    }

    protected void UI_btn_email_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        TextBox email = (TextBox)item.FindControl("_Semail");

        if (email.Enabled == true)
        {
            email.Enabled = false;
            email.BackColor = disable;
        }
        else
        {
            email.Enabled = true;
            email.BackColor = Color.White;
        }
    }
}