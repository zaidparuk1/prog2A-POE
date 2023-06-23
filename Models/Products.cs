using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Models
{
    public partial class Products
    {
        public Products()
        {
            Sales = new HashSet<Sales>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        public byte[] ProductImage { get; set; }
        public decimal? ProductPrice { get; set; }
        public string Productcategory { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
