<%@ WebHandler Language="C#" Class="upload" %>

using System;
using System.Web;
using System.IO;
using System.Text;

public class upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/html";

        // 下面这句是最重要的，取得HttpPostedFile对象后就可以调用他的SaveAs方法了  
        HttpPostedFile imgFile = HttpContext.Current.Request.Files["imgFile"];
        string Date = DateTime.Now.ToShortDateString().ToString();
        string filePath = context.Server.MapPath("~/upload/images/" + Date + "/").ToString();
        FolderCreate(filePath); //创建文件夹

        // 取文件后缀名
        string houzui = new FileInfo(imgFile.FileName).Extension;

        // 服务器上保存的文件名称
        string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + houzui;

        // 保存文件到根目录下的upload目录中
        string savePath = context.Server.MapPath("~/upload/images/"+ filename);
        imgFile.SaveAs(savePath);

        // 插入图片到kindeditor中
        string id = context.Request["id"];
        string file_url = "../upload/images/" + filename;
        string imgTitle = context.Request["imgTitle"];
        string imgWidth = context.Request["imgWidth"];
        string imgHeight = context.Request["imgHeight"];
        string imgBorder = context.Request["imgBorder"];
        StringBuilder sb = new StringBuilder();
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<title>Insert Image</title>");
        sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
        sb.Append("</head>");
        sb.Append("<body>");
        //sb.AppendFormat("<a href='#'>{0}</a>",filename);
         sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"image\"].insert(\"" + id + "\", \"" + file_url + "\",\"" + imgTitle + "\",\"" + imgWidth + "\",\"" + imgHeight + "\",\"" + imgBorder + "\");</script>");
        sb.Append("</body>");
        sb.Append("</html>");

        context.Response.Write(sb.ToString());

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    #region 创建新的目录
    public void FolderCreate(string Path)
    {
        // 判断目标目录是否存   
        if (!Directory.Exists(Path))
            Directory.CreateDirectory(Path);
    }
    #endregion
}