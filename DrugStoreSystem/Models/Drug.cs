using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStoreSystem.Models
{
    public class Drug
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Amount { get; set; }
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public string ATX { get; set; }
        public string PharmGroup { get; set; }
        public string Form { get; set; }
        public string Storehouse { get; set; }
    }
}
