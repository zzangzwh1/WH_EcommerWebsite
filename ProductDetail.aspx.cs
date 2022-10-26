using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;


public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // add message to drop down list
            _eProductDetail_ddl.Items.Add("Select Quantity");

            // add quantities to drop down list
            for (int i = 1; i <= 5; ++i)
                _eProductDetail_ddl.Items.Add(i.ToString());
            
            // check if passed productID is exist
            if (Request.QueryString["ProductID"] != null)
            {
                // get product id
                string selectedProduct = Request.QueryString["ProductID"].ToString();

                // connect datasource for product details from database
                dl_productImg.DataSource = EcommerceRegister.GetDataTable($"select * from Product where productID = '{selectedProduct}'");
                dl_productSide.DataSource = EcommerceRegister.GetDataTable($"select * from Product where productID = '{selectedProduct}'");
                _dl_Review.DataSource = EcommerceRegister.GetDataTable
                    ("select * from Product p join Board b on p.productID = b.productID " +
                    $"join Member m on b.memberID = m.memberID where p.productID = '{selectedProduct}' order by b.boardDate desc ");
            }

            // bind datasources
            dl_productImg.DataBind();
            dl_productSide.DataBind();
            _dl_Review.DataBind();

            // set review scores as readonly
            for (int i = 0; i < _dl_Review.Items.Count; i++)
            {
                AjaxControlToolkit.Rating rate = (AjaxControlToolkit.Rating)_dl_Review.Items[i].FindControl("Rating2");

                if (rate != null)
                    rate.ReadOnly = true;
            }

            if (Session["user"] != null)
            {
                isValidBtn();
                review_BT.Enabled = true;
                string userFullName = EcommerceRegister.GetFullName(Session["user"].ToString()).Item1 + " " + EcommerceRegister.GetFullName(Session["user"].ToString()).Item2;
                review_username.Text = userFullName;
            }
            else
            {
                review_BT.Visible = false;
                review_TB.Visible = false;
                review_username.Visible = false;
                Rating1.Visible = false;
            }
        }

    }

    protected void isValidBtn()
    {
        for (int i = 0; i < _dl_Review.Items.Count; i++)
        {
            Button btn = (Button)_dl_Review.Items[i].FindControl("_deleteBtnForAdmin");

            // split commandargument (passed two command argument with ,)
            string[] args = btn.CommandArgument.ToString().Split(new char[] { ',' });

            // enablte delete button for only admin or user themself
            if (Session["user"].ToString() == "Admin" || args[0] == Session["user"].ToString())
                btn.Visible = true;
            else
                btn.Visible = false;
        }
    }
    protected void review_BT_Click(object sender, EventArgs e)
    {
        if (review_TB.Text == "")
        {
            Response.Write("<Script>alert('You did not input any word in review!')</Script>");
            return;
        }

        string review = $"Score : {Rating1.CurrentRating}\n"
                      + $"Review : {review_TB.Text}";

        Label2.Text = review;

        string username = Session["user"].ToString();

        // check if passed productID is exist
        if (Request.QueryString["ProductID"] != null)
        {
            string selectedProduct = Request.QueryString["ProductID"].ToString();

            // SubmitReview - return boolean for submitted review is succeeded
            bool submitReview = EcommerceRegister.SubmitReview(username, review_TB.Text, Rating1.CurrentRating, Convert.ToInt32(selectedProduct));

            if (submitReview)
            {
                Response.Write("<Script>alert('review submitted')</Script>");
                review_TB.Text = "";
            }
            else
                Response.Write("<Script>alert('fail to submit')</Script>");

            Response.Redirect("ProductDetail.aspx?ProductID=" + selectedProduct, false);
        }
        else
            Debug.WriteLine("Error from [productdetail.aspx.cs] : No proudct ID");
    }

    protected void _productAddToCatImgBtn_Click(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            // show message to user and redirect to login page
            Response.Write("<Script>alert('You must login to purchase item'); window.location='Login.aspx'</Script>");
            return;
        }

        // check if passed productID is exist
        if (Request.QueryString["ProductID"] != null)
        {
            // retrieve productID from button's commandargument
            string selectedProduct = Request.QueryString["ProductID"];

            // check if valid quantity is selected
            if (_eProductDetail_ddl.SelectedIndex != 0)
            {
                // insert item to cart
                // InsertOrUpdateCart - return boolean for insert or update cart item is succeeded
                bool addtocart =
                    EcommerceRegister.InsertOrUpdateCart
                    (Convert.ToInt32(selectedProduct),
                        Convert.ToInt32(_eProductDetail_ddl.SelectedValue),
                            Session["user"].ToString());

                // success or fail 
                if (addtocart)
                    Response.Write("<Script>alert('added item to cart')</Script>");
                else
                    Response.Write("<Script>alert('fail to add item to cart')</Script>");
            }
            else
                Response.Write("<Script>alert('Must select quantity')</Script>");

            // stay in the same product page
            Response.Redirect("ProductDetail.aspx?ProductID=" + selectedProduct, false);
        }
    }

    protected void _buyNowBt_Click(object sender, EventArgs e)
    {
        // retrieve productID from querystring
        string selectedProduct = Request.QueryString["ProductID"];

        if (Session["user"] == null)
        {
            Response.Write("<Script>alert('You must login to purchase item'); window.location = 'Login.aspx'</Script>");
            return;
        }
        if (_eProductDetail_ddl.SelectedIndex != 0)
        {
            // insert item to cart
            // InsertOrUpdateCart - return boolean for insert or update cart item is succeeded
            bool buyItem =
                EcommerceRegister.InsertOrUpdateCart
                    (Convert.ToInt32(selectedProduct),
                        Convert.ToInt32(_eProductDetail_ddl.SelectedValue),
                            Session["user"].ToString());

            if (buyItem)
                Response.Redirect("MyCart.aspx", false);
            else
                Response.Write("<Script>alert('Fail to buy product')</Script>");

        }
        else
            Response.Write("<Script>alert('Must select quantity')</Script>");
    }

    protected void _deleteBtnForAdmin_Click(object sender, EventArgs e)
    {
        // retrieve productID from button's commandargument
        string selectedProduct = Request.QueryString["ProductID"];

        for (int i = 0; i < _dl_Review.Items.Count; i++)
        {
            Button btn = (Button)_dl_Review.Items[i].FindControl("_deleteBtnForAdmin");

            // split commandargument (passed two command argument with ,)
            string[] args = btn.CommandArgument.ToString().Split(new char[] { ',' });

            // for debug
            //Debug.WriteLine("ButtonName: " + args[0]);
            //Debug.WriteLine("boardNo: " + args[1]);

            // BoardDeleteBtn - return boolean for delete content is succeeded from board table
            bool deleteContent = EcommerceRegister.BoardDeleteBtn(Convert.ToInt32(args[1]));

            if (deleteContent)
            {
                Response.Write($"<Script>alert('Review Content is successfully Deleted'); " +
                    $"window.location='ProductDetail.aspx?ProductID= {selectedProduct}'</Script>");
                return;
            }
            else
                Response.Write("<Script>alert('Fail to delete review')</Script>");
        }

    }
}