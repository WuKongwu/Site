using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class CountRecord:baseModel
    {
         public virtual int PaperId { get; set; }
        public virtual int Type { get; set; }
        public virtual int Num { get; set; }
    }
}
