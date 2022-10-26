using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;

public partial class ProductUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void _UploadProduct_Click(object sender, EventArgs e)
    {
        if (_FileUpload1.HasFile)
        {
            // get length of uploaded file
            int length = _FileUpload1.PostedFile.ContentLength;
            byte[] img;

            using (BinaryReader _Br = new BinaryReader(_FileUpload1.PostedFile.InputStream))
            {
                // get image as bytes
                img = _Br.ReadBytes(_FileUpload1.PostedFile.ContentLength);
            }

            // read image bytes 0 to its length
            _FileUpload1.PostedFile.InputStream.Read(img, 0, length);


            bool isValid = EcommerceRegister.UploadProduct(_serialCode.Text, _pName.Text, _pClr.Text, float.Parse(_pSSize.Text), _processor.Text, _memory.Text, img, Convert.ToInt32(_capacity.Text), _brand.Text, _upload.Text, Convert.ToDecimal(_price.Text), float.Parse(_weight.Text));
            if (isValid)
            {
                Response.Write("<Script>alert('Success to upload Product!')</Script>");
                Reset();
            }
            else
            {

                Response.Write("<Script>alert('Fail to upload product')</Script>");
                Reset();
            }
        }

        // EcommerceRegister.UploadProduct()

    }
    private void Reset()
    {
        _brand.Text = "";
        _capacity.Text = "";
        _memory.Text = "";
        _pClr.Text = "";
        _pName.Text = "";
        _price.Text = "";
        _processor.Text = "";
        _pSSize.Text = "";
        _serialCode.Text = "";
        _weight.Text = "";
        _upload.Text = "";
        
      }

}