<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="About" MasterPageFile="~/MasterPage.master" Title="ProductDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">

    <%--css & js/jquery for image zoom --%> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/elevatezoom/2.2.3/jquery.elevatezoom.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".e_productImg").elevateZoom({
                cursor: 'pointer',
                imageCrossfade: true,
                loadingIcon: 'loading.gif'

            });
        }); 
    </script>

    <section id="e_ProductDetail">

        <section id="e_ProductDetail_productImg">

            <asp:DataList ID="dl_productImg" runat="server">

                <ItemTemplate>
                    <asp:Label Text='<%#Eval("ProductName")%>' runat="server" CssClass="e_productDetail_lbl_productName" /><br />
                    <asp:Image ImageUrl='<%# "data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("ProductImage")) %>' runat="server" CssClass="e_productImg" />
                    <br /><asp:Label Text='<%#string.Concat("$", Eval("price", "{0:0.00}")) %>' runat="server" CssClass="e_ProductDetail_lblPrice" />
                </ItemTemplate>

            </asp:DataList>

        </section>

        <section id="e_ProductDetail_sideBtn">

            <asp:DataList runat="server" ID="dl_productSide">

                <ItemTemplate>
                    
                    <asp:Label Text="Details & Specs" runat="server" CssClass="e_ProductDetail_textDetail" /><br />
                    <br />
                    <asp:Label Text="Brand:" runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Brand") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Color :" runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Color") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Screen Size : " runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("ScreenSize") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Processor :" runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Processor") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Memory : " runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Memory") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Capacity: " runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Capacity") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Operating System:" runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Operatingsystem") %>' runat="server" CssClass="e_productDetail_products" /><br />
                    <asp:Label Text="Weight:" runat="server" CssClass="e_productDetail_products" />
                    <asp:Label Text='<%#Eval("Weight") %>' runat="server" ID="_weight" CssClass="e_productDetail_products" />
                    <asp:Label Text="G" runat="server" CssClass="e_productDetail_products" />


                </ItemTemplate>
            </asp:DataList>

            <asp:DropDownList runat="server" ID="_eProductDetail_ddl" Width="60%" Height="150%" Font-Size="Larger" AutoPostBack="true">
            </asp:DropDownList><br />
            <br />
            <br />

            <asp:Button Text="Add To Cart" runat="server" CssClass="e_ProductDetail_addToCartImg" ID="_productAddToCatImgBtn" OnClick="_productAddToCatImgBtn_Click" />

            <asp:Button Text="Buy Now" runat="server" ID="_buyNowBt" CssClass="e_ProductDetail_buynowBTN" OnClick="_buyNowBt_Click" />
        </section>

    </section>
    <hr />

    <section id="e_ProductDetail_board">

        <div class="e_ProductDetail_review_others">

            <h1>REVIEWS</h1>
            <asp:DataList runat="server" ID="_dl_Review">

                <ItemTemplate>
                    <div id="e_ProductDetail_board_review_main">

                        <div id="review_main_name">

                            <asp:Label Text='<%# string.Concat(new string[] {Eval("fName ").ToString(), " ", Eval("lName ").ToString() }) %>' ID="lbl" runat="server" CssClass="review_writier" />
                        </div>

                        <div id="review_main_rating">

                            <cc1:Rating ID="Rating2" runat="server"
                                StarCssClass="starRating"
                                FilledStarCssClass="Filledstars"
                                WaitingStarCssClass="Watingstars"
                                EmptyStarCssClass="Emptystars"
                                CurrentRating='<%#Convert.ToInt32(Eval("grade"))%>'>
                            </cc1:Rating>
                        </div>

                        <div id="review_main_content">

                            <asp:Label Text='<%#Eval("content") %>' runat="server" CssClass="review_content"></asp:Label>
                        </div>
                        <div id="review_main_date">

                            <asp:Label Text='<%#Eval("boardDate") %>' runat="server" CssClass="review_date"></asp:Label>
                        </div>

                        <div id="review_main_btn">

                            <%--pass multiple commandargument--%>
                            <asp:Button Text="Delete" runat="server" Visible="false" ID="_deleteBtnForAdmin" CssClass="review_deleteContentBTN" 
                                CommandArgument='<%#Eval("username")+","+ Eval("boardNo")%>' OnClick="_deleteBtnForAdmin_Click"  />
                        </div>

                    </div>
                    <br />
                    <br />
                </ItemTemplate>

                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White"></SelectedItemStyle>
            </asp:DataList>

        </div>

        <div class="star_div">
            <div id="e_ProductDetail_board_result">

                <div id="e_ProductDetail_board_result_star">
                    <%--need ScriptManager to use UpdatePanel--%>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <cc1:Rating ID="Rating1" runat="server"
                                StarCssClass="starRating"
                                FilledStarCssClass="Filledstars"
                                WaitingStarCssClass="Watingstars"
                                EmptyStarCssClass="Emptystars">
                            </cc1:Rating>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div id="e_ProductDetail_board_result_name">
                    <asp:Label ID="review_username" runat="server" Text="" Font-Size="30px" CssClass="review_username"></asp:Label>

                </div>
                <div id="e_ProductDetail_board_result_writeContent">
                    <asp:TextBox ID="review_TB" runat="server" CssClass="TB_rating" placeholder="wirte review here" TextMode="MultiLine"></asp:TextBox>

                </div>


                <div id="e_ProductDetail_board_result_submitBtn">

                    <asp:Button ID="review_BT" runat="server" Text="Submit" CssClass="e_Shop_ReviewBTN" OnClick="review_BT_Click" />
                    <br />
                </div>
                <div id="e_ProductDetail_board_result_resultLbl">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>

                </div>
            </div>

        </div>


    </section>


</asp:Content>
