<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="centered-container">

<h1 class="display-5">Recently added photos</h1><hr />

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


