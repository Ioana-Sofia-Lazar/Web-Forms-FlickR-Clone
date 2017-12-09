<%@ Page Title="" Language="C#" MasterPageFile="~/UserProfile.master" AutoEventWireup="true" CodeFile="Photos.aspx.cs" Inherits="Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headProfile" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" Runat="Server">

<div class="flexbin">
        <a href="#">
            <img src="images/test1.jpeg" />
			<div class="photo-caption">
                <div class="photo-title">
                    <p>Photo Title</p>
                     <p>by Author</p>
                </div>
                <div class="photo-details">
                    <i class="fa fa-comment-o" aria-hidden="true"></i> <span>120</span>
                    <span>Nature</span>
                </div>
                
             </div>
        </a>
        <a href="#">
            <img src="images/test2.jpeg" />
        </a>
        <a href="#">
            <img src="images/test1.jpeg" />
        </a>
        <a href="#">
            <img src="images/test3.jpg" />
        </a>
        <a href="#">
            <img src="images/test1.jpeg" />
        </a>
        <a href="#">
            <img src="images/test4.jpg" />
        </a>
        <a href="#">
            <img src="images/test5.jpeg" />
        </a>
    </div>

</asp:Content>

