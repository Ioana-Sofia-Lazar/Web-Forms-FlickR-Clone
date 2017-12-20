<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PhotoDeleted.aspx.cs" Inherits="PhotoDeleted" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container-small text-center">
        <asp:Image ID="Image1" runat="server" ImageUrl="http://www.clker.com/cliparts/6/d/6/3/l/M/check-mark-hi.png"
            Width="200px" />
        <h1 class="display-5">
            Photo successfully deleted!</h1>
        <p>
            Go to 
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Default.aspx">All Photos</asp:HyperLink></p>
        <p>
            Go to 
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Photos.aspx">Your Photos</asp:HyperLink></p>
    </div>
</asp:Content>
