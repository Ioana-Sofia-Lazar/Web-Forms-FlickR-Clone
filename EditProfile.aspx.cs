using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web.Security;

public partial class EditProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            return;

        string username = Utils.getCurrentUsername();

        // load user info from the db
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM user_info WHERE username = @username", connection);
        command.Parameters.AddWithValue("@username", username);

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

        // load profile photo
        imgNewProfile.ImageUrl = "~/Handlers/PhotoHandler.ashx?username=" + username + "&type=profile";
        imgNewProfile.DataBind();

        // load cover photo
        imgNewCover.ImageUrl = "~/Handlers/CoverPhotoHandler.ashx?username=" + username + "&type=cover";
        imgNewCover.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/About.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string username = Utils.getCurrentUsername();
        Debug.WriteLine("~~" + username);

        // info
        string name = txtName.Text;
        string occupation = txtOccupation.Text;
        string country = txtCountry.Text;
        string website = txtWebsite.Text;  

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("UPDATE user_info SET name = @name, occupation = @occupation, country = @country, website = @website WHERE username = @username", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@occupation", occupation);
        command.Parameters.AddWithValue("@country", country);
        command.Parameters.AddWithValue("@website", website);
        command.Parameters.AddWithValue("@username", username);
        command.ExecuteNonQuery();

        // profile photo
        if (profileFileUp.HasFile)
        {
            Stream photoStream = profileFileUp.PostedFile.InputStream;
            int photoLength = profileFileUp.PostedFile.ContentLength;
            byte[] photoData = new byte[photoLength];
            photoStream.Read(photoData, 0, photoLength);

            command = new SqlCommand("UPDATE user_info SET profile_photo = @profilePhoto WHERE username = @username", connection);
            command.Parameters.AddWithValue("@profilePhoto", photoData);
            command.Parameters.AddWithValue("@username", username);
            command.ExecuteNonQuery();
        }

        // cover photo
        if (profileFileUp.HasFile)
        {
            Stream photoStream = coverFileUp.PostedFile.InputStream;
            int photoLength = coverFileUp.PostedFile.ContentLength;
            byte[] photoData = new byte[photoLength];
            photoStream.Read(photoData, 0, photoLength);

            command = new SqlCommand("UPDATE user_info SET cover_photo = @coverPhoto WHERE username = @username", connection);
            command.Parameters.AddWithValue("@coverPhoto", photoData);
            command.Parameters.AddWithValue("@username", username);
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