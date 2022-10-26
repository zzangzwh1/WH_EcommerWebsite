using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Windows.Forms;

public partial class Shop : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // check if passed QueryString for BrandFilter is exist
            // if so, show products with filtered brand
            if (Request.QueryString["BrandFilter"] != null)
            {
                // retrieve the passed QueryString for SortByBrand (ex. 'APPLE', 'SAMSUNG' ... etc)
                string BrandFilterFromHeader = Request.QueryString["BrandFilter"].ToString();

                // GetDataTable - return datable
                DataList2.DataSource = EcommerceRegister.GetDataTable($"select * from Product where Brand = '{BrandFilterFromHeader}'");
            }
            else
                // if no filter passed, show all products
                DataList2.DataSource = EcommerceRegister.GetDataTable("select * from Product");

            DataList2.DataBind();
        }

    }

    protected void SortOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sQuery = "select * from Product";

        // to add or not add 'where' for query 
        bool multisort = false;

        List<string> selectedBrand = new List<string>();
        List<string> selectedColor = new List<string>();
        List<string[]> selectedScreensize = new List<string[]>();
        List<string> selectedCapacity = new List<string>();
        List<string[]> selectedPrice = new List<string[]>();

        // BRAND
        foreach (ListItem item in CB_BRAND.Items)
        {
            if (item.Selected)
                selectedBrand.Add(item.Value);
        }

        // COLOR
        foreach (ListItem item in CB_COLOR.Items)
        {
            if (item.Selected)
                selectedColor.Add(item.Value);
        }

        // SCREENSIZE
        foreach (ListItem item in CB_SCREENSIZE.Items)
        {
            if (item.Selected)
                selectedScreensize.Add(item.Value.Split(','));
        }

        // CAPACITY
        foreach (ListItem item in CB_CAPACITY.Items)
        {
            if (item.Selected)
                selectedCapacity.Add(item.Value);
        }

        // PRICE
        foreach (ListItem item in CB_PRICE.Items)
        {
            if (item.Selected)
                selectedPrice.Add(item.Value.Split(','));
        }

        // add where clause for query with all selected brand
        if (selectedBrand.Count > 0)
        {
            if (selectedBrand.Count == 1)
                sQuery += $" where (brand = '{selectedBrand[0]}')";
            else
            {
                for (int i = 0; i < selectedBrand.Count; i++)
                {
                    if (i == 0)
                        sQuery += $" where (brand = '{selectedBrand[i]}' ";
                    else if (i == selectedBrand.Count - 1)
                        sQuery += $" or brand = '{selectedBrand[i]}') ";
                    else
                        sQuery += $" or brand = '{selectedBrand[i]}'";
                }
            }

            multisort = true;
        }

        // add where clause for query with all selected color
        if (selectedColor.Count > 0)
        {
            if (multisort)
            {
                if (selectedColor.Count == 1)
                    sQuery += $" and (color = '{selectedColor[0]}')";
                else
                {
                    for (int i = 0; i < selectedColor.Count; i++)
                    {
                        if (i == 0)
                            sQuery += $" and (color = '{selectedColor[i]}'";
                        else if (i == selectedColor.Count - 1)
                            sQuery += $" or color = '{selectedColor[i]}')";
                        else
                            sQuery += $" or color = '{selectedColor[i]}'";
                    }
                }
            }
            else
            {
                if (selectedColor.Count == 1)
                    sQuery += $" where (color = '{selectedColor[0]}')";
                else
                {
                    for (int i = 0; i < selectedColor.Count; i++)
                    {
                        if (i == 0)
                            sQuery += $" where (color = '{selectedColor[i]}'";
                        else if (i == selectedColor.Count - 1)
                            sQuery += $" or color = '{selectedColor[i]}')";
                        else
                            sQuery += $" or color = '{selectedColor[i]}'";
                    }

                }
            }
            multisort = true;
        }

        // add where clause for query with all selected screensize
        if (selectedScreensize.Count > 0)
        {
            if (multisort)
            {
                if (selectedScreensize.Count == 1)
                {
                    if (selectedScreensize[0].Count() == 1)
                    {
                        if (selectedScreensize[0][0] == "14")
                            sQuery += $" and ( screensize < {selectedScreensize[0][0]} )";
                        else // 17
                            sQuery += $" and ( screensize >= {selectedScreensize[0][0]} )";
                    }
                    else // between
                        sQuery += $" and ( screensize >= {selectedScreensize[0][0]} and screensize < {selectedScreensize[0][1]} )";
                }
                else
                {
                    for (int i = 0; i < selectedScreensize.Count; i++)
                    {
                        if (i == 0)
                        {
                            // 14
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" and ( (screensize < {selectedScreensize[i][0]} ) ";
                            else // bet
                                sQuery += $" and ( (screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][0]} ) ";
                        }
                        else if (i == selectedScreensize.Count - 1)
                        {
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" or (screensize >= {selectedScreensize[i][0]} ) )";
                            else
                                sQuery += $" or ( screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][1]} ) )";
                        }
                        else
                        {
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" or (screensize >= {selectedScreensize[i][0]} ) ";
                            else
                                sQuery += $" or ( screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][1]} ) ";
                        }
                    }

                }
            }
            else
            {
                // only one screen size option selected
                if (selectedScreensize.Count == 1)
                {
                    if (selectedScreensize[0].Count() == 1) // list -> string[] -> screen size value
                    {
                        if (selectedScreensize[0][0] == "14")
                            sQuery += $" where ( screensize < {selectedScreensize[0][0]} )";
                        else // 17
                            sQuery += $" where ( screensize >= {selectedScreensize[0][0]} )";
                    }
                    else // between
                        sQuery += $" where ( screensize >= {selectedScreensize[0][0]} and screensize < {selectedScreensize[0][1]} )";
                }
                else
                {
                    for (int i = 0; i < selectedScreensize.Count; i++)
                    {
                        if (i == 0)
                        {
                            // 14
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" where ( (screensize < {selectedScreensize[i][0]} ) ";
                            else // bet
                                sQuery += $" where ( (screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][1]} ) ";
                        }
                        else if (i == selectedScreensize.Count - 1)
                        {
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" or (screensize >= {selectedScreensize[i][0]} ) )";
                            else
                                sQuery += $" or ( screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][1]} ) )";
                        }
                        else
                        {
                            if (selectedScreensize[i].Count() == 1)
                                sQuery += $" or (screensize >= {selectedScreensize[i][0]} ) ";
                            else
                                sQuery += $" or ( screensize >= {selectedScreensize[i][0]} and screensize < {selectedScreensize[i][1]} ) ";
                        }
                    }

                }
            }
            multisort = true;
        }

        // add where clause for query with all selected capacity
        if (selectedCapacity.Count > 0)
        {
            if (multisort)
            {
                if (selectedCapacity.Count == 1)
                    sQuery += $" and (capacity = '{selectedCapacity[0]}')";
                else
                {
                    for (int i = 0; i < selectedCapacity.Count; i++)
                    {
                        if (i == 0)
                            sQuery += $" and (capacity = '{selectedCapacity[i]}'";
                        else if (i == selectedCapacity.Count - 1)
                            sQuery += $" or capacity = '{selectedCapacity[i]}')";
                        else
                            sQuery += $" or capacity = '{selectedCapacity[i]}'";
                    }
                }
            }
            
            else
            {
                if (selectedCapacity.Count == 1)
                    sQuery += $" where (capacity = '{selectedCapacity[0]}')";
                else
                {
                    for (int i = 0; i < selectedCapacity.Count; i++)
                    {
                        if (i == 0)
                            sQuery += $" where (capacity = '{selectedCapacity[i]}'";
                        else if (i == selectedCapacity.Count - 1)
                            sQuery += $" or capacity = '{selectedCapacity[i]}')";
                        else
                            sQuery += $" or capacity = '{selectedCapacity[i]}'";
                    }

                }
            }
            multisort = true;
        }

        // add where clause for query with all selected price
        if (selectedPrice.Count > 0)
        {
            if (multisort)
            {
                if (selectedPrice.Count == 1)
                {
                    if (selectedPrice[0].Count() == 1)
                    {
                        if (selectedPrice[0][0] == "1000")
                            sQuery += $" and ( Price < {selectedPrice[0][0]} )";
                        else // 2000
                            sQuery += $" and ( Price >= {selectedPrice[0][0]} )";
                    }
                    else // between
                        sQuery += $" and ( Price >= {selectedPrice[0][0]} and Price < {selectedPrice[0][1]} )";
                }
                else
                {
                    for (int i = 0; i < selectedPrice.Count; i++)
                    {
                        if (i == 0)
                        {
                            // 1000
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" and ( (Price < {selectedPrice[i][0]} ) ";
                            else // bet
                                sQuery += $" and ( (Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) ";
                        }
                        else if (i == selectedPrice.Count - 1)
                        {
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} ) )";
                            else
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) )";
                        }
                        else
                        {
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} ) ";
                            else
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) ";
                        }
                    }

                }
            }
            else
            {
                // only one screen size option selected
                if (selectedPrice.Count == 1)
                {
                    if (selectedPrice[0].Count() == 1) // list -> string[] -> screen size value
                    {
                        if (selectedPrice[0][0] == "1000")
                            sQuery += $" where ( Price < {selectedPrice[0][0]} )";
                        else // 2000
                            sQuery += $" where ( Price >= {selectedPrice[0][0]} )";
                    }
                    else // between
                        sQuery += $" where ( Price >= {selectedPrice[0][0]} and Price < {selectedPrice[0][1]} )";
                }
                else
                {
                    for (int i = 0; i < selectedPrice.Count; i++)
                    {
                        if (i == 0)
                        {
                            // 1000
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" where ( (Price < {selectedPrice[i][0]} ) ";
                            else // bet
                                sQuery += $" where ( (Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) ";
                        }
                        else if (i == selectedPrice.Count - 1)
                        {
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} ) )";
                            else
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) )";
                        }
                        else
                        {
                            if (selectedPrice[i].Count() == 1)
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} ) ";
                            else
                                sQuery += $" or ( Price >= {selectedPrice[i][0]} and Price < {selectedPrice[i][1]} ) ";
                        }
                    }
                }
            }
        }

        // for debugging
        //Debug.WriteLine(sQuery);

        DataList2.DataSource = EcommerceRegister.GetDataTable(sQuery);
        DataList2.DataBind();
    }
    protected void ProductImgClick(object sender, ImageClickEventArgs e)
    {
        // pass productID retreived from DB when product img button click
        string s = "ProductDetail.aspx?ProductID=" + ((ImageButton)sender).CommandArgument.ToString();

        // redirect to productdetail page with productid
        Response.Redirect(s, false);
    }

    protected void _eShopHide_Click(object sender, EventArgs e)
    {
        // show and hide the filter option bar for products 
        if (e_Shop_SideNav.Visible == true)
        {
            _eShopHide.Text = "Open";
            e_Shop_SideNav.Visible = false;
        }
        else
        {
            e_Shop_SideNav.Visible = true;
            _eShopHide.Text = "Hide";
        }     
    }
}