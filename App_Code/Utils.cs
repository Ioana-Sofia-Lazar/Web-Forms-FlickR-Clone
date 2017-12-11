using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;

public class Utils
{
	public Utils()
	{
	}

    public static string getCurrentUsername()
    {
        if (!((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Response.Redirect("~/LogIn.aspx");          
        }

        MembershipUser membershipUser = Membership.GetUser();
        var username = membershipUser.UserName;

        return username;
    }

    public static string getCurrentUserType()
    {
        if (!((System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated))
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Response.Redirect("~/LogIn.aspx");
        }

        MembershipUser membershipUser = Membership.GetUser();
        string username = membershipUser.UserName;
        string type = "";

        string query = @"SELECT r.RoleName 
                         FROM aspnet_Users u 
                            JOIN aspnet_UsersInRoles ur ON (u.UserId = ur.UserId) 
                            JOIN aspnet_Roles r ON (r.RoleId = ur.RoleId) 
                         WHERE username = @username";

        // get user type for current user
        using (SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString()))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        type = reader["RoleName"].ToString();
                    }
                }
            }
            connection.Close();
        }

        return type.ToLower();
    }
}