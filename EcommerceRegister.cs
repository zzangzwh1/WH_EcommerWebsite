using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Web.UI;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Drawing;

/// <summary>
/// Summary description for EcommerceRegister
/// </summary>
public class EcommerceRegister
{


    public EcommerceRegister()
    {}

    static string sConnection = "yoocho2965_Ecommerce";
    static Random _rnd = new Random();

    #region Header
    public static bool IsVendorOrAdmin(string username)
    {
        // query to get roleID of login user
        string sqlQuery = "select roleID "
                        + "FROM Member "
                        + $"where username = '{username}'";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();

                string roleID = "";
                bool IsVendorOrAdmin = false;

                try
                {
                    // get result from sqlcommand
                    object o = dataCommand.ExecuteScalar();
                    
                    // convert object to string
                    roleID = o.ToString();

                    // check if userID is user or not
                    // u : user, v : vendor, a : admin
                    if (roleID != "u")
                        IsVendorOrAdmin = true;
                    else
                        IsVendorOrAdmin = false;

                }
                catch (Exception err)
                {
                    Debug.Write("Error from [IsVendorOrAdmin]: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }

                return IsVendorOrAdmin;
            }
        }
    }
    #endregion

    #region login
    public static Tuple<bool, string> LogIn(string userID, string password)
    {
        string query = "select * from Member where username =@username and password =@password";
        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("username", userID);
                command.Parameters.AddWithValue("password", password);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                conn.Open();

                try
                {
                    command.ExecuteNonQuery();
                    System.Diagnostics.Debug.Write("success Login");
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.Write("Fail to execute non query from Sign up: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }

                if (dt.Rows.Count > 0)
                {// Admin , admin
                    string s = "";
                    s = (from DataRow dr in dt.Rows
                         where ((string)dr.ItemArray[9]).ToLower() == userID.ToLower()
                         select dr.ItemArray[9]).First().ToString();

                    return Tuple.Create(true, s);
                }

                return Tuple.Create(false, "");
            }
        }
    }
    #endregion

