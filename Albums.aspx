<%@ Page Title="" Language="C#" MasterPageFile="~/UserProfile.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headProfile" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderProfile" Runat="Server">

    <div class="row">
  <div class="column">
    <div class="content">
      <div class="album-cover" 
        style="background-image: url('images/test1.jpeg');">
        </div>
      <h3>My Work</h3>
       <p>5 photos</p>
    </div>
  </div>
  <div class="column">
    <div class="content">
    <div class="album-cover" 
        style="background-image: url('images/test1.jpeg');">
        </div>
      <h3>My Work</h3>
      <p>5 photos</p>
    </div>
  </div>
  <div class="column">
    <div class="content">
    <div class="album-cover" 
        style="background-image: url('images/test1.jpeg');">
        </div>
      <h3>My Work</h3>
       <p>5 photos</p>
    </div>
  </div>
  <div class="column">
    <div class="content">
    <img src="/w3images/mountains.jpg" alt="Mountains" style="width:100%">
      <h3>My Work</h3>
       <p>5 photos</p>
    </div>
  </div>
<!-- END GRID -->
</div>

</asp:Content>
