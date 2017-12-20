<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Album.aspx.cs"
    Inherits="Album" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" href="Style/profile.css" />
    <link rel="Stylesheet" href="Style/photo-gallery.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container">
        <i class="fa fa-chevron-left" aria-hidden="true"></i>
        <asp:Button ID="btnBack" runat="server" Text="Back to Albums" OnClick="btnBack_Click" CssClass="btn btn-link" />
        <br />
        <asp:Label ID="albumTitle" runat="server" Text="" CssClass="display-4"></asp:Label>
        <hr />
        <asp:Button ID="btnAddPhoto" runat="server" Text="Add Photo" CssClass="btn btn-primary" OnClick="btnAddPhoto_Click" />
        <br />
        <div class="flexbin">
            <asp:Repeater ID="repeaterPhotos" runat="server">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("Photo.aspx?photo_id={0}", Eval("photo_id")) %>'>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("Handlers/AlbumPhotoHandler.ashx?photo_id={0}", Eval("photo_id")) %>' />
                        <div class="photo-caption">
                            <div class="photo-title">
                                <p>
                                    <strong>
                                        <%# Eval("photo_title") %></strong>
                                </p>
                                <p>
                                    in <strong>
                                        <%# Eval("category") %></strong>
                                </p>
                            </div>
                        </div>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
