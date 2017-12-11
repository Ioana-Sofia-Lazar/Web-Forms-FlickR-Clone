<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AddPhoto.aspx.cs" Inherits="AddPhoto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="centered-container-small">
        <form class="form-horizontal" action="">
        <br />
        <div class="form-group">
            <asp:Label ID="Label5" runat="server" Text="Choose photo:" CssClass="col-lg-3 control-label"></asp:Label>
            <div class="col-lg-8">
                <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="photoFileUp" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please choose a photo!"
                    ControlToValidate="photoFileUp" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Category:" CssClass="col-lg-3 control-label"></asp:Label>
            <div class="col-lg-8">
                <asp:DropDownList ID="ddlCategories" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="Title:" CssClass="col-lg-3 control-label"></asp:Label>
            <div class="col-lg-8">
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Photo Title"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Title required!"
                    ControlToValidate="txtTitle" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="Description:" CssClass="col-lg-3 control-label"></asp:Label>
            <div class="col-lg-8">
                <asp:TextBox TextMode="multiline" Rows="5" ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-md-3 control-label">
            </label>
            <div class="col-md-8">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary"
                    OnClick="btnSave_Click" />
                <span></span>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false"
                    OnClick="btnCancel_Click" />
            </div>
        </div>
        </form>
    </div>
</asp:Content>
