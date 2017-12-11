using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Albums : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        string username = Utils.getCurrentUsername();
        string query = @"SELECT a.id, a.title, p.id photo_id 
                         FROM album a 
                            LEFT JOIN photo p ON p.id = (
                                                        SELECT TOP 1 p1.id FROM photo p1 
                                                        WHERE a.id = p1.album_id 
                                                        ORDER BY p1.id
                                                        ) 
                         WHERE username = @username";

        // load albums for this user
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    repeaterAlbums.DataSource = dt;
                    repeaterAlbums.DataBind();
                }
            }
        }
    }
}