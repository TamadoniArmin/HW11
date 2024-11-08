using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
//using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlTypes;
using System.Security.Principal;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;


namespace HW_11
{
    public class DapperRopasitory
    {
        string connectionstrting = @"Data Source=DESKTOP-6RE5DJR\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=True;TrustServerCertificate=True;";
        public bool AddProduct(string modelname, int price,int categoryId)
        {
            try
            {
                using (var cn = new SqlConnection(connectionstrting))
                {
                    string sql = $"select * from ShopDb.dbo.Products where Name=@name";
                    var command = new CommandDefinition(sql, new { name = modelname });
                    var result = cn.QueryFirstOrDefault<Products>(command);
                    if (result is not null)
                    {
                        Console.WriteLine("This username is alrady taken");
                        return false;
                    }
                    else
                    {
                        string newsql = "INSERT INTO ShopDb.dbo.Products(Name, Price,CategoryId) VALUES (@N,@P,@C)";
                        var newcommand = new CommandDefinition(newsql, new { N = modelname, P = price, C = categoryId });
                        cn.Execute(newcommand);
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong! Please try again");
            }

        }
        public List<Products> GetAllProducts()
        {
            using var cn = new SqlConnection(connectionstrting);
            var sql = $"select p.name as product_name, c.name as category_name from ShopDb.dbo.Products p join ShopDb.dbo.Categories c on p.CategoryId=c.Id";
            var cmd = new CommandDefinition(sql);
            var result = cn.Query<Products>(cmd);
            return result.ToList();
        }
        public List<Products> Search(int productId)
        {
            string sql = $"select * from ShopDb.dbo.Products where Id=@productId";
            using var cn = new SqlConnection(connectionstrting);
            var command = new CommandDefinition(sql, new { productId = productId });
            var result = cn.Query<Products>(command);
            return result.ToList();
        }
        public bool EditProductInfo(int productId, int price, string newname,int categoryid,int option)
        {
            try
            {
                string sql = $"select * from ShopDb.dbo.Products where Id = @id ";
                using (var cn = new SqlConnection(connectionstrting))
                {
                    var command = new CommandDefinition(sql, new { id = productId });
                    var result = cn.QueryFirstOrDefault<string>(command);
                    if (result is null)
                    {
                        Console.WriteLine("There is no product with this information");
                        return false;
                    }
                    else
                    {
                        if (option==1)
                        {
                            string newsql = $"Update ShopDb.dbo.Products set Price = @newprice where Id = @id";
                            var Updatecommand = new CommandDefinition(newsql, new { newprice = price,id = productId});
                            cn.Execute(Updatecommand);
                            return true;
                        }
                        else if (option==2)
                        {
                            string newsql = $"Update ShopDb.dbo.Products set Name = @newname where Id = @id";
                            var Updatecommand = new CommandDefinition(newsql, new { newname = newname , id = productId });
                            cn.Execute(Updatecommand);
                            return true;
                        }
                        else
                        {
                            string newsql = $"Update ShopDb.dbo.Products set CategoryId = @newcategoryid where Id = @id";
                            var Updatecommand = new CommandDefinition(newsql, new { newcategoryid = categoryid, id = productId });
                            cn.Execute(Updatecommand);
                            return true;
                        }
                        
                    }

                }

            }
            catch (Exception)
            {

                throw new Exception("Oop.... Something went wrong!");
            }
        }
        public bool DeleteProduct(int productId)
        {
            try
            {
                string sql = $"select * from ShopDb.dbo.Products where Id = @id ";
                using (var cn = new SqlConnection(connectionstrting))
                {
                    var command = new CommandDefinition(sql, new { id = productId });
                    var result = cn.QueryFirstOrDefault<string>(command);
                    if (result is null)
                    {
                        Console.WriteLine("There is no product with this information");
                        return false;
                    }
                    else
                    {
                            string newsql = $"delete from ShopDb.dbo.Products where Id = @id";
                            var Updatecommand = new CommandDefinition(newsql, new { id = productId });
                            cn.Execute(Updatecommand);
                            return true;
                    }

                }

            }
            catch (Exception)
            {

                throw new Exception("Oop.... Something went wrong!");
            }
        }
 //Queries:
// --(1)-----------------------------------------------------------------------------
//--select* from ShopDb.dbo.Products where Price >=500;
//--(2)-----------------------------------------------------------------------------
//--select YEAR(orderdate) as Order_year,SUM(totalamount) as total_amount from ShopDb.dbo.Orders group by YEAR(OrderDate) order by Order_year
//--(3)-----------------------------------------------------------------------------
//--select c.name as category_name, SUM(TotalAmount) as total_amount
//--from ShopDb.dbo.Orders o 
//--join ShopDb.dbo.Products p on o.ProductId = p.id
//--join ShopDb.dbo.Categories c on p.CategoryId = c.id
//--group by c.name
//--(4)-----------------------------------------------------------------------------
//--select* from ShopDb.dbo.Products order by Price desc
//--(5)-----------------------------------------------------------------------------
//--select customerId, COUNT(*) as Order_count from ShopDb.dbo.Orders group by CustomerId order by Order_count desc
//--(6)-----------------------------------------------------------------------------
//--select CategoryId, AVG(price) as avrageprice from ShopDb.dbo.Products group by CategoryId order by CategoryId
//--(7)-----------------------------------------------------------------------------
//--select Products.Name, Categories.Name from ShopDb.dbo.Products inner join ShopDb.dbo.Categories on Products.CategoryId= Categories.Id
//--(8)---------------------------------------------------------------------------- -
//--select c.name as category_name, SUM(TotalAmount) as total_amount
//--from ShopDb.dbo.Orders o 
//--join ShopDb.dbo.Products p on o.ProductId = p.id
//--join ShopDb.dbo.Categories c on p.CategoryId = c.id
//--where YEAR(OrderDate)= 2023
//--group by c.name
//--order by total_amount desc
//--(9)-----------------------------------------------------------------------------
//--select count(*) as order_count, MONTH(orderdate) as order_month from ShopDb.dbo.Orders group by OrderDate order by order_month
//--(10)----------------------------------------------------------------------------
//--select c.Name as customer_name, SUM(Totalamount) as total_amount
//--from ShopDb.dbo.Orders
//--join ShopDb.dbo.Customers c on CustomerId = c.Id
//--group by c.Name
//--(11)----------------------------------------------------------------------------
//--select c.Name as category_name, COUNT(*) as category_count
//--from ShopDb.dbo.Orders o
//--join ShopDb.dbo.Products p on o.ProductId = p.id
//--join ShopDb.dbo.Categories c on p.CategoryId = c.id
//--group by c.Name
//--(12)----------------------------------------------------------------------------
//--select c.Name as customer_name, COUNT(*) as customer_count
//--from ShopDb.dbo.Orders o
//--join ShopDb.dbo.Customers c on o.CustomerId= c.Id
//--group by c.Name
//--order by customer_count desc
//--(13)----------------------------------------------------------------------------
//--select YEAR(orderdate) as order_year, COUNT(*) as year_count
//--from ShopDb.dbo.Orders
//--group by YEAR(OrderDate)
//--(14)----------------------------------------------------------------------------
//--select p.Name as product_name, COUNT(*) as customer_count
//--from ShopDb.dbo.Orders o
//--join ShopDb.dbo.Products p on o.ProductId = p.id
//--join ShopDb.dbo.Categories c on p.CategoryId = c.id
//--group by p.Name
//--order by customer_count desc
    }
}
