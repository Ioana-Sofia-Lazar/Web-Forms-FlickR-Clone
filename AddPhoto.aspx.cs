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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // send user back to viewing the same album
        if (string.IsNullOrEmpty(Request.QueryString["album_id"].ToString()))
        {
            Response.Redirect("~/Albums.aspx");
        }
        string albumId = Request.QueryString["album_id"].ToString();
        Response.Redirect("~/Album.aspx?album_id=" + albumId);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string albumId = Request.QueryString["album_id"].ToString();
        
        // info
        string title = txtTitle.Text;
        string description = txtDescription.Text;
        string category = ddlCategories.SelectedValue;
        string query = "INSERT INTO photo(title, description, image, album_id, category_id) VALUES(@title, @description, @image, @albumId, @categId)";

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
        command.Parameters.AddWithValue("@albumId", Int32.Parse(albumId));
        command.Parameters.AddWithValue("@categId", Int32.Parse(category));
        command.ExecuteNonQuery();

        connection.Close();

        Response.Redirect("~/Album.aspx?album_id=" + albumId);
    }
}