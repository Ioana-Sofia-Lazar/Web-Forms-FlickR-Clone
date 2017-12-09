using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

public class Utils
{
	public Utils()
	{
	}

    public static string getCurrentUsername()
    {
        MembershipUser membershipUser = Membership.GetUser();
        var username = membershipUser.UserName;

        return username;
    }
}