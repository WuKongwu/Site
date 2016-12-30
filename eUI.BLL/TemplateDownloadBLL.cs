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
  public  class TemplateDownloadBLL
    {
      TemplateDownloadDAL templateDownloadDAL = new TemplateDownloadDAL();
      public TemplateDownload getTemplateDownloadList(TemplateDownloadInfo templateDownloadInfo)
        {
            TemplateDownload templateDownload = new TemplateDownload();
            DataTable dt = templateDownloadDAL.getTemplateList(templateDownloadInfo);
            templateDownload.rows = dt.toList<TemplateDownloadInfo>();
            templateDownload.total = templateDownloadDAL.getTemplateDownloadCount(templateDownloadInfo);
            return templateDownload;
        }
      public TemplateTypeList getTemplateTypeList(TemplateTypeInfo templateTypeInfo)
        {
            TemplateTypeList templateTypeList = new TemplateTypeList();
            DataTable dt = templateDownloadDAL.getTmpTypeList(templateTypeInfo);
            templateTypeList.rows = dt.toList<TemplateTypeInfo>();
            templateTypeList.total = templateDownloadDAL.getTmpTypeCount(templateTypeInfo);
            return templateTypeList;
        }

      public TemplateTypeList getTemplateTypeData()
      {
          TemplateTypeList templateTypeList = new TemplateTypeList();
          DataTable dt = templateDownloadDAL.getTmpTypeData();
          templateTypeList.rows = dt.toList<TemplateTypeInfo>();
          return templateTypeList;
      }



      public bool SaveTemplate(TemplateDownloadInfo templateDownloadInfo)
        {
            bool b = false;
            if (templateDownloadInfo.Id > 0)
            {
                b = templateDownloadDAL.UpdateTemplateDownload(templateDownloadInfo);
            }
            else
            {
                b = templateDownloadDAL.InputTemplateDownload(templateDownloadInfo);
            }
            return b;
        }


      public bool SaveTmpType(TemplateTypeInfo templateTypeInfo)
        {
            bool b = false;
            if (templateTypeInfo.Id > 0)
            {
                b = templateDownloadDAL.UpdateTemplateType(templateTypeInfo);
            }
            else
            {
                b = templateDownloadDAL.InputTemplateType(templateTypeInfo);
            }
            return b;
        }


      public TemplateDownloadInfo GetTemplateById(int id)
        {
            return templateDownloadDAL.GetTemplateById(id).toList<TemplateDownloadInfo>().FirstOrDefault();
        }

        public TemplateTypeInfo GetTemplateTypeById(int id)
        {
            return templateDownloadDAL.GetTemplateTypeById(id).toList<TemplateTypeInfo>().FirstOrDefault();
        }

        public bool DeteleTemplate(int id)
        {
            return templateDownloadDAL.DeteleTemplate(id);
        }

        public bool DeteleTemplateType(int id)
        {
            return templateDownloadDAL.DeteleTemplateType(id);
        }


        public int AddDownloadNum(int id)
        {
            return templateDownloadDAL.AddDownloadNum(id);
        }
    }
}
