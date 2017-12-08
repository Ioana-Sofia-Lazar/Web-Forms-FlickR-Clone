using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

public partial class EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            return;

        // load user info from the db
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM user_info WHERE username = @username", connection);
        command.Parameters.AddWithValue("@username", Session["username"]);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                txtName.Text = reader["name"].ToString();
                txtOccupation.Text = reader["occupation"].ToString();
                txtCountry.Text = reader["country"].ToString();
                txtWebsite.Text = reader["website"].ToString();
            }
        }

        connection.Close();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/About.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // info
        string name = txtName.Text;
        string occupation = txtOccupation.Text;
        string country = txtCountry.Text;
        string website = txtWebsite.Text;  

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("UPDATE user_info SET name = @name, occupation = @occupation, country = @country, website = @website, profile_photo = @profilePhoto, cover_photo = @coverPhoto WHERE username = @username", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@occupation", occupation);
        command.Parameters.AddWithValue("@country", country);
        command.Parameters.AddWithValue("@website", website);
        command.Parameters.AddWithValue("@username", Session["username"]);
        command.ExecuteNonQuery();

        // profile photo
        if (profileFileUp.HasFile)
        {
            byte[] profilePhoto = ReadFile(profileFileUp.PostedFile.FileName);
            command = new SqlCommand("UPDATE user_info SET profile_photo = @profilePhoto WHERE username = @username", connection);
            command.Parameters.AddWithValue("@profilePhoto", (object)profilePhoto);
            command.Parameters.AddWithValue("@username", Session["username"]);
            command.ExecuteNonQuery();
        }

        // cover photo
        if (profileFileUp.HasFile)
        {
            byte[] coverPhoto = ReadFile(coverFileUp.PostedFile.FileName);
            command = new SqlCommand("UPDATE user_info SET cover_photo = @coverPhoto WHERE username = @username", connection);
            command.Parameters.AddWithValue("@coverPhoto", (object)coverPhoto);
            command.Parameters.AddWithValue("@username", Session["username"]);
            command.ExecuteNonQuery();
        }

        connection.Close();

        Response.Redirect("~/About.aspx", false);
    }

    //Open file in to a filestream and read data in a byte array.
    byte[] ReadFile(string sPath)
    {
        byte[] data = null;

        // Use FileInfo object to get file size.
        FileInfo fInfo = new FileInfo(sPath);
        long numBytes = fInfo.Length;

        // Open FileStream to read file
        FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

        // Use BinaryReader to read file stream into byte array.
        BinaryReader br = new BinaryReader(fStream);

        data = br.ReadBytes((int)numBytes);

        return data;
    }
}