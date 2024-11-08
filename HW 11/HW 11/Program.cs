using HW_11;

while (true)
{
    Console.WriteLine("***** Wellcome *****");
    Console.Write("Please enter your command: ");
    var command = Console.ReadLine();
    var acc = command.ToLower().Split(" ");
    DapperRopasitory dapperRopasitory = new DapperRopasitory();
    switch (acc[0])
    {
        case "addproduct":
            var repository1 = new DapperRopasitory();

            repository1.AddProduct(int.Parse(acc[2]),acc[4], int.Parse(acc[6]), int.Parse(acc[8]));

            Console.WriteLine("Product added successfully.");
            //bool res1=dapperRopasitory.AddProduct(acc[2], Convert.ToInt32(acc[4]), Convert.ToInt32(acc[6]));
            //if (!res1)
            //{
            //    Console.WriteLine("Try again!");
            //};
            break;
        case "getallproducts":
            var repository = new DapperRopasitory();
            var products = repository.GetAllProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Category ID: {product.CategoryId}");
            };
            break;
        case "search":
            var repository5 = new DapperRopasitory();

            repository5.Search(int.Parse(acc[2]));

            Console.WriteLine("Search completed.");
            break;
        case "edit":
            var repository2 = new DapperRopasitory();

            repository2.EditProductInfo(int.Parse(acc[2]), acc[4], int.Parse(acc[6]), int.Parse(acc[8]));

            Console.WriteLine("Update completed.");
            break;
        case "delet":
            var repository7 = new DapperRopasitory();

            repository7.DeleteProduct(Convert.ToInt32(acc[2]));

            Console.WriteLine("Delete operation completed.");
            break;
        default:
            break;
    }
}
