using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DatabaseManager
{
    private static DatabaseManager _instance;

    public DatabaseManager()
	{
	}

    static DatabaseManager()
    {
        _instance = new DatabaseManager();
    }
    public DatabaseManager Instance
    {
        get { return _instance; }
    }

    public static string GetConnectionString()
    {
        return @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Jojo\Web-Forms-FlickR-Clone\App_Data\ASPNETDB.MDF;Integrated Security=True;User Instance=True";
    }

}