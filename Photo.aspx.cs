using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

public partial class Photo : System.Web.UI.Page
{
    private string username;
    private string photoId;

    protected void Page_Load(object sender, EventArgs e)
    {
        // admin and owner of the photo can delete this photo (others cannot)
        if (!(Utils.getCurrentUserType().Equals("admin") || Utils.getCurrentUsername().Equals(getPhotoOwner()))) 
            linkDeletePhoto.Visible = false;

        if (Page.IsPostBack) return;

        // if there is an error with the url parameter
        if (Request.QueryString["photo_id"] == null || string.IsNullOrEmpty(Request.QueryString["photo_id"].ToString()))
        {
            Response.Redirect("~/Photos.aspx");
        }

        photoId = Request.QueryString["photo_id"].ToString();
        username = Utils.getCurrentUsername();

        linkDeletePhoto.NavigateUrl = "?photo_id=" + photoId + "&delete_photo=" + photoId;

        // comment request has been sent so show alert
        if (Request.QueryString["show_alert"] != null)
        {
            commentAlert.Visible = true;
        }
        
        // display elements on page
        displayPhotoDetails();
        displayCommentOption();
        displayComments();

        // delete comment
        if (Request.QueryString["delete"] != null) 
        {
            string commentId = Request.QueryString["delete"];
            deleteComment(commentId);
        }

        // delete photo
        if (Request.QueryString["delete_photo"] != null)
        {
            deletePhoto(photoId);
        }

    }

    protected void displayCommentOption() 
    {
        string username = Utils.getCurrentUsername();

        // load photo for logged in user
        loggedUserAvatar.Style.Add("background-image", "Handlers/PhotoHandler.ashx?username=" + username + "&type=profile");
        loggedUserAvatar.DataBind();
    }

    protected void deleteComment(string id)
    {
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();
        SqlCommand command = new SqlCommand("DELETE FROM comment WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", Int32.Parse(id));
        command.ExecuteNonQuery();
        connection.Close();

        // reload page
        photoId = Request.QueryString["photo_id"].ToString();
        Response.Redirect("~/Photo.aspx?photo_id=" + photoId);
    }

    protected void deletePhoto(string id)
    {
        // delete comments for this photo
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();
        SqlCommand command = new SqlCommand("DELETE FROM comment WHERE photo_id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();

        // delete photo
        command = new SqlCommand("DELETE FROM photo WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
        command.Dispose();
        connection.Close();

        // reload page
        Response.Redirect("~/PhotoDeleted.aspx");
    }

    protected void displayPhotoDetails()
    {
        // load photo details
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        string query = @"SELECT p.*, u.name, c.title categ_title    
                         FROM photo p 
                            JOIN album a ON (p.album_id = a.id) 
                            JOIN user_info u ON (a.username = u.username) 
                            JOIN category c on (c.id = p.category_id)
                         WHERE p.id = @photoId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@photoId", photoId);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                txtTitle.InnerText = reader["title"].ToString();
                txtDescription.InnerText = reader["description"].ToString();
                txtAuthor.InnerText += reader["name"].ToString();
                txtCategory.InnerText += reader["categ_title"].ToString();
            }
        }
        command.Dispose();
        connection.Close();

        // load photo
        imgPhotoBig.Style.Add("background-image", "Handlers/AlbumPhotoHandler.ashx?photo_id=" + photoId);
        imgPhotoBig.DataBind();
    }

    protected void displayComments()
    {
        string query = @"SELECT c.id commentId, c.text, c.date, u.username, u.name, u.profile_photo, c.photo_id photoId  
                         FROM comment c  
                            JOIN user_info u ON (u.username = c.username)  
                         WHERE c.photo_id = @photoId AND accepted = 1 
                         ORDER BY date DESC";

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
        photoId = Request.QueryString["photo_id"].ToString();
        username = Utils.getCurrentUsername();

        // if the photo belongs to the user that adds the comment, the comment is accepted by default
        string photoOwner = getPhotoOwner();

        int accepted = 0;
        if (photoOwner.Equals(username)) accepted = 1;

        // add comment
        string comment = commentText.Text;
        string query = "INSERT INTO comment(username, photo_id, text, date, accepted) VALUES(@username, @photoId, @text, GETDATE(), @accepted)";

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@photoId", photoId);
        command.Parameters.AddWithValue("@text", comment);
        command.Parameters.AddWithValue("@accepted", accepted);
        command.ExecuteNonQuery();

        command.Dispose();
        connection.Close();

        // reload page
        if (accepted == 0)
        {
            Response.Redirect("~/Photo.aspx?photo_id=" + photoId + "&show_alert=true");
        }
        else
        {
            Response.Redirect(Request.RawUrl);
        }

    }

    protected void removerQueryString(string param)
    {
        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        // make collection editable
        isreadonly.SetValue(this.Request.QueryString, false, null);
        // remove
        this.Request.QueryString.Remove("delete");
    }

    protected string getPhotoOwner()
    {
        photoId = Request.QueryString["photo_id"].ToString();

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        string query = @"SELECT a.username 
                         FROM photo p 
                            JOIN album a ON (p.album_id = a.id) 
                         WHERE p.id = @photoId
                        ";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@photoId", photoId);

        string photoOwner = "";
        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                photoOwner = reader["username"].ToString();
            }
        }
        command.Dispose();
        connection.Close();

        return photoOwner;
    }
}