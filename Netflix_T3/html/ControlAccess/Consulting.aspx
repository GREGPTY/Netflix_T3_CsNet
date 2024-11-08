<%@ Page Title="" Language="C#" MasterPageFile="~/html/ControlAccess/MP_ControlAccess.Master" AutoEventWireup="true" CodeBehind="Consulting.aspx.cs" Inherits="Netflix_T3.html.ControlAccess.Consulting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="tittle" runat="server">
    Consulting Data
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../../css/ControlAccess/Consulting.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form  class="form-consulting" id="form1">
        <div class="Consulting_Class">
            <div class="Consulting_Body">
                <div class="Borders">
                    <h1 class="Consulting_H1">
                        Consulting My Data
                    </h1>                    
                </div>                    
            </div>
            <div class="Consulting_Body">
                <div class="Consulting_Body_Left">
                    <div class="Borders">
                        <h2 class="Consulting_H2">
                            Personal Data: <asp:Label CssClass="Consulting_H2" runat="server" ID="ID_txt_1_1"></asp:Label>                    
                        </h2>
                        <div class="photo_piture_div">
                            <asp:Literal runat="server" ID="Literal_UserLogo"></asp:Literal>
                        </div>
                       <div>
                            <strong>ID:</strong> <asp:Label runat="server" ID="ID_txt_0"></asp:Label>
                        </div>
                        <div>
                            <strong>Username:</strong> <asp:Label runat="server" ID="ID_txt_1"></asp:Label>
                        </div>
                        <div>
                            <strong>Rank:</strong> <asp:Label runat="server" ID="ID_txt_2"></asp:Label>
                        </div>
                        <div>
                            <strong>Email:</strong> <asp:Label runat="server" ID="ID_txt_3"></asp:Label>
                        </div>
                        <div>
                            <strong>Salary Per Hour:</strong> <asp:Label runat="server" ID="ID_txt_4"></asp:Label>
                        </div>
                        <div>
                            <strong>Payment Mode:</strong> <asp:Label runat="server" ID="ID_txt_5"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="Consulting_Body_Right">
                    <div class="Borders">
                        <h2 class="Consulting_H2">
                            Last Data (Week Close)
                        </h2>
                        <h3 class="Consulting_H3">
                            Weekly Salary (Last)
                        </h3>
                        <div class="outer-container">
                            <div class="grid-container">
                                <asp:GridView ID="ID_DGV_0" runat="server" AutoGenerateColumns="true" CssClass="grid-view" />
                            </div>
                        </div>
                        <h3 class="Consulting_H3">
                            Daily Salary (Rank Of Week)
                        </h3>
                        <div class="outer-container">
                            <div class="grid-container">
                                <asp:GridView ID="ID_DGV_1" runat="server" AutoGenerateColumns="true" CssClass="grid-view" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="Consulting_Body">
                <div class="Borders">
                    <h2 class="Consulting_H2">
                        All Data (Weekly, Daily, Access Control)
                    </h2>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
