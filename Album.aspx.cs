using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Album : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (string.IsNullOrEmpty(Request.QueryString["album_id"].ToString()))
        {
            Response.Redirect("~/Albums.aspx");
        }

        displayAlbumTitle();

        string albumId = Request.QueryString["album_id"].ToString();
        string query = @"SELECT a.title album_title, p.id photo_id, p.title photo_title, p.description, p.image, u.name, c.title category 
                         FROM album a 
                            LEFT JOIN photo p ON (a.id = p.album_id) 
                            JOIN user_info u ON (u.username = a.username)  
                            JOIN category c ON (p.category_id = c.id) 
                         WHERE a.id = @id";

        // load photos for this album
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", albumId);
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    repeaterPhotos.DataSource = dt;
                    repeaterPhotos.DataBind();
                }
            }
        }
    }

    private void displayAlbumTitle()
    {
        string albumId = Request.QueryString["album_id"].ToString();
        string query = @"SELECT title  
                         FROM album 
                         WHERE id = @albumId";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@albumId", albumId);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        albumTitle.Text = reader["title"].ToString();
                    }
                }
            }
        }
    }

    protected void btnAddPhoto_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddPhoto.aspx");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Albums.aspx", false);
    }

}