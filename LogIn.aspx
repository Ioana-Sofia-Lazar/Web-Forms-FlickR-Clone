<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container-small text-center">
        <h1>LOGIN</h1>
        <asp:Login ID="Login1" runat="server" CssClass="form-row align-items-center wizard-container-login" LoginButtonStyle-CssClass="btn btn-primary btn-lg btn-block" TextBoxStyle-CssClass="form-control"
            OnLoggedIn="OnLoggedIn" TitleText=" ">
        </asp:Login>
    </div>
</asp:Content>
