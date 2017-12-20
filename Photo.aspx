<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Photo.aspx.cs" Inherits="Photo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="Stylesheet" href="Style/comments.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container text-center">
        <i class="fa fa-chevron-left"></i>
        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-link" OnClientClick="JavaScript:window.history.back(1);return false;" />
        <br />
        <div class="photo-big" runat="server" id="imgPhotoBig">
        </div>
        <div class="details-right">
            <p id="txtAuthor" runat="server">
                by
            </p>
            <p id="txtCategory" runat="server">
                in
            </p>
        </div>
        <div class="delete-opt">
            <asp:HyperLink ID="linkDeletePhoto" runat="server">Delete Photo</asp:HyperLink>
        </div>
        <h1 class="display-4" id="txtTitle" runat="server">
        </h1>
        <hr />
        <p id="txtDescription" runat="server" style="font-size: 1.7em;">
        </p>
        <br />
        <!-- Comments -->
        <div class="comments">
            <h1 class="display-5" id="h1" runat="server">
                Comments</h1>
            <div class="alert alert-warning alert-dismissible fade show" runat="server" id="commentAlert" visible="false">
                A request to add the comment has been sent to the owner of the photo!
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span>&times;</span>
                </button>
            </div>
            <div class="comment-wrap">
                <div class="photo">
                    <div id="loggedUserAvatar" runat="server" class="avatar" style="background-image: url('https://s3.amazonaws.com/uifaces/faces/twitter/dancounsell/128.jpg')">
                    </div>
                </div>
                <div class="comment-block">
                    <asp:TextBox CssClass="type-text" ID="commentText" runat="server" Rows="3" TextMode="multiline"
                        placeholder="Add comment..."></asp:TextBox>
                    <asp:ImageButton ID="btnAddComment" runat="server" ImageUrl="https://www.shareicon.net/data/128x128/2016/11/14/852369_send_512x512.png"
                        Height="17px" OnClick="btnAddComment_Click" />
                </div>
            </div>
            <asp:Repeater ID="repeaterComments" runat="server">
                <ItemTemplate>
                    <div class="comment-wrap">
                        <div class="photo">
                            <div id="userAvatar" class="avatar" style="background-image: url('<%# string.Format("Handlers/PhotoHandler.ashx?username={0}&type=profile", Eval("username")) %>');">
                            </div>
                        </div>
                        <div class="comment-block">
                            <p class="comment-text">
                                <%# Eval("text") %></p>
                            <div class="bottom-comment">
                                <div class="comment-date">
                                    <%# Eval("name") %>
                                    |
                                    <%# Eval("date") %></div>
                                <ul class="comment-actions">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# string.Format("?photo_id={0}&delete={1}", Eval("photoId"), Eval("commentId")) %>'>
                                    <%# (Eval("username").Equals(Utils.getCurrentUsername()) || Utils.getCurrentUserType().Equals("admin")) ? 
                                        "<li class='complain'>Delete</li>" : "" %>
                                    </asp:HyperLink>
                                </ul>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
