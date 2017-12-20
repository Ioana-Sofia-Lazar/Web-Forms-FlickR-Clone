using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

public partial class EditPhoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        displayPhoto();
    }

    protected void displayPhoto()
    {
        string photoId = Request.QueryString["photo_id"].ToString();

        // load photo
        imgPhotoBig.Style.Add("background-image", "Handlers/AlbumPhotoHandler.ashx?photo_id=" + photoId);
        imgPhotoBig.DataBind();
    }

    protected void btnEdited_Click(object sender, EventArgs e)
    {
        string photoId = Request.QueryString["photo_id"].ToString();

        // save edited photo to the db
        string query = "UPDATE photo SET image = @image WHERE id = @id";

        //Response.Write(Session["editedPhoto"].ToString());

        string converted = Session["editedPhoto"].ToString().Replace('-', '+');
        converted = converted.Replace('_', '/');
        byte[] img = Convert.FromBase64String(converted);

        // execute query
        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@image", img);
        command.Parameters.AddWithValue("@id", photoId);
        command.ExecuteNonQuery();

        command.Dispose();
        connection.Close();

        // redirect user to album where photo was added
        string albumId = Request.QueryString["album_id"].ToString();
        Response.Redirect("~/Album.aspx?album_id=" + albumId);
    }

    protected void btnOriginal_Click(object sender, EventArgs e)
    {
        // don't modify photo and redirect user to album where photo was added
        string albumId = Request.QueryString["album_id"].ToString();
        Response.Redirect("~/Album.aspx?album_id=" + albumId);
    }

    // ======== Filter Button Click Handlers =========

    protected void btnOrigin_Click(object sender, EventArgs e)
    {
        byte[] imageBytes = getImageFromDb();

        var base64 = Convert.ToBase64String(imageBytes);
        var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

        imgPhotoBig.Style.Add("background-image", imgSrc);

        Session["editedPhoto"] = base64;
    }

    protected void btnTransparency_Click(object sender, EventArgs e)
    {
        byte[] imageBytes = getImageFromDb();
        System.Drawing.Image image = byteToImage(imageBytes);

        image = drawWithTransparency(image);
        imageBytes = imageToByte(image);

        var base64 = Convert.ToBase64String(imageBytes);
        var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

        imgPhotoBig.Style.Add("background-image", imgSrc);

        Session["editedPhoto"] = base64;
    }

    protected void btnSepia_Click(object sender, EventArgs e)
    {
        byte[] imageBytes = getImageFromDb();
        System.Drawing.Image image = byteToImage(imageBytes);

        image = drawAsSepiaTone(image);
        imageBytes = imageToByte(image);

        var base64 = Convert.ToBase64String(imageBytes);
        var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

        imgPhotoBig.Style.Add("background-image", imgSrc);

        Session["editedPhoto"] = base64;
    }

    protected void btnNegative_Click(object sender, EventArgs e)
    {
        byte[] imageBytes = getImageFromDb();
        System.Drawing.Image image = byteToImage(imageBytes);

        image = drawAsNegative(image);
        imageBytes = imageToByte(image);

        var base64 = Convert.ToBase64String(imageBytes);
        var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

        imgPhotoBig.Style.Add("background-image", imgSrc);

        Session["editedPhoto"] = base64;
    }

    // ============ Image Utils =============

    private byte[] getImageFromDb()
    {
        // get photo from the database
        string photoId = Request.QueryString["photo_id"].ToString();

        string query = "SELECT image FROM photo WHERE id = @id";

        SqlConnection connection = new SqlConnection(DatabaseManager.GetConnectionString());
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@id", photoId);

        connection.Open();
        object data = command.ExecuteScalar();
        connection.Close();

        return (byte[]) data;
    }

    private byte[] imageToByte(System.Drawing.Image img)
    {
        ImageConverter converter = new ImageConverter();
        return (byte[]) converter.ConvertTo(img, typeof(byte[]));
    }

    private System.Drawing.Image byteToImage(byte[] bytes)
    {
        using (var ms = new MemoryStream(bytes))
        {
            return System.Drawing.Image.FromStream(ms);
        }
    }

    private Bitmap applyColorMatrix(System.Drawing.Image sourceImage, ColorMatrix colorMatrix)
    {
        Bitmap bmp32BppSource = getArgbCopy(sourceImage);
        Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);

        using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
        {
            ImageAttributes bmpAttributes = new ImageAttributes();
            bmpAttributes.SetColorMatrix(colorMatrix);

            graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);
        }

        bmp32BppSource.Dispose();

        return bmp32BppDest;
    }

    private Bitmap getArgbCopy(System.Drawing.Image sourceImage)
    {
        Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);

        using (Graphics graphics = Graphics.FromImage(bmpNew))
        {
            graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
            graphics.Flush();
        }

        return bmpNew;
    }

    // ========== Image Filters ==========

    public Bitmap drawWithTransparency(System.Drawing.Image sourceImage)
    {
        ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                        {
                            new float[]{1, 0, 0, 0, 0},
                            new float[]{0, 1, 0, 0, 0},
                            new float[]{0, 0, 1, 0, 0},
                            new float[]{0, 0, 0, 0.3f, 0},
                            new float[]{0, 0, 0, 0, 1}
                        });

        return applyColorMatrix(sourceImage, colorMatrix);
    }

    public Bitmap drawAsSepiaTone(System.Drawing.Image sourceImage)
    {
        ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
                {
                        new float[]{.393f, .349f, .272f, 0, 0},
                        new float[]{.769f, .686f, .534f, 0, 0},
                        new float[]{.189f, .168f, .131f, 0, 0},
                        new float[]{0, 0, 0, 1, 0},
                        new float[]{0, 0, 0, 0, 1}
                });


        return applyColorMatrix(sourceImage, colorMatrix);
    }

    public Bitmap drawAsNegative(System.Drawing.Image sourceImage)
    {
        ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
                    {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                    });


        return applyColorMatrix(sourceImage, colorMatrix);
    }
}