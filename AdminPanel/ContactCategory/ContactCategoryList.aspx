<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="container border my-4 p-4">
        <div class="row mb-2">
            <div class="col-md-4">
                <h2>
                    <i class="fas fa-users"></i>
                    Contact Category
                </h2>
            </div>
            <div class="col-md-8 d-flex justify-content-end p-2">
                <asp:HyperLink runat="server" ID="btnAddCountry" NavigateUrl="~/AdminPanel/ContactCategory/ContactCategoryAddEdit.aspx" CssClass="btn btn-danger">
                    <i class="fas fa-plus"></i>
                    Add Contact Category
                </asp:HyperLink>
            </div>
        </div>
        <asp:GridView ID="gvContactCategory" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
    </div>
</asp:Content>

