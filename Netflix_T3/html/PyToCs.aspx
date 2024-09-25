<%@ Page Title="" Language="C#" MasterPageFile="~/html/MP.Master" AutoEventWireup="true" CodeBehind="PyToCs.aspx.cs" Inherits="Netflix_T3.html.PyToCs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Py to C#
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../css/PyToCs.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-main-pytocs">
    <p class="p-title">Bienvenido a la recreacion de Py to C#, esta pagina esta principalmente para recrear lo aprendido en Python y ver su comparacion en C#</p>
    <p class="p-title">Asi que sientanse libre de probar esto</p>
    </div>
    
        <div class="div-title">
            <p class="p-title-module1">Modulo 1</p>
            <p class="p-title-module1">Math, random, platform </p>
            <asp:PlaceHolder runat="server" ID="phMathData"></asp:PlaceHolder>
            <asp:Literal runat="server" ID="liMessage"></asp:Literal>

        <div class="div-content-module1">
        <div class="div-math-module-left">
            <p class="p-title-module1">Math Module</p>
            <p class="p-title-module1">Que desea realizar?</p>
            <div class="div-ddClass"><asp:DropDownList ID="ddlMathOptions" CssClass="ddClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMathOptions_SelectedIndexChanged"></asp:DropDownList></div>
            <asp:literal runat="server" ID="literalMathA"></asp:literal>
            <div class="div-ddClass"><asp:TextBox ID="txtMathA" runat="server" CssClass="TextBox-Input"> </asp:TextBox></div>

            <asp:literal runat="server" ID="literalMathB"></asp:literal>
            <div class="div-ddClass"><asp:TextBox ID="txtMathB" runat="server" CssClass="TextBox-Input"> </asp:TextBox></div>   
            
            <div class="div-ddClass"><asp:Button ID="ID_ButtonMathCalculate" OnClick="ClcButtonMathCalculate" CssClass="button-asp" Text="Calcular" runat="server"/></div>

            <asp:literal runat="server" ID="literalRespuesta"></asp:literal>            
            
        </div>
        <div class="div-random-module-mid">
            <p class="p-title-module1">Random Module</p> 
            <p class="p-title-module1">Que desea realizar?</p>
            <div class="div-ddClass"><asp:DropDownList ID="ddlRandomOptions" CssClass="ddClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRandomOptions_SelectedIndexChanged"></asp:DropDownList></div>
            
            <div class="div-ddClass"> <asp:Literal ID="RandomLiteralA" runat="server"> </asp:Literal></div>
            <div class="div-ddClass"><asp:TextBox ID="txtRandomA" runat="server" CssClass="TextBox-Input"> </asp:TextBox></div>

            <asp:literal runat="server" ID="RandomLiteralB"></asp:literal>
            <div class="div-ddClass"><asp:TextBox ID="txtRandomB" runat="server" CssClass="TextBox-Input"> </asp:TextBox></div>       
            
            <div class="div-ddClass"><asp:Button ID="ID_ButtonRandomGenerate" OnClick="ClcButtonRandomGenerate" CssClass="button-asp" Text="Generar" runat="server"/></div>

            <asp:literal runat="server" ID="RandomLiteralAnswer"></asp:literal>    
        </div>
        <div class="div-platform-module-right">
            <p class="p-title-module1">Platform Module</p>
            <p>En C# no existe como tal un modulo de platform como en Python, esto se buscara luego de analizar como funciona el mismo</p>
            <p>Este narra como obtener informacion acerca del sistema operativo en si, como la version, los bits, etc...</p>
            <div class="div-ddClass"><asp:DropDownList ID="ddlPlatformModule" CssClass="ddClass" runat="server" Visible="false" Enabled="false"></asp:DropDownList></div>
<div class="div-ddClass"> <asp:Literal ID="PlatformLiteral" runat="server"> </asp:Literal></div>
        </div>
    </div>
            </div>
</asp:Content>
