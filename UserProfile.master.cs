using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class UserProfile : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        string username = Utils.getCurrentUsername();

        // load user info from db
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM user_info WHERE username = @username", connection);
        command.Parameters.AddWithValue("@username",username);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                userName.InnerText = reader["name"].ToString();
            }
        }

        connection.Close();

        // load profile photo
        imgProfile.ImageUrl = "~/Handlers/PhotoHandler.ashx?username=" + username + "&type=profile";
        imgProfile.DataBind();

        // load cover photo
        divCoverPhoto.Style.Add("background-image", "Handlers/PhotoHandler.ashx?username=" + username + "&type=cover");
        divCoverPhoto.DataBind();
    }

    protected void btnEditProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EditProfile.aspx", false);
    }
}
