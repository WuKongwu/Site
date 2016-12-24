using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class Business : baseModel
    {
        public virtual DateTime PayDate { get; set; }
        public virtual DateTime PayDateStart { get; set; }
        public virtual DateTime PayDateEnd { get; set; }
        public virtual string OrderNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string  Version { get; set; }
    }
}
