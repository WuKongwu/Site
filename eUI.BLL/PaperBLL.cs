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
            DataTable dtFootLink = paperDAL.SearchFootLink();
            paperInit.footLinkList = dtFootLink.toList<FootLinkViewModel>();
            DataTable dtMenu = paperDAL.SearchMenuList();
            paperInit.menuModelList = dtMenu.toList<MenuModel>();

            DataTable dtOtherPage = paperDAL.SearchOtherPage();
            paperInit.otherPageList = dtOtherPage.toList<OtherPageInfo>();
            paperInit.otherPageList = paperInit.otherPageList.Take(4).ToList<OtherPageInfo>();
            return paperInit;
        }

        public PaperListViewModel PaperTypeList(string type,string flag)
        {
            PaperListViewModel paperListViewModel = new PaperListViewModel();
            DataTable dtTypeList = paperDAL.SearchPaperListByType(type);
            DataTable dtSearchRandomList = paperDAL.SearchRandomList(type);
            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(type);
            DataTable dtSubPage = paperDAL.SearchPaperToolPage();
            DataTable dtTmpPage = paperDAL.SearchPaperTmpPage(flag);

         
            PaperInfoList PaperInfoList = new PaperInfoList();
            TemplateDownload templateDownload = new TemplateDownload();
            ToolDownloadList toolDownloadList = new ToolDownloadList();
            paperListViewModel.paperInfoList = PaperInfoList;
            paperListViewModel.paperTmpPage = templateDownload;
            paperListViewModel.paperToolPage = toolDownloadList;
            paperListViewModel.paperInfoList.rows = dtTypeList.toList<PaperInfo>();
            paperListViewModel.randomList = dtSearchRandomList.toList<PaperList>();
            paperListViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            paperListViewModel.paperToolPage.rows = dtSubPage.toList<ToolDownloadInfo>();
            paperListViewModel.paperTmpPage.rows = dtTmpPage.toList<TemplateDownloadInfo>();

            DataTable dtMenu = paperDAL.SearchMenuList();
            paperListViewModel.menuModelList = dtMenu.toList<MenuModel>();
            DataTable dtFootLink = paperDAL.SearchFootLink();
            paperListViewModel.footLinkList = dtFootLink.toList<FootLinkViewModel>();

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
            DataTable dtFootLink = paperDAL.SearchFootLink();
            paperListViewModel.footLinkList = dtFootLink.toList<FootLinkViewModel>();
            DataTable dtMenu = paperDAL.SearchMenuList();
            paperListViewModel.menuModelList = dtMenu.toList<MenuModel>();

            return paperListViewModel;
        }


        public PaperDetailViewModel PaperDetailById(string Id,string userId)
        {
            PaperDetailViewModel paperDetailViewModel = new PaperDetailViewModel();
            DataTable dtPaperDetail = paperDAL.SearchPaperDetailById(Id, userId);
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
            DataTable dtFootLink = paperDAL.SearchFootLink();
            paperDetailViewModel.footLinkList = dtFootLink.toList<FootLinkViewModel>();
            DataTable dtMenu = paperDAL.SearchMenuList();
            paperDetailViewModel.menuModelList = dtMenu.toList<MenuModel>();
            return paperDetailViewModel;
        }

        public PaperDetailViewModel TmpDetailById(string Id) {

            PaperDetailViewModel paperDetailViewModel = new PaperDetailViewModel();
            DataTable dtPaperDetail = paperDAL.SearchTmpDetailById(Id);
            // DataTable dtImgList = paperDAL.SearchPaperImgListById(Id);


            paperDetailViewModel.paperTmpPage = dtPaperDetail.toList<TemplateDownloadInfo>();
            DataTable dtSearchPaperHotListBytype = paperDAL.SearchPaperHotListBytype(null);
            DataTable dtSearchNewList = paperDAL.SearchNewList();
            paperDetailViewModel.HotList = dtSearchPaperHotListBytype.toList<PaperList>();
            paperDetailViewModel.NewList = dtSearchNewList.toList<PaperList>();
            DataTable dtFootLink = paperDAL.SearchFootLink();
            paperDetailViewModel.footLinkList = dtFootLink.toList<FootLinkViewModel>();
            DataTable dtMenu = paperDAL.SearchMenuList();
            paperDetailViewModel.menuModelList = dtMenu.toList<MenuModel>();
            return paperDetailViewModel;
        }

        public void AddReadCount(string id)
        {
            paperDAL.AddReadCount(id);
        }

        public ImageManageList SearchImgManage()
        {
            ImageManageList imageManageList = new ImageManageList();
            DataTable dtSearchImgManage = paperDAL.SearchImgManage();
            imageManageList.rows = dtSearchImgManage.toList<ImageManageModel>();
            return imageManageList;
        }


        public bool CreateBusiness(Business business)
        {
            bool result = paperDAL.CreateBusiness(business);
            return result;
        }

        public string SearchPayGuide()
        {
            DataTable dtSearchImgManage = paperDAL.SearchSubPage();
            string contentStr = dtSearchImgManage.Rows[0]["payguide"].ToString(); ;
            return contentStr;
        }
        public string SearchCreditGuarantee()
        {
            DataTable dtSearchImgManage = paperDAL.SearchSubPage();
            string contentStr = dtSearchImgManage.Rows[0]["creditguarantee"].ToString(); ;
            return contentStr;
        }
        public string SearchAboutUs()
        {
            DataTable dtSearchImgManage = paperDAL.SearchSubPage();
            string contentStr = dtSearchImgManage.Rows[0]["aboutus"].ToString(); ;
            return contentStr;
        }
    }
}
