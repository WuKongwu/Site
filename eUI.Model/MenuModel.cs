using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class  MenuModel
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public int  Type{ get; set; }
        public string ChildImageURL { get; set; }
        public int OrderIndex { get; set; }
    }
}
