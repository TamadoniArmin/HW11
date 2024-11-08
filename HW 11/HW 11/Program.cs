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
            bool res1=dapperRopasitory.AddProduct(acc[2], Convert.ToInt32(acc[4]), Convert.ToInt32(acc[6]));
            if (!res1)
            {
                Console.WriteLine("Try again!");
            };
            break;
        case "getallproducts":
            dapperRopasitory.GetAllProducts();
            break;
        case "search":
            dapperRopasitory.Search(Convert.ToInt32(acc[2]));
            break;
        case "edit":
            bool res2 = dapperRopasitory.EditProductInfo(Convert.ToInt32(acc[2]), Convert.ToInt32(acc[4]), acc[6], Convert.ToInt32(acc[8]), Convert.ToInt32(acc[10]));
            if (!res2)
            {
                Console.WriteLine("Try again!");
            }
            break;
        case "delet":
            bool res3 = dapperRopasitory.DeleteProduct(Convert.ToInt32(acc[2]));
            if (!res3)
            {
                Console.WriteLine("Try again!");
            };
            break;
        default:
            break;
    }
}
