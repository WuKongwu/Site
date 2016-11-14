using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class UserRecord :baseModel
    {
        public virtual string UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual int Flag { get; set; }

    }
}