    #region SignUp Registration
    public static bool Registeration(string firstName, string lastName, string password, string address, string phoneNum, string postCode, char roleID, string email, string userName)
    {
        string sqlQuery = "INSERT INTO Member (fName, lName, password, address, phone, postCode, roleID, email, username)";
        sqlQuery += "VALUES (@fName, @lName, @password, @address, @phone, @postCode, @roleID, @email, @username)";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@fName", firstName);
                dataCommand.Parameters.AddWithValue("@lName", lastName);
                dataCommand.Parameters.AddWithValue("@password", password);
                dataCommand.Parameters.AddWithValue("@address", address);
                dataCommand.Parameters.AddWithValue("@phone", phoneNum);
                dataCommand.Parameters.AddWithValue("@postCode", postCode);
                dataCommand.Parameters.AddWithValue("@roleID", roleID);
                dataCommand.Parameters.AddWithValue("@email", email);
                dataCommand.Parameters.AddWithValue("@username", userName); // must not duplicated


                conn.Open();



                string existusernames = "select username from Member";
                using (SqlCommand data = new SqlCommand(existusernames, conn))
                {
                    using (SqlDataReader reader = data.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["username"].ToString() == userName)
                                return false;
                        }
                    }
                }

                try
                {
                    dataCommand.ExecuteNonQuery();
                    System.Diagnostics.Debug.Write("success");
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.Write("Fail to execute non query from Sign up: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
    }
    #endregion

    public static bool UploadProduct(string serialCode, string name, string color, float size, string processor, string memory, byte[] image, int capacity, string brand, string opSystem, decimal price, float weight)
    {
        string sqlQuery = " insert into product "
            + "( productSerialCode,productName,Color,ScreenSize,Processor, Memory,ProductImage,Capacity,Brand,OperatingSystem,Price,Weight) "
            + " values( @serialCode, @name, @color, @size, @processor, @memory, @image, @capacity, @brand, @opSystem, @price,@Weight)";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                // 8000 is the max size of VarBinary
                SqlParameter param = new SqlParameter("@image", SqlDbType.VarBinary, 8000);
                param.Value = image;
                dataCommand.Parameters.AddWithValue("@serialCode",serialCode);
                dataCommand.Parameters.AddWithValue("@name", name);
                dataCommand.Parameters.AddWithValue("@color", color);
                dataCommand.Parameters.AddWithValue("@size", size);
                dataCommand.Parameters.AddWithValue("@processor", processor);
                dataCommand.Parameters.AddWithValue("@memory", memory);
                dataCommand.Parameters.AddWithValue("@image", image);
                dataCommand.Parameters.AddWithValue("@capacity", capacity);
                dataCommand.Parameters.AddWithValue("@brand", brand);
                dataCommand.Parameters.AddWithValue("@opSystem", opSystem);
                dataCommand.Parameters.AddWithValue("@price", price);
                dataCommand.Parameters.AddWithValue("@Weight",weight);

                conn.Open();
                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to inserting item in cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to add item to cart: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }


        }
    }
   
    public static DataTable GetDataTable(string query)
    {

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(query, conn))
            {
                conn.Open();

                // execute sqlcommand
                dataCommand.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(dataCommand);

                // fill datatable
                da.Fill(dt);

                return dt;
            }
        }


    }
    #region Insert Data into Board
    public static bool SubmitReview(string username, string content, int grade, int productID)
    {
        //insert into Board(memberID, content, boardDate, grade, productID)
        //select memberID, 'test', GETDATE(), 3, 4
        //from Member
        //where username = 'yyoo2'

        string sqlQuery = "INSERT INTO Board(memberID, content, boardDate, grade, productID) "
                        + "SELECT memberID, @content, GETDATE(), @grade, @productID "
                        + "FROM Member "
                        + $"where username = '{username}'";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                //dataCommand.Parameters.AddWithValue("@memberID", memberID);
                dataCommand.Parameters.AddWithValue("@content", content);
                dataCommand.Parameters.AddWithValue("@grade", grade);
                dataCommand.Parameters.AddWithValue("@productID", productID);

                conn.Open();


                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to submit reveiw");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to submit reveiw: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
    }
    #endregion
    #region My information Update
    public static bool MyInfoUpdate(string password, string address, string phone, string post, string email,string username)
    {
        string sqlQuery = " update Member";
        sqlQuery += $" set password = '{password}',";
        sqlQuery += $" address = '{address}',";
        sqlQuery += $" phone = '{phone}',";
        sqlQuery += $" postCode = '{post}',";
        sqlQuery += $" email = '{email}'";
        sqlQuery += $" where username = '{username}'";
        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {             

                conn.Open();
                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to inserting item in cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to add item to cart: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
     
    }
    #endregion
    #region Cart
    public static bool InsertOrUpdateCart(int productID, int quantity, string username)
    {
        string sqlQuery = "declare @memID int ";
        sqlQuery += "SELECT @memID = memberID ";
        sqlQuery += "FROM Member ";
        sqlQuery += "where username = '" + username + "' ";
        sqlQuery += "if not exists (select * from Cart where ProductID = " + productID + " and MemberID = @memID ) ";
        sqlQuery += "   begin ";
        sqlQuery += "   insert into Cart values (@productID, @amount, @memID) ";
        sqlQuery += "   end ";
        sqlQuery += "else ";
        sqlQuery += "   update Cart ";
        sqlQuery += "   set amount = amount + @amount ";
        sqlQuery += "   where memberID = @memID and productID = @productID ";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                //dataCommand.Parameters.AddWithValue("@memberID", memberID);
                dataCommand.Parameters.AddWithValue("@productID", productID);
                dataCommand.Parameters.AddWithValue("@amount", quantity);

                conn.Open();

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to inserting item in cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to add item to cart: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
    }

    // show # of items in cart on the header
    public static int ShowItemsInCart(string username)
    {
        string sqlQuery = "select COUNT(*) ";
        sqlQuery += "FROM Cart c ";
        sqlQuery += "JOIN Member m on c.memberID = m.memberID ";
        sqlQuery += "where username = '" + username + "'";
        sqlQuery += "group by c.memberID ";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();

                int iIteminCart = 0;
                try
                {
                    object o = dataCommand.ExecuteScalar() == null ? 0 : dataCommand.ExecuteScalar();

                    iIteminCart = (int)o;
                    Debug.Write("success to inserting item in cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to add item to cart: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }

                return iIteminCart;
            }
        }
    }

    // show items in cart on MyCart.aspx.cs
    public static Dictionary<string, Dictionary<string,object>> GetItemsInfoInCart(string username)
    {
        string sqlQuery = "select p.productName, p.Price, c.amount "
                        + "FROM Cart c "
                        + "JOIN Member m on c.memberID = m.memberID "
                        + "Join Product p on p.productID = c.productID "
                        + $"where username = '{username}'";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();

                // key : productname 
                // value : dict( key : column name, value : vlaue of column)
                Dictionary<string, Dictionary<string, object>> cartItems = new Dictionary<string, Dictionary<string, object>>();

                try
                {
                    dataCommand.CommandType = CommandType.Text;
                    SqlDataReader dr = dataCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        // temp dictionary for value of main dictionary
                        Dictionary<string, object> info = new Dictionary<string, object>();
                        // key : price , value : value of price
                        info["Price"] = dr["Price"];
                        // key : amount , value : value of amount
                        info["amount"] = dr["amount"];
                        // main dict = key : productName , value : dict (price : value , amount : value)
                        cartItems[dr["productName"].ToString()] = info;
                    }

                }
                catch (Exception err)
                {
                    Debug.Write($"Error from [GetItemsInfoInCart]: {err.Message}");
                }
                finally
                {
                    conn.Close();
                }

                return cartItems;
            }
        }
    }
    #region ContentDelete
    public static bool BoardDeleteBtn(int boardNum)
    {
        // query to delete review from board table
        string sqlQuery = $" delete from Board"
                        + $" where boardNo = {boardNum}";     

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {

                conn.Open();
                try
                {
                    dataCommand.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    Debug.Write("Error from [BoardDeleteBtn]: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
   
    }
    #endregion

    // delete selected item from cart
    public static bool RemoveCartItem(int productID, string username)
    {
        string sqlQuery = "delete Cart ";
        sqlQuery += "from Cart c ";
        sqlQuery += "join Member m on m.memberID = c.memberID ";
        sqlQuery += "where productID = @ProductID and m.username = @username ";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@productID", productID);
                dataCommand.Parameters.AddWithValue("@username", username);

                conn.Open();

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to delete item from cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to delete item from cart: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
    }

    public static bool UpdateCartItemQantity(int productID, int newquantity, string username)
    {
       
        string sqlQuery = "update Cart ";
        sqlQuery += "set amount = @newquantity ";
        sqlQuery += "from Cart c ";
        sqlQuery += "join Member m on m.memberID = c.memberID ";
        sqlQuery += "where productID = @ProductID and m.username = @username ";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@productID", productID);
                dataCommand.Parameters.AddWithValue("@newquantity", newquantity);
                dataCommand.Parameters.AddWithValue("@username", username);

                conn.Open();

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to update item quantity from cart");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to update item quantity from cart: " + err.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }

                return true;
            }
        }
    }
    #endregion

    public static Tuple<string, string> GetFullName(string username)
    {
        string sqlQuery = "SELECT fName, lName ";
        sqlQuery += "FROM Member ";
        sqlQuery += "where username = '" + username + "'";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();

                string lName = "";
                string fName = "";

                try
                {
                    dataCommand.CommandType = CommandType.Text;
                    SqlDataReader dr = dataCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        lName = dr["lName"].ToString();
                        fName = dr["fName"].ToString();
                    }

                    Debug.Write("success to retrieve full name");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to retrieve full name: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }

                return new Tuple<string, string>(fName, lName);
            }
        }
    }

    #region orders
    public static void InsertOrders(string username, decimal totalprice, string shipmethod, string shipPostCode, string shipaddr
        , string shipCity, string shipProvince)
    {
        // process sequence 
        // 1. create order first => 2. insert orderitems with same orderID => 3. delete ordered items from cart

        // generate random orderNumber

        // declare @randomOrderNumber nvarchar(10) = CONVERT(nvarchar(10), LEFT(REPLACE(NEWID(), '-', ''), 10))
        // while exists((select * from Orders o where o.ordernumber = @randomOrderNumber))
        // begin
        // set @randomOrderNumber = CONVERT(nvarchar(10), LEFT(REPLACE(NEWID(), '-', ''), 10))
        // end
        // insert into Orders
        //select memberID, 1000.00
        //from Member
        //where username = 'testuser'
        string sqlQuery = "declare @randomOrderNumber nvarchar(10) = CONVERT(nvarchar(10), LEFT(REPLACE(NEWID(), '-', ''), 10)) " +
            "while exists((select * from Orders o where o.ordernumber = @randomOrderNumber)) " +
            "begin " +
            "set @randomOrderNumber = CONVERT(nvarchar(10), LEFT(REPLACE(NEWID(), '-', ''), 10)) " +
            "end " +
            "insert into Orders " +
            "select memberID, @totalprice, @randomOrderNumber " +
            "from Member " +
            "where username = @username ";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@username", username);
                dataCommand.Parameters.AddWithValue("@totalprice", totalprice);

                conn.Open();

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to insert to Orders");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to insert to Orders: " + err.Message);
                }
            }

            // declare @orderId int
            //select top 1 @orderId = o.ID
            //from Orders o
            //join Member m on m.memberID = o.memberID
            //where m.username = 'testuser'
            //order by o.ID desc

            // insert into OrderItem
            //select o.ID, p.productID, c.amount, GETDATE(), 'Free', '1336 cunningham dr', 'T6W0R8'
            //from Orders o
            //join Member m on m.memberID = o.memberID
            //join Cart c on c.memberID = m.memberID
            //join Product p on p.productID = c.productID
            //where m.username = 'testuser'

            sqlQuery =
                   " declare @orderId int " +
                   " select top 1 @orderId = o.ID " +
                   " from Orders o " +
                   " join Member m on m.memberID = o.memberID " +
                   " where m.username = @username " +
                   " order by o.ID desc " +
                   " insert into OrderItem " +
                   " select o.ID, p.productID, c.amount, GETDATE(), @shipmethod , @shipPostCode, @shipaddr, @shipCity, @shipProvince " +
                   " from Orders o " +
                   " join Member m on m.memberID = o.memberID " +
                   " join Cart c on c.memberID = m.memberID " +
                   " join Product p on p.productID = c.productID " +
                   " where m.username = @username and o.ID = @orderId";

            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@username", username);
                dataCommand.Parameters.AddWithValue("@shipmethod", shipmethod);
                dataCommand.Parameters.AddWithValue("@shipaddr", shipaddr);
                dataCommand.Parameters.AddWithValue("@shipPostCode", shipPostCode);
                dataCommand.Parameters.AddWithValue("@shipCity", shipCity);
                dataCommand.Parameters.AddWithValue("@shipProvince", shipProvince);

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to insert to OrderItems");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to insert to OrderItems: " + err.Message);
                }
            }

            sqlQuery = "delete Cart ";
            sqlQuery += "from Cart c ";
            sqlQuery += "join Member m on m.memberID = c.memberID ";
            sqlQuery += "where m.username = @username ";

            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@username", username);

                try
                {
                    dataCommand.ExecuteNonQuery();
                    Debug.Write("success to delete item from cart after order");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to delete item from cart after order: " + err.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

    public static string GetUsersEmail(string username)
    {
        string sqlQuery = "SELECT email ";
        sqlQuery += "FROM Member ";
        sqlQuery += "where username = '" + username + "'";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                conn.Open();

                string userEmail = "";

                try
                {
                    dataCommand.CommandType = CommandType.Text;
                    SqlDataReader dr = dataCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        userEmail = dr["email"].ToString();
                    }
                }
                catch (Exception err)
                {
                    Debug.Write($"Error from [GetUsersEmail]: {err.Message}");
                }
                finally
                {
                    conn.Close();
                }

                return userEmail;
            }
        }
    }
    #endregion

    #region receipt

    public static string CreateReceipt(string username)
    {
        //declare @orderId int
        //select top 1 @orderId = o.ID
        //from Orders o
        //join Member m on m.memberID = o.memberID
        //where m.username = 'testuser2'
        //order by o.ID desc

        //select oi.orderDate, oi.quantity, p.productName, p.Price, o.totalprice
        //from Orders o
        //join OrderItem oi on oi.orderID = o.ID
        //join Product p on p.productID = oi.productID
        //join Member m on m.memberID = o.memberID
        //where m.username = 'testuser2'
        string sqlQuery = "declare @orderId int " +
                          "select top 1 @orderId = o.ID " +
                          "from Orders o " +
                          "join Member m on m.memberID = o.memberID " +
                          "where m.username = @username " +
                          "order by o.ID desc " +
                          "select oi.orderDate, oi.quantity, p.productName, p.Price, o.totalprice, o.ordernumber, oi.shipMethod ,oi.shipAddress, oi.shipCity, oi.shipProvince, m.fName + ' ' + m.lName as 'name'" +
                          "from Orders o  " +
                          "join OrderItem oi on oi.orderID = o.ID " +
                          "join Product p on p.productID = oi.productID " +
                          "join Member m on m.memberID = o.memberID " +
                          "where m.username = @username and o.ID = @orderId";

        string contentToReturn = "";

        using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings[sConnection].ConnectionString))
        {
            using (SqlCommand dataCommand = new SqlCommand(sqlQuery, conn))
            {
                dataCommand.Parameters.AddWithValue("@username", username);

                conn.Open();

                string orderdate = "";
                List<string> productQuantities = new List<string>();
                List<string> productNames = new List<string>();
                List<string> productPrices = new List<string>();
                string ordernumber = "";
                string shipMethod = "";
                string shipAddress = "";
                string shipCity = "";
                string shipProvince = "";
                string totalPrice = "";
                string name = "";

                try
                {
                    dataCommand.CommandType = CommandType.Text;
                    SqlDataReader dr = dataCommand.ExecuteReader();

                    while (dr.Read())
                    {
                        orderdate = dr["orderDate"].ToString();
                        productQuantities.Add(dr["quantity"].ToString());
                        productNames.Add(dr["productName"].ToString());
                        productPrices.Add(dr["Price"].ToString());
                        ordernumber = dr["ordernumber"].ToString();
                        shipMethod = dr["shipMethod"].ToString();
                        shipAddress = dr["shipAddress"].ToString();
                        shipCity = dr["shipCity"].ToString();
                        shipProvince = dr["shipProvince"].ToString();
                        name = dr["name"].ToString();
                        totalPrice = $"${dr["totalprice"]:F2}";
                        // each product prices and total price - change to int with no decimal points
                    }

                    // create online receipt content
                    contentToReturn = ReceiptFormat(orderdate, productQuantities, productNames, productPrices, totalPrice, ordernumber, shipMethod, shipAddress, shipCity, shipProvince, name);

                    // for debug
                    //Debug.Write("success to generate email receipt");
                }
                catch (Exception err)
                {
                    Debug.Write("Fail to generate email receipt: " + err.Message);
                    return @"<html><body><pre>There was an error on generating your receipt<br>" +
                           "Please contect to our service team<br>" +
                           "Tel : 780-111-1111<br>" +
                           "E-mail : support@LaptopWorld.com<br>" +
                           "Sorry for the inconvenience</pre></body></html>";
                    // need to change website name on email addr 
                }
                finally
                {
                    conn.Close();
                }

                return contentToReturn;
            }
        }
    }

    public static string ReceiptFormat(string orderdate, List<string> productQuantities, List<string> productNames,
        List<string> productPrices, string totalPrice, string ordernumber, string shipMethod ,string shipAddress, string shipCity, string shipProvince, string name)
    {
        // c# console font and gmail font are diff
        // using html format to align text properly
        // <pre> => preformatted text which is to be presented exactly as written in the HTML file
        // String.Concat(Enumerable.Repeat("&nbsp;", N)) => add required space N times
        int widthLength = 60;
        string htmlString = $@"<html><body>
                                  <pre>{String.Concat(Enumerable.Repeat("*", widthLength))}<br>"
                           + $"**{String.Concat(Enumerable.Repeat("&nbsp;", ((widthLength - 4 - "Online Payment Receipt".Length) / 2)))}"
                           + $"Online Payment Receipt{String.Concat(Enumerable.Repeat("&nbsp;", (widthLength - 4 - "Online Payment Receipt".Length) / 2))}**<br>"
                           + $"{ String.Concat(Enumerable.Repeat("*", widthLength))}<br>"
                           + $"Deliver to :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - name.Length - "Deliver to :".Length))}{name}<br>"
                           + $"Address :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - $"{shipAddress}, {shipCity} ,{shipProvince}".Length - "Address :".Length))}{shipAddress}, {shipCity} ,{shipProvince}<br>"
                           + $"Date :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - orderdate.Length - "Date :".Length))}{orderdate}<br>"
                           + $"OrderNumber :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - $"#{ordernumber}".Length - "OrderNumber :".Length))}#{ordernumber}<br>"
                           + $"Shipping method :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - shipMethod.Length - "Shipping method :".Length))}{shipMethod}<br>"
                           + $"Order Summary :<br>";

        for (int i = 0; i < productNames.Count; i++)
        {
            double price = Convert.ToDouble(productQuantities[i]) * Convert.ToDouble(productPrices[i]);
            string CountAndName = $"{productQuantities[i]} x {productNames[i]}";
            string itemPrice = "$" + price;

            htmlString += $"{CountAndName}{String.Concat(Enumerable.Repeat("&nbsp;", 60 - CountAndName.Length - itemPrice.Length))}{itemPrice}<br>";
        }

        htmlString += $"{String.Concat(Enumerable.Repeat("*", widthLength))}<br>"
                       + $"Total :{String.Concat(Enumerable.Repeat("&nbsp;", widthLength - "Total :".Length - totalPrice.Length))}{totalPrice}<br>"
                       + $"{String.Concat(Enumerable.Repeat("*", widthLength))}<br>"
                       + $"{String.Concat(Enumerable.Repeat("&nbsp;", (widthLength - "Thanks for your order".Length) / 2))}Thanks for your order"
                       + $"{String.Concat(Enumerable.Repeat("&nbsp;", (widthLength - "Thanks for your order".Length) / 2))}<br>"
                       + $"{String.Concat(Enumerable.Repeat("&nbsp;", (widthLength - "Labtop World".Length) / 2))}Labtop World"
                       + $"{String.Concat(Enumerable.Repeat("&nbsp;", (widthLength - "Labtop World".Length) / 2))}<br>"
                       + $"{String.Concat(Enumerable.Repeat("*", widthLength))}<br>"
                       + "</pre></body></html>";

        return htmlString;
    }

    #endregion

}