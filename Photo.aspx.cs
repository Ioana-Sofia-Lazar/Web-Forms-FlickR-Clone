using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Photo : System.Web.UI.Page
{
    private string username;
    private string photoId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        if (string.IsNullOrEmpty(Request.QueryString["photo_id"].ToString()))
        {
            Response.Redirect("~/Photos.aspx");
        }
        photoId = Request.QueryString["photo_id"].ToString();

        displayPhotoDetails();
        displayCommentOption();
        displayComments();
    }

    protected void displayCommentOption() 
    {
        string username = Utils.getCurrentUsername();

        // load photo for logged in user
        loggedUserAvatar.Style.Add("background-image", "Handlers/PhotoHandler.ashx?username=" + username + "&type=profile");
        loggedUserAvatar.DataBind();
    }

    protected void displayPhotoDetails()
    {
        // load photo details

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM photo WHERE id = @photoId", connection);
        command.Parameters.AddWithValue("@photoId", photoId);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                txtTitle.InnerText = reader["title"].ToString();
                txtDescription.InnerText = reader["description"].ToString();
            }
        }

        connection.Close();

        // load photo
        imgPhotoBig.Style.Add("background-image", "Handlers/AlbumPhotoHandler.ashx?photo_id=" + photoId);
        imgPhotoBig.DataBind();
    }

    protected void displayComments()
    {

        string query = @"SELECT c.text, c.date, u.username, u.name, u.profile_photo 
                         FROM comment c  
                            JOIN user_info u ON (u.username = c.username)  
                         WHERE c.photo_id = @photoId";

        // load photos for this album
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@photoId", photoId);
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    repeaterComments.DataSource = dt;
                    repeaterComments.DataBind();
                }
            }
        }
    }

    protected void btnAddComment_Click(object sender, EventArgs e)
    {
        string comment = commentText.Text;
        string query = "INSERT INTO comment(username, photo_id, text, date) VALUES(@username, @photoId, @text, GETDATE())";

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@photoId", photoId);
        command.Parameters.AddWithValue("@text", comment);
        command.ExecuteNonQuery();

        connection.Close();

        // reload page
        Response.Redirect(Request.RawUrl);

    }
}