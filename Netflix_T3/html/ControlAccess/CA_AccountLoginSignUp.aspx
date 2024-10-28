<%@ Page Title="Crear Usuario" Language="C#" MasterPageFile="~/html/ControlAccess/MP_ControlAccess.Master" AutoEventWireup="true" CodeBehind="CA_AccountLoginSignUp.aspx.cs" Inherits="Netflix_T3.html.ControlAccess.CA_AccountLoginSignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Crear Usuario
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <link href="../../css/css-acount/acoount.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="form-user">
    
    <div class="sign-up"> 
        <div class="form-group">
            <div class="div-button-horizontal">
            <asp:Button runat="server" CssClass="button-asp" ID="btn_signup" OnClick="BTN_SignUp" Text="Crear Usuario" />
               <h1 class="p-separate">/</h1> 
            <asp:Button runat="server" CssClass="button-asp" ID="btn_login" OnClick="BTN_Login" Text="Iniciar Sesion"/>
             </div>
        </div>
        <asp:Literal runat="server" ID="Literal_Sign_Log"></asp:Literal>
        <asp:PlaceHolder ID="phSignUp" runat="server"></asp:PlaceHolder>
        
        <div class="form-group">
            <asp:Button runat="server" CssClass="button-asp" ID="btn_signup_create" OnClick="BTN_SignUp_Create" Text="Crear Usuario" />
            <asp:Button runat="server" CssClass="button-asp" ID="btn_login_create" OnClick="BTN_Login_Create" Text="Iniciar Sesion"/>
        </div>

            <div class="form-group">
            <asp:Button ID="Button_Enviar" runat="server" Text="Enviar" OnClick="Enviar" CssClass="button-asp" />
            <asp:Button ID="btn_nuevo1" runat="server" Text="Boton de crear" OnClick="Btn_nuevo" CssClass="button-asp" />
            <asp:Button ID="btn_clean" runat="server" Text="Borrar Tabla" OnClick="Btn_Clean" CssClass="button-asp" />
        </div>
        <asp:Label ID="message_id_rory" runat="server"></asp:Label>
        <asp:Literal ID="LiteralHTML" runat="server"></asp:Literal>

        </div>
        <div class="gridview" id="div-gridvew">
            <asp:GridView runat="server" CssClass="class_gridvew" ID="ID_Gridview_admin" AutoGenerateColumns="false">
                <Columns>                    
                    <asp:BoundField DataField="id_user" HeaderText="Numero del Usuario" InsertVisible="False" ReadOnly="True" SortExpression="id_user"/>
                    <asp:BoundField DataField="user_names" HeaderText="Nombre" SortExpression="user_names" />
                    <asp:BoundField DataField="mail" HeaderText="Correo" SortExpression="mail" />
                    <asp:BoundField DataField="age" HeaderText="Edad" SortExpression="age" />
                    <asp:BoundField DataField="password_user" HeaderText="Contrasena" />                    
                      
                </Columns>
            </asp:GridView>
        </div>

</form>
</asp:Content>
