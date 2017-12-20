<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs"
    Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <br />
        <h1>
            Edit Profile</h1>
        <hr />
        <div class="row">
            <!-- left column -->
            <div class="col-md-3">
                <div class="text-center">
                    <asp:Image ID="imgNewProfile" runat="server" CssClass="avatar img-circle" alt="avatar" ImageUrl="//placehold.it/100"
                        Height="100px" Width="100px" />
                    <h6>
                        Change profile photo...</h6>
                    <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="profileFileUp" runat="server" CssClass="form-control profileUp" />
                </div>
                <br />
                <div class="text-center">
                    <asp:Image ID="imgNewCover" runat="server" CssClass="avatar img-circle" alt="avatar" ImageUrl="//placehold.it/200x100"
                        Height="100px" Width="200px" />
                    <h6>
                        Change cover photo...</h6>
                    <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="coverFileUp" runat="server" CssClass="form-control" />
                </div>
            </div>
            <!-- edit form column -->
            <div class="col-md-9 personal-info">
                <h3>
                    Personal info</h3>
                <form class="form-horizontal" action="">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Name:" CssClass="col-lg-3 control-label"></asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Your Name"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Occupation:" CssClass="col-lg-3 control-label"></asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtOccupation" runat="server" CssClass="form-control" placeholder="Your Occupation"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Country:" CssClass="col-lg-3 control-label"></asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" placeholder="Your Country"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Website:" CssClass="col-lg-3 control-label"></asp:Label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Your Website"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label">
                    </label>
                    <div class="col-md-8">
                        <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <span></span>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" />
                    </div>
                </div>
                </form>
            </div>
        </div>
    </div>
    <hr />
    <!-- Display photo when choosing one -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript">
        function readURLProfile(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_imgNewProfile').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        function readURLCover(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_imgNewCover').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

        $("input[id$='profileFileUp']").change(function () {
            readURLProfile(this);
        });
        $("input[id$='coverFileUp']").change(function () {
            readURLCover(this);
        });

    </script>
</asp:Content>
