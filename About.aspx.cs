using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class About : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
                userDescription.InnerText = reader["description"].ToString();
                userOccupation.InnerText = reader["occupation"].ToString();
                userCountry.InnerText = reader["country"].ToString();
                userWebsite.InnerText = reader["website"].ToString();
            }
        }

        command.Dispose();
        connection.Close();
        
    }
}