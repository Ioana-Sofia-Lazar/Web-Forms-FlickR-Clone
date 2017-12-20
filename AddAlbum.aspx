<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddAlbum.aspx.cs"
    Inherits="AddAlbum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container-small" style="width: 500px;">
        <h1 class="display-4">
            Add New Album</h1>
        <div class="form-group row">
            <label for="albumTitle" class="col-sm-3 col-form-label">
                Album Title:
            </label>
            <div class="col-sm-8">
                <asp:TextBox ID="albumTitle" CssClass="form-control" runat="server" placeholder="New Album Name"></asp:TextBox>
                <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="albumTitle" ID="RequiredFieldValidator2"
                    runat="server" ErrorMessage="Please enter a title" CssClass="alert-danger"></asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Button ID="btnAddAlbum" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddAlbum_Click" />
        <span></span>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click"
            CausesValidation="false" />
    </div>
</asp:Content>
