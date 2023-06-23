using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progPart2.Models
{
    public partial class Users
    {
        public Users()
        {
            Sales = new HashSet<Sales>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public int? Level { get; set; }



        public virtual ICollection<Sales> Sales { get; set; }
    }
}
