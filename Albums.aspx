<%@ Page Title="" Language="C#" MasterPageFile="~/UserProfile.master" AutoEventWireup="true" CodeFile="Albums.aspx.cs"
    Inherits="Albums" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headProfile" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" runat="Server">
    <div class="row">
        <div class="column">
            <a href="AddAlbum.aspx">
                <asp:Image ID="Image1" runat="server" ImageUrl="Images/add_photo.png" Style="padding-left: 50px;" />
            </a>
        </div>
        <asp:Repeater ID="repeaterAlbums" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("id", "Album.aspx?album_id={0}") %>'>
 
                    <div class="column">
                        <div class="content">
                            <div class="album-cover" 
                                style="background-image: url('<%# string.Format("Handlers/AlbumPhotoHandler.ashx?photo_id={0}", Eval("photo_id")) %>');">
                            </div>
                            <h3><%# Eval("title") %></h3>
                        </div>
                    </div>
                </asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
