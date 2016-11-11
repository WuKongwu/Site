using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class PaperInit
    {
        public List<PaperList> allTypeList { get; set; }
       
        public List<PaperList> newList { get; set; }
        public List<PaperList> randomList { get; set; }

        public int allTypeListCount { get; set; }
        public int newListCount { get; set; }
        public int randomListCount { get; set; }
    }
}
