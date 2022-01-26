<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="container border border-primary border-3 my-4 p-4">
        <h2>
            <i class="fas fa-flag-checkered"></i>
            State
        </h2>
        <asp:GridView ID="gvState" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
    </div>
</asp:Content>

