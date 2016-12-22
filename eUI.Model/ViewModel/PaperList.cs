using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class PaperList: baseModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string DetailInfo { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public  string Number { get; set; }

        public DateTime StTime { get; set; }
        public DateTime EdTime { get; set; }
    }
}
