using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class ImageManageViewModel:baseModel
    {
        public string ImageName { get; set; }
        public string ImagePosition { get; set; }
        public string ImageURL { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
