using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PhotoAdded : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string photoId = Request.QueryString["photo_id"].ToString();
        string albumId = Request.QueryString["album_id"].ToString();
        Response.Redirect("~/EditPhoto.aspx?album_id=" + albumId + "&photo_id=" + photoId);
    }

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string albumId = Request.QueryString["album_id"].ToString();
        Response.Redirect("~/Album.aspx?album_id=" + albumId);
    }
}