using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class PaperDetailViewModel
    {
        public List<PaperInfo> detail { get; set; }
        public List<Img> imgList { get; set; }


        public List<PaperList> NewList { get; set; }
        public List<PaperList> HotList { get; set; }
    }
}
