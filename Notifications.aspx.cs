using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Notifications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        displayNotifications();

        if (Request.QueryString["accept"] != null)
        {
            acceptComment(Request.QueryString["accept"].ToString());
            removerQueryString("accept");
        }
        if (Request.QueryString["decline"] != null)
        {
            declineComment(Request.QueryString["decline"].ToString());
            removerQueryString("decline");
        }
    }

    protected void acceptComment(string commentId)
    {
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();
        SqlCommand command = new SqlCommand("UPDATE comment SET accepted = 1 WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", commentId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    protected void declineComment(string commentId)
    {
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();
        SqlCommand command = new SqlCommand("DELETE FROM comment WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", commentId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    protected void displayNotifications()
    {
        string username = Utils.getCurrentUsername();
        // all comments that are not accepted and that are on a photo that belongs to the current user
        string query = @"SELECT c.id commentId, c.text, c.date, u.username fromUsername, u.name fromName, u.profile_photo fromPhoto, p.id photoId 
                         FROM comment c  
                            JOIN user_info u ON (u.username = c.username) 
                            JOIN photo p ON (p.id = c.photo_id) 
                            JOIN album a ON (p.album_id = a.id)
                            JOIN user_info o ON (o.username = a.username)  
                         WHERE o.username = @username AND accepted = 0";

        // load notifications for this user
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    repeaterNotifications.DataSource = dt;
                    repeaterNotifications.DataBind();

                    // no photos to show
                    if (dt.Rows.Count == 0)
                    {
                        message.Visible = true;
                    }
                }
            }
        }
    }

    protected void removerQueryString(string param)
    {
        var nvc = HttpUtility.ParseQueryString(Request.Url.Query);
        nvc.Remove(param);
        string url = Request.Url.AbsolutePath;
        Response.Redirect(url);
    }
}