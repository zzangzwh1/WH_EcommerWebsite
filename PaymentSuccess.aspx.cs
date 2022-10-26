using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PaymentSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            // assign StripeConfiguration.ApiKey as scret key 
            StripeConfiguration.ApiKey = "sk_test_51KfUlDEE8YeCCXUqpUBuBC3HuoOtSbAC8heEV8D7Uun0AxVHnYMhq5xinvnNZJd0z1cnHfzgXoss7l5M3DITurqk00uxk1vUdK";

            // create session service
            var service = new SessionService();
            // receive result of payment, QueryString["id"] from sessioncreateoptions -> SuccessUrl
            var apiResult = service.Get(Request.QueryString["id"]);
            
            decimal totalPrice = (decimal)apiResult.AmountTotal / 100;
            string shipMethod = "";
            string shipPostCode = apiResult.Shipping.Address.PostalCode;
            string shipAddr = apiResult.Shipping.Address.Line1;
            string shipCity = apiResult.Shipping.Address.City;
            string shipProvince = apiResult.Shipping.Address.State;

            // initialize shipmethod
            switch (service.Get(Request.QueryString["id"]).TotalDetails.AmountShipping / 100)
            {
                case 0:
                    shipMethod = "CanadaPost";
                    break;
                case 15:
                    shipMethod = "UPS";
                    break;
            }

            // process sequence 
            // 1. create order first => 2. insert orderitems with same orderID => 3. delete ordered items from cart
            EcommerceRegister.InsertOrders
                (Session["user"].ToString(), totalPrice, shipMethod, shipPostCode, shipAddr, shipCity, shipProvince);

            // create smtpclient to send email
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                // gmail port = 587
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    // server gmail (sender)
                    UserName = "laptopworldcmpe2965@gmail.com",
                    Password = "Cmpe2965!@",
                }
            };

            // GetUsersEmail - return user email
            string userEmail = EcommerceRegister.GetUsersEmail(Session["user"].ToString());
            // GetFullName - return user fname & lname as tuple
            Tuple<string, string> userFLname = EcommerceRegister.GetFullName(Session["user"].ToString());
            string userName = userFLname.Item1 + " " + userFLname.Item2;

            // create mailaddress instance of server side
            MailAddress FromEmail = new MailAddress("laptopworldcmpe2965@gmail.com", "Laptopworld Inc");
            MailAddress ToEmail = new MailAddress(userEmail, userName);
            MailMessage msg = new MailMessage()
            {
                From = FromEmail,
                Subject = " Purchase Receipt ",
                IsBodyHtml = true,
                Body = EcommerceRegister.CreateReceipt(Session["user"].ToString()),
            };

            msg.To.Add(ToEmail);

            try
            {
                Client.Send(msg);
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error from [SmtpClient.Send] : {err.Message}");
            }
        }
    }

    protected void e_Success_goHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void e_Success_shop_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shop.aspx");
    }
}