using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class Paper:baseModel
    {
        public virtual string Title { get; set; }
        public virtual string Info { get; set; }
        public virtual string DetailInfo { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int Type { get; set; }
        public virtual string ImgUrl { get; set; }
        public virtual string VUrl { get; set; }
    }
}
