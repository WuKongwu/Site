using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
   public class UserRecordList
    {
        public int total { get; set; }
        public List<UserRecord> rows { get; set; }
    }
}
