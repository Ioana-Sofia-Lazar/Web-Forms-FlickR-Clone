<%@ Page Title="" Language="C#" MasterPageFile="~/UserProfile.master" AutoEventWireup="true" CodeFile="About.aspx.cs"
    Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headProfile" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" runat="Server">
    <div class="about-container">
        <p id="userDescription" runat="server" class="about-description">
        </p>
        <div class="align-right">
            <p>
                Occupation</p>
            <p>
                Country</p>
            <p>
                Website</p>
        </div>
        <div class="align-left">
            <strong>
                <p id="userOccupation" runat="server">
                </p>
                <p id="userCountry" runat="server">
                </p>
                <p id="userWebsite" runat="server">
                </p>
            </strong>
        </div>
    </div>
</asp:Content>
