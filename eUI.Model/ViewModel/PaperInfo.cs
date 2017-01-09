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

        public string Video { get; set; }
        public string ImgA { get; set; }
        public string ImgB { get; set; }
        public string ImgC { get; set; }
        public string ImgD { get; set; }
        public string ImgE { get; set; }
        public string imgPath { get; set; }
        public int ReadCount { get; set; }
        public int BuyCount { get; set; }
        public string FileUrl { get; set; }
        public string Code { get; set; }
        public string VideoZip { get; set; }
        public string Version { get; set; }
        public int PayNum { get; set; }
        public int ReadNum { get; set; }
        public int PayState { get; set; }
        public string OrderNumber { get; set; }
        public string MenuName { get; set; }
    }
}
