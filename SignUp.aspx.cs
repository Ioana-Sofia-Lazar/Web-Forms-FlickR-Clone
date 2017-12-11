using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        // Add role for the new user
        Roles.AddUserToRole(CreateUserWizard1.UserName, "User");

        // Get ID of the newly added user
        MembershipUser newUser = Membership.GetUser(CreateUserWizard1.UserName);
        Guid newUserId = (Guid)newUser.ProviderUserKey;

        // Get profile data Entered by the user
        String username = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName")).Text;
        String email = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email")).Text;

        // Insert new user with this info
        string query = "INSERT INTO user_info(ref_id, username, email) VALUES(@id, @username, @email);";

        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {

            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", newUserId);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            command.ExecuteNonQuery();

            connection.Close();

        }
 

    }
}