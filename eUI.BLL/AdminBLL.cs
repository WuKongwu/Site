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

        public bool SavePaper(PaperInfo paperInfo)
        {
            bool b = false;
            if (paperInfo.Id>0)
            {
                b = adminDAL.UpdatePaper(paperInfo);
            }
            else
            {
                b = adminDAL.Input(paperInfo); 
            }
            return b ;
        }
        public PaperInfo GetPaperById(int id)
        {
            return adminDAL.GetPaperId(id).toList<PaperInfo>().FirstOrDefault();
        }

        public bool DetelePaper(int id)
        {
           return adminDAL.DetelePaper(id);
        }
    }
}
