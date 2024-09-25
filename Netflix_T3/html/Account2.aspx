<%@ Page Title="" Language="C#" MasterPageFile="~/MP.Master" AutoEventWireup="true" CodeBehind="Account2.aspx.cs" Inherits="Netflix_T3.html.Account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Account
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../css/css-acount/acoount.css" rel="stylesheet"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Holas
    <form class="form-user">
        <table class="user">
            <tr class="tr-title">
                <td class="tr-title-user">Usuario</td>
            </tr>
            <tr class="tr-content">
                <td class="field"><label class="label-item-title"> Nombre de Usuario</label></td>
                <td class="field"><input type="text" id="user_name" name="user_name"></td>
                <td class="field"><label class="label-item-title">Correo</label></td>
                <td class="field"><input type="text" id="email" name="email"></td>
                <td class="field"><label class="label-item-title">Contraseña</label></td>
                <td class="field"><input type="text" id="password" name="email"></td>
                <td class="field"><label class="label-item-title">Fecha de nacimiento</label></td>
                <td class="field"><input type="date" id="date" name="email"></td>
                <td class="field"><button type="submit" id="submit-data" name="submit">Enviar</button></td>
            </tr>
        </table>
    </form>
</asp:Content>
