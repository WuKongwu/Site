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
    public class ToolDownloadBLL
    {

        ToolDownloadDAL toolDownloadDAL = new ToolDownloadDAL();
        public ToolDownloadList getToolDownloadList(ToolDownloadInfo toolDownloadInfo)
        {
            ToolDownloadList toolDownloadList = new ToolDownloadList();
            DataTable dt = toolDownloadDAL.getToolList(toolDownloadInfo);
            toolDownloadList.rows = dt.toList<ToolDownloadInfo>();
            toolDownloadList.total = toolDownloadDAL.getToolDownloadCount(toolDownloadInfo);
            return toolDownloadList;
        }


        public bool SaveTool(ToolDownloadInfo toolDownloadInfo)
        {
            bool b = false;
            if (toolDownloadInfo.Id > 0)
            {
                b = toolDownloadDAL.UpdateTool(toolDownloadInfo);
            }
            else
            {
                b = toolDownloadDAL.Input(toolDownloadInfo);
            }
            return b;
        }
        public ToolDownloadInfo GetToolById(int id)
        {
            return toolDownloadDAL.GetToolById(id).toList<ToolDownloadInfo>().FirstOrDefault();
        }

        public bool DeteleTool(int id)
        {
            return toolDownloadDAL.DeteleTool(id);
        }

    }
}
