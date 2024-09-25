<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="Index3.aspx.cs" Inherits="Netflix_T3.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="css/css-home/home.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Hello this is a try
    <table class="table-try1">
        <tr>
            <th class="title-left">Left Title</th>
            <th class="title-left">Right Title</th>
        </tr>
        <tr>
            <td class="Content-left">Content Left</td>
            <td class="Content-Rigth">Content Right</td>
        </tr>
    </table>
</asp:Content>
