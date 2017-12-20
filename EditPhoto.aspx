<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditPhoto.aspx.cs"
    Inherits="EditPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container">
        <div class="photo-big" runat="server" id="imgPhotoBig">
        </div>
        <div class="buttons-container text-center">
            <asp:Button ID="btnOrigin" runat="server" Text="Original" OnClick="btnOriginal_Click" CssClass="btn btn-default" />
            <asp:Button ID="btnTransparency" runat="server" Text="Transparency" OnClick="btnTransparency_Click" CssClass="btn btn-warning" />
            <asp:Button ID="btnSepia" runat="server" Text="Sepia" OnClick="btnSepia_Click" CssClass="btn btn-warning" />
            <asp:Button ID="btnNegative" runat="server" Text="Negative" OnClick="btnNegative_Click" CssClass="btn btn-warning" />
        </div>
        <div class="buttons-container text-center">
            <asp:Button ID="btnEdited" runat="server" Text="Publish Edited Photo" CssClass="btn btn-primary" OnClick="btnEdited_Click" />
            <span></span>
            <asp:Button ID="btnOriginal" runat="server" Text="Publish Original Photo" CssClass="btn btn-default"
                OnClick="btnOriginal_Click" />
        </div>
    </div>
</asp:Content>
