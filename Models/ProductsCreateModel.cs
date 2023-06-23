using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Models
{
    public class ProductsCreateModel
    {


        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public IFormFile theFile { get; set; }
        public decimal? ProductPrice { get; set; }
        public string Productcategory { get; set; }
    }
}
