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
            DataTable dtAllTypeList = paperDAL.SearchAllTypeList();
            paperInit.allTypeList = dtAllTypeList.toList<PaperList>();
            paperInit.allTypeListCount = dtAllTypeList.Rows.Count;

            DataTable dtSearchNewList = paperDAL.SearchNewList();
            paperInit.newList = dtSearchNewList.toList<PaperList>();
            paperInit.newListCount = dtSearchNewList.Rows.Count;

            DataTable dtSearchRandomList = paperDAL.SearchRandomList(null);
            paperInit.randomList = dtSearchRandomList.toList<PaperList>();
            paperInit.randomListCount = dtSearchRandomList.Rows.Count;

            return paperInit;
        }

        public PaperListViewModel PaperTypeList(string type)
        {
            PaperListViewModel paperListViewModel = new PaperListViewModel();
            DataTable dtTypeList = paperDAL.SearchPaperListByType(type);
            DataTable dtSearchRandomList = paperDAL.SearchRandomList(type);
            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(type);
            PaperInfoList PaperInfoList = new PaperInfoList();
            paperListViewModel.paperInfoList = PaperInfoList;
            paperListViewModel.paperInfoList.rows = dtTypeList.toList<PaperInfo>();
            paperListViewModel.randomList = dtSearchRandomList.toList<PaperList>();
            paperListViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            return paperListViewModel;
        }

        public PaperDetailViewModel PaperDetailById(string Id)
        {
            PaperDetailViewModel paperDetailViewModel = new PaperDetailViewModel();
            DataTable dtPaperDetail = paperDAL.SearchPaperDetailById(Id);
            DataTable dtImgList = paperDAL.SearchPaperImgListById(Id);
            paperDetailViewModel.detail = dtPaperDetail.toList<PaperInfo>();
            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(paperDetailViewModel.detail[0].Type.ToString());
            DataTable dtSearchNewList = paperDAL.SearchNewList();
            paperDetailViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            paperDetailViewModel.NewList = dtSearchNewList.toList<PaperList>();
            paperDetailViewModel.imgList = dtImgList.toList<Img>();
            return paperDetailViewModel;
        }
        public void AddReadCount(string id) {
            paperDAL.AddReadCount(id);
        }
    }
}
