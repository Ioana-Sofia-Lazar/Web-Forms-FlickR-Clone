using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;

public partial class AddPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        populateCategories();
        populateAlbums();
    }

    protected void populateCategories()
    {
        // populate ddl with categories
        string query = "SELECT id, title FROM category";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable categs = new DataTable();
                    adapter.Fill(categs);

                    ddlCategories.DataSource = categs;
                    ddlCategories.DataTextField = "title";
                    ddlCategories.DataValueField = "id";
                    ddlCategories.DataBind();
                }
            }
        }
    }

    protected void populateAlbums()
    {
        // populate ddl with albums
        string username = Utils.getCurrentUsername();
        string query = "SELECT id, title FROM album WHERE username = @username";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable albums = new DataTable();
                    adapter.Fill(albums);

                    ddlAlbums.DataSource = albums;
                    ddlAlbums.DataTextField = "title";
                    ddlAlbums.DataValueField = "id";
                    ddlAlbums.DataBind();
                }
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {        
        // info
        string title = txtTitle.Text;
        string description = txtDescription.Text;
        string category = ddlCategories.SelectedValue;
        string album = ddlAlbums.SelectedValue;
        string query = "INSERT INTO photo(title, description, image, album_id, category_id, date) output INSERTED.ID VALUES(@title, @description, @image, @albumId, @categId, GETDATE())";

        // image
        Stream photoStream = photoFileUp.PostedFile.InputStream;
        int photoLength = photoFileUp.PostedFile.ContentLength;
        byte[] photoData = new byte[photoLength];
        photoStream.Read(photoData, 0, photoLength);

        // execute query
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@title", title);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@image", photoData);
        command.Parameters.AddWithValue("@albumId", Int32.Parse(album));
        command.Parameters.AddWithValue("@categId", Int32.Parse(category));
        int photoId = (int) command.ExecuteScalar();

        command.Dispose();
        connection.Close();

        Response.Redirect("~/PhotoAdded.aspx?album_id=" + album + "&photo_id=" + photoId);
    }
}