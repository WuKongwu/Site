using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class PaperInfo
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public string DetailInfo { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }

        public string videoPath { get; set; }
        public string imgPath { get; set; }
        public int ReadCount { get; set; }
        public int BuyCount { get; set; }

    }
}
