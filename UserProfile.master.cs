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
        // load user info from db
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM user_info WHERE username = @username", connection);
        command.Parameters.AddWithValue("@username", Session["username"]);

        using (SqlDataReader reader = command.ExecuteReader())
        {
            // show user info in fields
            if (reader.Read())
            {
                userName.InnerText = reader["name"].ToString();
            }
        }

        connection.Close();
    }
}
