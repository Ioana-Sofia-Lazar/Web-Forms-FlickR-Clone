<%@ Page Title="" Language="C#" MasterPageFile="~/UserProfile.master" AutoEventWireup="true"
    CodeFile="Photos.aspx.cs" Inherits="Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headProfile" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" runat="Server">
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
</asp:Content>
