using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HW_11;

public class DapperRopasitory
{
    private string connectionstrting = "Data Source=DESKTOP-6RE5DJR\\SQLEXPRESS;Initial Catalog=ShopDb; User Id=sa; password=arminpooma00; TrustServerCertificate=True;Integrated Security=false;";


    public void AddProduct(int id, string name, int price, int categoryId)
    {
        using (var connection = new SqlConnection(connectionstrting))
        {
            var sql = @"INSERT INTO ShopDb.dbo.Products (Id,Name, Price, CategoryId) VALUES (@id,@Name, @Price, @CategoryId)";
            var parameters = new
            {
                Id = id,
                Name = name,
                Price = price,
                CategoryId = categoryId
            };
            var cmd = new CommandDefinition(sql, parameters);
            connection.Execute(cmd);
        }
    }



    //public List<Products> GetAllProducts()
    //{
    //    using SqlConnection cn = new SqlConnection(connectionstrting);
    //    string sql = "SELECT p.name AS product_name, c.name AS category_name FROM ShopDb.dbo.Products p JOIN ShopDb.dbo.Categories c ON p.CategoryId = c.Id";
    //    CommandDefinition cmd = new CommandDefinition(sql);
    //    IEnumerable<Products> result = cn.Query<Products>(cmd);
    //    return result.ToList();
    //}

    public List<Products> GetAllProducts()
    {
        using (SqlConnection connection = new SqlConnection(connectionstrting))
        {
            string sql = "SELECT Id, Name, Price, CategoryId FROM ShopDb.dbo.Products";
            var cmd = new CommandDefinition(sql);
            var result = connection.Query<Products>(cmd);
            return result.ToList();
        }


    }

    public void Search(int productId)
    {
        using (var connection = new SqlConnection(connectionstrting))
        {

            var sql = "SELECT * FROM ShopDb.dbo.Products WHERE Id = @Id";
            var command = new CommandDefinition(sql, new { Id = productId });
            var product = connection.QueryFirstOrDefault<Products>(command);

            if (product != null)
            {
                Console.WriteLine($"Product ID: {product.Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Category ID: {product.CategoryId}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
    }


    //public bool EditProductInfo(int productId, int price, string newname, int categoryid, int option)
    //{
    //    try
    //    {
    //        string sql = "select * from ShopDb.dbo.Products where Id = @id ";
    //        using SqlConnection cn = new SqlConnection(connectionstrting);
    //        CommandDefinition command = new CommandDefinition(sql, new
    //        {
    //            id = productId
    //        });
    //        string result = cn.QueryFirstOrDefault<string>(command);
    //        if (result == null)
    //        {
    //            Console.WriteLine("There is no product with this information");
    //            return false;
    //        }
    //        switch (option)
    //        {
    //            case 1:
    //                {
    //                    string newsql = "Update ShopDb.dbo.Products set Price = @newprice where Id = @id";
    //                    CommandDefinition Updatecommand2 = new CommandDefinition(newsql, new
    //                    {
    //                        newprice = price,
    //                        id = productId
    //                    });
    //                    cn.Execute(Updatecommand2);
    //                    return true;
    //                }
    //            case 2:
    //                {
    //                    string newsql3 = "Update ShopDb.dbo.Products set Name = @newname where Id = @id";
    //                    CommandDefinition Updatecommand3 = new CommandDefinition(newsql3, new
    //                    {
    //                        newname = newname,
    //                        id = productId
    //                    });
    //                    cn.Execute(Updatecommand3);
    //                    return true;
    //                }
    //            default:
    //                {
    //                    string newsql2 = "Update ShopDb.dbo.Products set CategoryId = @newcategoryid where Id = @id";
    //                    CommandDefinition Updatecommand = new CommandDefinition(newsql2, new
    //                    {
    //                        newcategoryid = categoryid,
    //                        id = productId
    //                    });
    //                    cn.Execute(Updatecommand);
    //                    return true;
    //                }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        throw new Exception("Oop.... Something went wrong!");
    //    }
    //}
    public void EditProductInfo(int id, string name, int price, int categoryId)
    {
        using (var connection = new SqlConnection(connectionstrting))
        {
            var sql = @"UPDATE ShopDb.dbo.Products SET Name = @Name, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id";
            var command = new CommandDefinition(sql, new { Id = id, Name = name, Price = price, CategoryId = categoryId });
            var affectedRows = connection.Execute(command);
            Console.WriteLine("Done");
        }
    }


    public void DeleteProduct(int id)
    {
        
        using (var connection = new SqlConnection(connectionstrting))
        {
            var sql = "DELETE FROM ShopDb.dbo.Products WHERE Id = @Id";
            var command = new CommandDefinition(sql, new { Id = id });
            var affectedRows = connection.Execute(command);
            Console.WriteLine("Done");
        }
    }
    //Quesries
//    --(1)-----------------------------------------------------------------------------
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
