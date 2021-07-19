﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminSite.master" AutoEventWireup="true"
    CodeFile="MemberList.aspx.cs" Inherits="Admin_MemberList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row-fluid">
        <div class="span12 well well-small">
            <h1>Member List</h1>
            <div class="form-inline">
                <label class="control-label" for="<%: StatusDropDown.ClientID %>">
                    Status</label>
                <asp:DropDownList ID="StatusDropDown" runat="server">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="0">Active</asp:ListItem>
                    <asp:ListItem Value="2">Deleted</asp:ListItem>
                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                    <asp:ListItem Value="3">Unverified</asp:ListItem>
                </asp:DropDownList>
                <label class="control-label" for="<%: MemberTypeDropDown.ClientID %>">
                    Member Type</label>
                <asp:DropDownList ID="MemberTypeDropDown" runat="server">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="2">Author</asp:ListItem>
                    <asp:ListItem Value="3">Member</asp:ListItem>
                </asp:DropDownList>
                <label class="control-label" for="<%: SubscribeList.ClientID %>">
                    Subscribed</label>
                <asp:DropDownList ID="SubscribeList" runat="server">
                    <asp:ListItem Value="">--Select--</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                    <asp:ListItem Value="0">No</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="SubmitButton" runat="server" Text="Filter" CssClass="btn btn-primary"
                    CausesValidation="false" OnClick="SubmitButton_Click" />
                <asp:Button ID="btnDeleteRecords" CausesValidation="false" CssClass="btn" Text="Delete Records" OnClick="btnDeleteRecords_Click"  
                        runat="server" style="margin-left:25px;" />
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
            <asp:GridView ID="MemberGridView" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" AllowPaging="True"
                AllowSorting="false" CssClass="table table-striped table-bordered table-condensed"
                PageSize="100" OnPageIndexChanging="MemberGridView_PageIndexChanging" OnRowDataBound="MemberGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Select">  
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkSelect" CssClass="chkSelect" runat="server" />  
                                </ItemTemplate>  
                            </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Createdate" DataFormatString="{0:d MMM y}" HeaderText="Createdate"
                        SortExpression="Createdate" />
                    <asp:CheckBoxField DataField="Newsletter" HeaderText="Newsletter" SortExpression="Newsletter" />
                    <asp:BoundField DataField="UserType" HeaderText="Type" SortExpression="UserType" />
                    <asp:BoundField DataField="MemberName" HeaderText="MemberName" SortExpression="MemberName" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ID", "managemember.aspx?id={0}&mode=edit") %>'
                                Text="Edit"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("ID", "SendMail.aspx?id={0}") %>'
                                Text="Mail"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script>
        $("#ContentPlaceHolder1_MemberGridView tr").click(function () {
            if ($(this).find(".chkSelect > input[type=checkbox]").prop('checked')) {

                $(this).find(".chkSelect > input[type=checkbox]").prop("checked", false);

            } else {
                $(this).find(".chkSelect > input[type=checkbox]").prop("checked", true);
            }
        })
    </script>
</asp:Content>