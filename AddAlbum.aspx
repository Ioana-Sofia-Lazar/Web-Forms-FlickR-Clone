<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddAlbum.aspx.cs" Inherits="AddAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="centered-container-small">
    <h1 class="display-4">Add New Album</h1>

    <div class="form-group row">
    <label for="albumTitle" class="col-sm-3 col-form-label">New Album Title: </label>
    <div class="col-sm-8">
        <asp:RequiredFieldValidator display=Dynamic ControlToValidate="albumTitle" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter a title" CssClass="alert-danger"></asp:RequiredFieldValidator>
        <asp:TextBox ID="albumTitle" CssClass="form-control" runat="server" placeholder="New Album Name"></asp:TextBox>
    </div>
  </div>

    <asp:Button ID="btnAddAlbum" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddAlbum_Click" />
</div>

</asp:Content>

