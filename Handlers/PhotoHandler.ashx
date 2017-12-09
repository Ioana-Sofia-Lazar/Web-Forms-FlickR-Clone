<%@ WebHandler Language="C#" Class="PhotoHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class PhotoHandler : IHttpHandler {

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
        }
        
        return query;
    }

    public void ProcessRequest (HttpContext context) {
        // get username and type sent as request parameter
        string username = context.Request.QueryString["username"].ToString();
        string type = context.Request.QueryString["type"].ToString();

        string query = getQuery(type);
        
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        SqlCommand command = new SqlCommand(query, connection);

        command.Parameters.AddWithValue("@username", username);

        connection.Open();
        object data = command.ExecuteScalar();
        connection.Close();

        context.Response.BinaryWrite((byte[])data);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}