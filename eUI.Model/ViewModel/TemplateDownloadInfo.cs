using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class TemplateDownloadInfo : baseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public string Picture { get; set; }
        public int DownloadNum { get; set; }
        public string TemplateFile { get; set; }
    }
}
