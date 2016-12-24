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

            DataTable dtSearchRandomList = paperDAL.SearchPaperHotListBytype(null);
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
            DataTable dtSubPage = paperDAL.SearchPaperSubPage();
            PaperInfoList PaperInfoList = new PaperInfoList();
            paperListViewModel.paperInfoList = PaperInfoList;
            paperListViewModel.paperInfoList.rows = dtTypeList.toList<PaperInfo>();
            paperListViewModel.randomList = dtSearchRandomList.toList<PaperList>();
            paperListViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            paperListViewModel.paperSubPage = dtSubPage.toList<PaperSubPage>();
            return paperListViewModel;
        }

        public PaperListViewModel PaperSearchList(string key, string type)
        {

            PaperListViewModel paperListViewModel = new PaperListViewModel();
            DataTable dtTypeList = paperDAL.SearchPaperList(key,type);

            DataTable dtSearchRandomList = paperDAL.SearchRandomList(null);

            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(null);
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
            // DataTable dtImgList = paperDAL.SearchPaperImgListById(Id);
            List<string> imgList = new List<string>();
            if (dtPaperDetail.Rows.Count > 0)
            {
                string ImgA = dtPaperDetail.Rows[0]["ImgA"].ToString() ?? "";
                if (!string.IsNullOrEmpty(ImgA)) { imgList.Add(ImgA); }
                string ImgB = dtPaperDetail.Rows[0]["ImgB"].ToString() ?? "";
                if (!string.IsNullOrEmpty(ImgB)) { imgList.Add(ImgB); }
                string ImgC = dtPaperDetail.Rows[0]["ImgC"].ToString() ?? "";
                if (!string.IsNullOrEmpty(ImgC)) { imgList.Add(ImgC); }
                string ImgD = dtPaperDetail.Rows[0]["ImgD"].ToString() ?? "";
                if (!string.IsNullOrEmpty(ImgD)) { imgList.Add(ImgD); }
                string ImgE = dtPaperDetail.Rows[0]["ImgE"].ToString() ?? "";
                if (!string.IsNullOrEmpty(ImgE)) { imgList.Add(ImgE); }
            }
            paperDetailViewModel.detail = dtPaperDetail.toList<PaperInfo>();
            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(paperDetailViewModel.detail[0].Type.ToString());
            DataTable dtSearchNewList = paperDAL.SearchNewList();
            paperDetailViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            paperDetailViewModel.NewList = dtSearchNewList.toList<PaperList>();
            paperDetailViewModel.imgList = imgList;
            return paperDetailViewModel;
        }
        public void AddReadCount(string id)
        {
            paperDAL.AddReadCount(id);
        }
    }
}
