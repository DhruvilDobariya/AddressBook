<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanel_Contact_ContactList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="container-fluid border border-primary border-3 my-4 p-4">
        <h2>
            <i class="fas fa-user"></i>
            Contact
        </h2>
        <asp:GridView ID="gvContact" runat="server" CssClass="table table-striped table-hover table-responsive"></asp:GridView>
    </div>
</asp:Content>