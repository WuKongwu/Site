using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class MenuViewModel:baseModel
    {
        public string MenuName { get; set; }
        public int Type { get; set; }
        public string ChildImageURL { get; set; }
        public int OrderIndex { get; set; }
    }
}
