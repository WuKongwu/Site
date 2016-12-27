using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eUI.Model.ViewModel
{
    public class ToolDownloadInfo : baseModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public string picture { get; set; }
        public int downloadNum { get; set; }
    }
}
