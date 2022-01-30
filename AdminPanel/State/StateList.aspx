<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="container border my-4 p-4">
        <div class="row mb-2">
            <div class="col-md-4">
                <h2>
                    <i class="fas fa-flag-checkered"></i>
                    State
                </h2>
            </div>
            <div class="col-md-8 d-flex justify-content-end p-2">
                <asp:HyperLink runat="server" ID="btnAddCountry" NavigateUrl="~/AdminPanel/State/StateAddEdit.aspx" CssClass="btn btn-danger">
                    <i class="fas fa-plus"></i>
                    Add State
                </asp:HyperLink>
            </div>
        </div>
        <asp:GridView ID="gvState" runat="server" CssClass="table table-striped table-hover"></asp:GridView>
    </div>
</asp:Content>

