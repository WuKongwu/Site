using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
  public   class OtherPageInfo:baseModel
    {
      public string Name{get;set;}
      public string ImgFile { get; set; }
      public string Url { get; set; }
      public DateTime CreateDate { get; set; }
    }
}
