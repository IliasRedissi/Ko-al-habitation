
<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="UploadTest.aspx.cs" Inherits="ClientWeb.UploadTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
    <hr />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false">
        <Columns>
            <asp:BoundField DataField="Text" />
            <asp:ImageField  DataImageUrlField="Value" ControlStyle-Height="100" ControlStyle-Width="100" />
        </Columns>
    </asp:GridView>
</asp:Content>
