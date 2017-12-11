using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class AddAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddAlbum_Click(object sender, EventArgs e)
    {
        string username = Utils.getCurrentUsername();

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        string query = "INSERT INTO album(title, username) VALUES (@title, @username)";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@title", albumTitle.Text);
        command.Parameters.AddWithValue("@username", username);

        connection.Open();
        if (command.ExecuteNonQuery() > 0)
        {
            Response.Redirect("~/Albums.aspx");
        }
        connection.Close();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {        
        Response.Redirect("~/Albums.aspx");      
    }

}