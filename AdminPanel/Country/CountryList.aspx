﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="AdminPanel_Country_Read" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cphContent" runat="Server">
    <div class="container border my-3 p-4">
        <div class="row mb-2">
            <div class="col-md-4">
                <h2>
                    <i class="fas fa-globe-americas"></i>
                    Country
                </h2>
            </div>
            <div class="col-md-8 d-flex justify-content-end p-2">
                <asp:HyperLink runat="server" ID="btnAddCountry" NavigateUrl="~/AdminPanel/Country/CountryAddEdit.aspx" CssClass="btn btn-danger" >
                    <i class="fas fa-plus"></i>
                    Add Country
                </asp:HyperLink>
            </div>
        </div>
        <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="CountryID" HeaderText="Id"/>
                <asp:BoundField DataField="CountryName" HeaderText="Name"/>
                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date"/>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger">
                            <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="btnEdit" CssClass="btn btn-gradient">
                            <i class="fas fa-edit"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

