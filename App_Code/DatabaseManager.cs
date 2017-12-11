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

    public static string GetConnectionString()
    {
        return @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\FMI\AN3\Sem1\DAW\Proiect\App_Data\ASPNETDB.MDF;Integrated Security=True;User Instance=True";
    }

}