<%@ WebHandler Language="C#" Class="CategoryImageHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class CategoryImageHandler : IHttpHandler
{

    /*
     * Returns an SQL query according to the type of photo that is needed (profile or cover)
     */
    private string getPath(string id)
    {
        string path = "~/images/Categories/";

        path += id + ".jpeg";
        
        return path;
    }

    public void ProcessRequest (HttpContext context) {
        // get category id
        string id = context.Request.QueryString["categ_id"].ToString();

        string imgPath = getPath(id);  
        string path = HttpContext.Current.Server.MapPath(imgPath);
        object data = System.IO.File.ReadAllBytes(path);

        context.Response.BinaryWrite((byte[])data);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}