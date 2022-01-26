<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="AdminPanel_City_CityList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="container border border-primary border-3 my-4 p-4">
        <h2>
            <i class="fas fa-city"></i>
            City
        </h2>
        <asp:GridView ID="gvCity" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
    </div>
</asp:Content>

