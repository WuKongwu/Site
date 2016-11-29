<%@ WebHandler Language="C#" Class="UplodHandler" %>

using System;
using System.Web;
using System.IO;

public class UplodHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        HttpPostedFile file = context.Request.Files["Filedata"];

        string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]+"\\");

        if (file != null)
        {
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            string name = DateTime.Now.ToString("yyyyMMddhhmmss") + System.IO.Path.GetExtension(file.FileName).ToLower();
            file.SaveAs(uploadPath + name);

            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}