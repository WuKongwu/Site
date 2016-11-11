using eUI.DAL;
using eUI.Common;
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
    public class AdminBLL
    {

        adminDAL adminDAL = new adminDAL();
        public PaperInfoList getPaperList(PaperList paperList)
        {
            PaperInfoList paperInfoList = new PaperInfoList();
            DataTable dt = adminDAL.getPaperList(paperList);
            paperInfoList.rows = dt.toList<PaperInfo>();
            paperInfoList.total = dt.Rows.Count;
            return paperInfoList;
        }

        public int InputPaperInfo(PaperInfo paperInfo)
        {



            return 0;
        }
    }
}
