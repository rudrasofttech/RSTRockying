﻿<%@ Page Title="Book Search History" Language="C#" MasterPageFile="~/Admin/AdminSite.master" AutoEventWireup="true" CodeFile="SearchHistory.aspx.cs" Inherits="Admin_SearchHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Search History</h1>
    <asp:GridView ID="SearchHistoryGridView" EmptyDataText="No Search History" CssClass="table" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SearchHistoryDataSource" PageSize="30" AutoGenerateDeleteButton="True" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="ID" Visible="false" />
            <asp:BoundField DataField="MemberName" HeaderText="Search By" SortExpression="MemberName" />
            <asp:BoundField DataField="Keywords" HeaderText="Keywords" SortExpression="Keywords" />
            <asp:BoundField DataField="SearchDate" DataFormatString="{0: yyyy-MMM-d}" HeaderText="Search Date" SortExpression="SearchDate" />
            <asp:BoundField DataField="ResultCount" HeaderText="Results" SortExpression="ResultCount" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SearchHistoryDataSource" ConnectionString="<%$ ConnectionStrings:RockyingConnectionString %>" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="SELECT M.MemberName, SH.ID, SH.Keywords, SH.SearchDate, SH.ResultCount FROM Member AS M RIGHT OUTER JOIN SearchHistory AS SH ON M.ID = SH.MemberID ORDER BY SH.ResultCount" DeleteCommand="DELETE FROM SearchHistory WHERE (ID = @ID)">
        <DeleteParameters>
            <asp:ControlParameter ControlID="SearchHistoryGridView" Name="ID" PropertyName="SelectedValue" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>

