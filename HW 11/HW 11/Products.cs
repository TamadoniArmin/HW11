using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_11
{
    public class Products
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string Categoryname { get; set; }

        public override string ToString()
        {
            return $"id={Id} , Name={Name} , price = {Price} , Categoryname={Categoryname}";
        }
        public Products()
        {

        }
    }
}
