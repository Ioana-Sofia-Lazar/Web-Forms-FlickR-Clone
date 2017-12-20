<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoAdded.aspx.cs" Inherits="PhotoAdded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="centered-container-small">
        <p>Before your photo getting published, would you like to edit it?</p>
        <div class="form-group">
            <label class="col-md-3 control-label">
            </label>
            <div class="col-md-12">
                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary"
                    OnClick="btnEdit_Click" />
                <span></span>
                <asp:Button ID="btnPublish" runat="server" Text="Publish Photo" CssClass="btn btn-default" 
                    OnClick="btnPublish_Click" />
            </div>
        </div>
        
    </div>
</asp:Content>

