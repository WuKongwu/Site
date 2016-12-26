using eUI.Common;
using eUI.DAL;
using eUI.Model;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.BLL
{
    public class SubPageBLL
    {
        public PaperSubPageList SearchTemplate()
        {
            SubPageDAL subPageDAL = new SubPageDAL();
            PaperSubPageList paperSubPageList = new PaperSubPageList();
            DataTable dt = subPageDAL.SearchSubPageDAL();
            paperSubPageList.rows = dt.toList<PaperSubPage>();
            paperSubPageList.rows = paperSubPageList.rows.Take(1).ToList<PaperSubPage>();
            paperSubPageList.total = 1;
            return paperSubPageList;
        }
        public PaperSubPageList SearchTemplateById(string id)
        {
            SubPageDAL subPageDAL = new SubPageDAL();
            PaperSubPageList paperSubPageList = new PaperSubPageList();
            DataTable dt = subPageDAL.SearchSubPageByIdDAL(id);
            paperSubPageList.rows = dt.toList<PaperSubPage>();
            paperSubPageList.rows = paperSubPageList.rows.Take(1).ToList<PaperSubPage>();
            paperSubPageList.total = 1;
            return paperSubPageList;
        }
        public bool SaveTemplate(PaperSubPage paperSubPage)
        {
           
            bool b = false;
            SubPageDAL subPageDAL = new SubPageDAL();
            if (paperSubPage.id > 0)
            {
                b = subPageDAL.UpdateTemplate(paperSubPage);
            }
            return b;
        }

        private string ReplaceTheHtmlTag(string htmlStr)
        {
            if (!string.IsNullOrEmpty(htmlStr))
            {
                htmlStr = htmlStr.Replace("'<'", "<");
                htmlStr = htmlStr.Replace("'>'", ">");
            }
            return htmlStr;
        }
    }
}
