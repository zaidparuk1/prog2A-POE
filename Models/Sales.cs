using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Models
{
    public partial class Sales
    {
        public string Username { get; set; }
        public int ProductId { get; set; }
        public int? TotalSales { get; set; }

        public virtual Products Product { get; set; }
        public virtual Users UsernameNavigation { get; set; }
    }
}
