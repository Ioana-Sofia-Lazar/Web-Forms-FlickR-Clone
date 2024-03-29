﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Photos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        string username = Utils.getCurrentUsername();
        string query = @"SELECT a.title album_title, p.id photo_id, p.title photo_title, p.description, p.image, u.name, c.title category 
                         FROM photo p 
                            LEFT JOIN album a ON (a.id = p.album_id) 
                            JOIN user_info u ON (u.username = a.username)  
                            JOIN category c ON (p.category_id = c.id) 
                         WHERE u.username = @username";

        // load photos for this album
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
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

    protected void btnAddPhoto_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddPhoto.aspx");
    }
}