<%@ Page Title="" Language="C#" MasterPageFile="~/html/ControlAccess/MP_ControlAccess.Master" AutoEventWireup="true" CodeBehind="EditarUsuarios.aspx.cs" Inherits="Netflix_T3.html.ControlAccess.EditarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Editar Usuarios
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/css-acount/acoount.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <form  class="form-user" id="form1">
    
    <div class="sign-up"> 
        <div class="form-group">
            <div class="div-button-horizontal">
            <asp:Button runat="server" CssClass="button-asp" ID="btn_edituser" OnClick="BTN_EditUser" Text="Edit User" />
        </div>
            <asp:Panel runat="server" ID="ID_Contenedor">
         <div class='form-group'>
            <h1>Select User</h1>
            <asp:TextBox runat="server" ID="ID_Usernames_List_datalist" CssClass="listClass" AutoPostBack="true" OnTextChanged="listUsernames_TextChanged" list="Usernames_List_datalist" />
            <datalist id="Usernames_List_datalist">
               <asp:Literal runat="server" ID="Literal_Usernames_List"/>
            </datalist>
        </div>
             <div class='form-group'>
                <h1>New Username for: [<asp:Literal runat="server" ID="LiteralShow_0"/>]</h1>
                <asp:TextBox runat="server" ID="ID_txt_0" CssClass="form-control" />
                <p>Change Username <asp:CheckBox runat="server" ID="ID_chk_0" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
            </div>
                 <div class='form-group'>
                    <h1>New Rank, Last Rank is: [<asp:Literal runat="server" ID="LiteralShow_1"/>]</h1>
                    <asp:DropDownList runat="server" ID="ID_ddl_1" CssClass="ddClass" />
                    <p>Change Rank <asp:CheckBox runat="server" ID="ID_chk_1" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
                </div>
                <div class='form-group'>
                    <h1>New email, Last Email: [<asp:Literal runat="server" ID="LiteralShow_2"/>]</h1>
                    <asp:TextBox runat="server" ID="ID_txt_2" CssClass="form-control" />
                    <p>Change Email <asp:CheckBox runat="server" ID="ID_chk_2" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
                </div>
                <div class='form-group'>
                    <h1>New Salary/Hour, Last Salary/Hour: [<asp:Literal runat="server" ID="LiteralShow_3"/>]</h1>
                    <asp:TextBox runat="server" ID="ID_txt_3" CssClass="form-control" />
                    <p>Change Salary/Hour <asp:CheckBox runat="server" ID="ID_chk_3" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
                </div>
                <div class='form-group'>
                    <h1>New Payment Mode, Last Payment Mode: [<asp:Literal runat="server" ID="LiteralShow_4"/>]</h1>
                    <asp:DropDownList runat="server" ID="ID_ddl_4" CssClass="ddClass" />
                    <p>Change Payment Mode <asp:CheckBox runat="server" ID="ID_chk_4" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
                </div>                
                <div class='form-group'>
                    <h1>Change Password, Introduce The Current Password</h1>
                    <asp:TextBox runat="server" ID="ID_txt_5" CssClass="form-control" TextMode="Password"/>
                    <p>Change Password <asp:CheckBox runat="server" ID="ID_chk_5" CssClass="style_chk" AutoPostBack="true" OnCheckedChanged="Chk_Changed"/></p>
                    <h1>New Password</h1>
                    <asp:TextBox runat="server" ID="ID_txt_6" CssClass="form-control" TextMode="Password" />
                    <h1>Confirm New Password</h1>
                    <asp:TextBox runat="server" ID="ID_txt_7" CssClass="form-control" TextMode="Password" />
                </div>
                <div class='form-group'>
                    <asp:Literal runat="server" ID="Literal_Message"/>                    
                </div>
                <div class="form-group">
                    <div class="div-button-horizontal">
                    <asp:Button runat="server" CssClass="button-asp" ID="btn_edituser_create" OnClick="BTN_EditUser_Create" Text="Edit User" />
                </div>                    
                </asp:Panel>
   </div>        
        
        <div class="form-group">
            
        </div>
    </div>
</form>
</asp:Content>
