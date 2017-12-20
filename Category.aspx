<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Category.aspx.cs"
    Inherits="Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1 id="categTitle" runat="server" class="display-5 text-center">
    </h1>
    <hr />
    <div class="centered-container">
        <i class="fa fa-chevron-left" aria-hidden="true"></i>
        <asp:Button ID="btnBack" runat="server" Text="Back to all Categories" OnClick="btnBack_Click" CssClass="btn btn-link" />
        <div class="flexbin">
            <asp:Label ID="message" runat="server" Text="" Visible="False"></asp:Label>
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
