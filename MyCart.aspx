<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyCart.aspx.cs" Inherits="MyCart" MasterPageFile="~/MasterPage.master" Title="MyCart" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">

    <%--payment api - stripe--%>
    <script src="https://js.stripe.com/v3/"></script>
    <script type="text/javascript">

        function Payment() {
            //get masterpage form id
            var masterFormId = document.getElementById("MasterForm").id;
            // create stripe object with publishable key
            var stripe = Stripe('pk_test_51KfUlDEE8YeCCXUqmp8l1mu1iDque14zXz9rI4WPmMy2DP8GoRpFrsr40bIAM1F516ce20SkdYtNGZxkEsiidUJi00ClhXTM4F');
            // getelement using form from master page
            var form = document.getElementById(masterFormId);
            // add submit event on the master page form
            form.addEventListener('submit', function (e) {
                // prevent default submit
                e.preventDefault();
                // redirect to checkout session with stripe sessionID
                stripe.redirectToCheckout({
                    sessionId: "<%= sessionId %>"
                });
            })
        }
    </script>

    <section id="e_MyCart_Main">

        <section id="e_MyCart_hSection">
            <asp:Label Text="Shopping Cart" runat="server" ID="e_myCartLbl" CssClass="e_MyCart_hSection_lbl" />
        </section>

        <section id="e_MyCart_items">

            <asp:DataList runat="server" ID="e_MyCart_dl">
                <ItemTemplate>

                    <hr />
                    <div id="e_MyCart_div">

                        <div id="e_MyCart_items_img">
                            <asp:ImageButton ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("ProductImage")) %>' runat="server" CssClass="e_MyCart_items_img_button" />
                        </div>

                        <div id="e_MyCart_items_desc">
                            <asp:Label runat="server" Text='<%# string.Concat(new string[] {Eval("Brand").ToString(),",", Eval("ScreenSize").ToString(),",", Eval("processor").ToString(),",",Eval("Memory").ToString(),",",Eval("productName").ToString() }) %>' CssClass="e_MyCart_items_detail" />
                        </div>

                        <div id="e_MyCart_items_price">
                            <asp:Label Text='<%# string.Concat("$", Eval("price","{0:0.00}")) %>' runat="server" CssClass="e_MyCart_items_eachPrice" />
                        </div>

                        <div id="e_MyCart_items_quantity">

                            <asp:Label Text="Quantity : " runat="server" CssClass="e_MyCart_lbl_qauntity" />

                            <asp:DropDownList ID="e_MyCart_ddl_quantity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="e_MyCart_ddl_quantity_SelectedIndexChanged">
                                <asp:ListItem Text="1" Value="1" />
                                <asp:ListItem Text="2" Value="2" />
                                <asp:ListItem Text="3" Value="3" />
                                <asp:ListItem Text="4" Value="4" />
                                <asp:ListItem Text="5" Value="5" />
                                <asp:ListItem Text="6" Value="6" />
                                <asp:ListItem Text="7" Value="7" />
                                <asp:ListItem Text="8" Value="8" />
                                <asp:ListItem Text="9" Value="9" />
                            </asp:DropDownList>

                        </div>

                        <div id="e_MyCart_items_remove">
                            <asp:Button Text="Remove" runat="server" ID="e_Mycart_remove" CommandName="delete" CommandArgument='<%#Eval("ProductID") %>' OnClick="e_Mycart_remove_Click"  CssClass="e_MyCart_lbl_qRemove"/>
                        </div>

                    </div>
                    <hr />
                </ItemTemplate>



            </asp:DataList>
        </section>

        <section id="e_MyCart_buyItems">

            <asp:Label ID="e_MyCart_subTotal" Text="" runat="server" CssClass="e_MyCart_totalPrice" />
            <button type="submit" runat="server" class="e_ProductDetail_addToCartImg" onclick="Payment()" > BuyNOW</button> 

        </section>

    </section>


</asp:Content>
