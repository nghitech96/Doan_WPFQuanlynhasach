using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoRibbon
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int? Price { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        
    }
}
