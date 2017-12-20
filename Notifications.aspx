<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Notifications.aspx.cs" Inherits="Notifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link rel="Stylesheet" href="Style/notifications.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="message" runat="server" Text="No notifications to display."></asp:Label>
        <asp:Repeater ID="repeaterNotifications" runat="server">
            <ItemTemplate>
                <div class="notification">
                    <div class="photo">
                            <div id="userAvatar" class="avatar"
                                style="background-image: url('<%# string.Format("Handlers/PhotoHandler.ashx?username={0}&type=profile", Eval("fromUsername")) %>');">
                            </div>
                        </div>
                    <div class="message">
                        <h2>
                            <%# Eval("fromName") %> has added a comment on your <asp:HyperLink ID="HyperLink1" runat="server" 
                                NavigateUrl='<%# string.Format("Photo.aspx?photo_id={0}", Eval("photoId")) %>'>photo</asp:HyperLink>
                        </h2>
                        <p>
                            <%# Eval("text") %>
                        </p>
                    </div>
                    <div class="buttons">
                        <asp:HyperLink ID="HyperLink2" runat="server"
                                NavigateUrl='<%# string.Format("?accept={0}", Eval("commentId")) %>'>Accept</asp:HyperLink>
                        <asp:HyperLink ID="HyperLink3" runat="server" 
                                NavigateUrl='<%# string.Format("?decline={0}", Eval("commentId")) %>'>Decline</asp:HyperLink>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
 
</asp:Content>
