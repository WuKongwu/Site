using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class PaperListViewModel
    {
        public PaperInfoList paperInfoList { get; set; }

        public List<PaperList> randomList { get; set; }
        public List<PaperList> HotList { get; set; }

        public ToolDownloadList paperToolPage { get; set; }
        public TemplateDownload paperTmpPage { get; set; }

        public List<FootLinkViewModel> footLinkList { get; set; }

        public List<MenuModel> menuModelList { get; set; }

    }
}
