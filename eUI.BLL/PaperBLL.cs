using eUI.DAL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Common;
using eUI.Model.ViewModel;

namespace eUI.BLL
{
    public class PaperBLL
    {
        PaperDAL paperDAL = new PaperDAL();


        public PaperInit PaperInit()
        {
            PaperInit paperInit = new PaperInit();
            paperInit.allTypeList = paperDAL.SearchAllTypeList();
            paperInit.newList = paperDAL.SearchNewList();
            paperInit.randomList = paperDAL.SearchRandomList();
            return paperInit;
        }
    }
}
