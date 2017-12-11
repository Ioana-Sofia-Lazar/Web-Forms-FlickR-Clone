<%@ WebHandler Language="C#" Class="AlbumPhotoHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class AlbumPhotoHandler : IHttpHandler {

    /*
     * Returns an SQL query according to the type of photo that is needed (profile or cover)
     */
    private string getQuery(string type)
    {
        string query = "SELECT ";
        
        switch(type) 
        {
            case "cover":
                query += "cover_photo FROM user_info WHERE username = @username";
                break;
            case "profile":
                query += "profile_photo FROM user_info WHERE username = @username";
                break;
            case "album_cover":
                query += "image FROM photo WHERE id = @username";
                break;
        }
        
        return query;
    }

    public void ProcessRequest (HttpContext context) {
        // get photo id sent as request parameter
        string photoId = context.Request.QueryString["photo_id"].ToString();        

        string query = "SELECT image FROM photo WHERE id = @id";
        
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", photoId);

        connection.Open();            
        object data = command.ExecuteScalar();
        connection.Close();

        if (data == null)
        {
            context.Response.Write("Images/album-cover-default.png");
        }
        else
        {
            context.Response.BinaryWrite((byte[])data);
        }
        
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}