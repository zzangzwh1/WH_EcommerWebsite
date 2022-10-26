<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductUpload.aspx.cs" Inherits="ProductUpload" MasterPageFile="~/MasterPage.master" Title="ProductUpload" %>

<asp:Content ID="DefaultPage" runat="server" ContentPlaceHolderID="Main">
    <section class="e_ProductUpload">

        <section id="e_ProductUpload_serialCode">
            <asp:TextBox runat="server" ID="_serialCode" placeholder="Input ProductSerial Code" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Input ProductSerial Code" Text="Input ProductSerial Code" Display="Dynamic" ControlToValidate="_serialCode" ForeColor="Red"></asp:RequiredFieldValidator>

        </section>
        <section id="e_ProductUpload_productName">
            <asp:TextBox runat="server" ID="_pName" placeholder="Input Product Name" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Product Name" Display="Dynamic" ControlToValidate="_pName" ForeColor="Red"></asp:RequiredFieldValidator>
        </section>
        <section id="e_ProductUpload_productColor">
            <asp:TextBox runat="server" ID="_pClr" placeholder="Input Product Color" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" Text="Input Product Color" Display="Dynamic" ControlToValidate="_pClr" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                runat="server"
                ErrorMessage="Only character please...."
                ControlToValidate="_pClr"
                ValidationExpression="[a-zA-Z]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>

        </section>
        <section id="e_ProductUpload_screenSize">
            <asp:TextBox runat="server" ID="_pSSize" placeholder="Input Product Screen Size" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Screen Size" Display="Dynamic" ControlToValidate="_pSSize" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                runat="server"
                ErrorMessage="Only input number please...."
                ControlToValidate="_pSSize"
                ValidationExpression="[0-9]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>
        <section id="e_ProductUpload_processor">
            <asp:TextBox runat="server" ID="_processor" placeholder="Input Product Processor" CssClass="e_product_tBox" />

            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Processor" Display="Dynamic" ControlToValidate="_processor" ForeColor="Red"></asp:RequiredFieldValidator>
        </section>
        <section id="e_ProductUpload_Memory">
            <asp:TextBox runat="server" ID="_memory" placeholder="Input your Product Memory" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Processor" Display="Dynamic" ControlToValidate="_memory" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6"
                runat="server"
                ErrorMessage="Only input number please...."
                ControlToValidate="_memory"
                ValidationExpression="[0-9]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>

        <section id="e_ProductUpload_capacity">
            <asp:TextBox runat="server" ID="_capacity" placeholder="Input Product Capacity" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Capacity" Display="Dynamic" ControlToValidate="_capacity" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                runat="server"
                ErrorMessage="Only input number please...."
                ControlToValidate="_capacity"
                ValidationExpression="[0-9]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>

        <section id="e_ProductUpload_brand">
            <asp:TextBox runat="server" ID="_brand" placeholder="Input Product Brand" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must Input Brand Name" Display="Dynamic" ControlToValidate="_brand" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                runat="server"
                ErrorMessage="Only character please...."
                ControlToValidate="_brand"
                ValidationExpression="[a-zA-Z]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>
        <section id="e_ProductUpload_op">
            <asp:TextBox runat="server" ID="_upload" placeholder="Input Operating System" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must input Product Operating System" Display="Dynamic" ControlToValidate="_upload" ForeColor="Red"></asp:RequiredFieldValidator>

        </section>
        <section id="e_ProductUpload_price">
            <asp:TextBox runat="server" ID="_price" placeholder="Input Product Price" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must input Product Price" Display="Dynamic" ControlToValidate="_price" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                runat="server"
                ErrorMessage="Only input number please...."
                ControlToValidate="_price"
                ValidationExpression="[0-9]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>

        <section id="e_ProductUpload_weight">
            <asp:TextBox runat="server" ID="_weight" placeholder="Input Product Weight" CssClass="e_product_tBox" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="RequiredFieldValidator" Text="You Must input Product Weight" Display="Dynamic" ControlToValidate="_weight" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                runat="server"
                ErrorMessage="Only input number please...."
                ControlToValidate="_weight"
                ValidationExpression="[0-9]+"
                ForeColor="Red">
            </asp:RegularExpressionValidator>
        </section>
        <section id="e_ProductUpload_img">
            <asp:FileUpload ID="_FileUpload1" runat="server" />

             
        </section>

        <section id="e_ProductUpload_submit">
            <asp:Button Text="UpLoad Product" runat="server" ID="_UploadProduct" CssClass="e_productUpload_submit" OnClick="_UploadProduct_Click" />
        </section>
    </section>
</asp:Content>
