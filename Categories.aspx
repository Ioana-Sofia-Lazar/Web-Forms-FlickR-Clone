<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link rel="Stylesheet" href="Style/categories-gallery.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 class="display-4 text-center">Categories</h1> <hr />
<asp:Repeater ID="repeaterCategories" runat="server">
    <ItemTemplate>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Format("Category.aspx?categ_id={0}&title={1}", Eval("id"), Eval("title")) %>'>
            
        <figure class="snip1158 red">
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# string.Format("Handlers/CategoryImageHandler.ashx?categ_id={0}", Eval("id")) %>' AlternateText='<%# Eval("title") %>' />
            <figcaption>
                <h3><%# Eval("title") %></h3>
            </figcaption>
        </figure>

        </asp:HyperLink>
    </ItemTemplate>
    </asp:Repeater>

</asp:Content>

