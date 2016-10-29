using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class baseModel
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreateDate { get;set;}
        public virtual string CreateUserId { get; set; }
    }
}
