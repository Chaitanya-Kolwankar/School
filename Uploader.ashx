<%@ WebHandler Language="C#" Class="Uploader" %>

using System;
using System.Web;

public class Uploader : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string id = context.Request["ID"];
        string[] arr = id.Split('?');

        if (arr[1].ToString() == "Photo")
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                string[] ext = files[i].FileName.Split('.');

                string fname = context.Server.MapPath("~/Images/Staff/" + arr[0].ToString() + "_photo" + ".jpg" );

                string path = "Images/Staff/" + arr[0].ToString() + "_photo.jpg";
                classWebMethods cls = new classWebMethods();
                cls.uploadphoto(path, arr[0].ToString());
                
                file.SaveAs(fname);
            }
        }

        if (arr[1].ToString() == "Sign")
        {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                string[] ext = files[i].FileName.Split('.');

                string fname = context.Server.MapPath("~/Images/Staff/" + arr[0].ToString() + "_sign" + ".jpg");

                string path = "Images/Staff/" + arr[0].ToString() + "_sign.jpg";
                classWebMethods cls = new classWebMethods();
                cls.uploadsign(path, arr[0].ToString());
                
                file.SaveAs(fname);
            }
        }

        context.Response.ContentType = "text/plain";
        context.Response.Write("File Uploaded Successfully!");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}