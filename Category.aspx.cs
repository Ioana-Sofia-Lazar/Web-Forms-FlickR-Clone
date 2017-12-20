using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Category : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        // get category id from parameter
        string categId = Request.QueryString["categ_id"].ToString();

        // get category title from parameter and display it
        showCategTitle(Int32.Parse(categId));

        // load photos for this category
        string query = @"SELECT a.title album_title, p.id photo_id, p.title photo_title, p.description, p.image, u.name, c.title category 
                         FROM photo p 
                            LEFT JOIN album a ON (a.id = p.album_id) 
                            JOIN user_info u ON (u.username = a.username)  
                            JOIN category c ON (p.category_id = c.id) 
                         WHERE c.id = @categId 
                         ORDER BY p.id DESC";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@categId", categId);
                using (SqlDataAdapter sda = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);     
                    repeaterPhotos.DataSource = dt;
                    repeaterPhotos.DataBind();

                    // no photos to show
                    if (dt.Rows.Count == 0)
                    {
                        message.Text = "No photos to show in this category yet.";
                        message.Visible = true;
                    }
                }
            }
        }
        
    }

    private void showCategTitle(int id)
    {
        string query = @"SELECT title  
                         FROM category 
                         WHERE id = @categId";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@categId", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        categTitle.InnerText = reader["title"].ToString();
                    }
                }
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Categories.aspx");
    }
}