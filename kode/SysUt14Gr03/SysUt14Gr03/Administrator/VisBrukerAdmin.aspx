<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="VisBrukerAdmin.aspx.cs" Inherits="SysUt14Gr03.VisBrukerAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="headerCenter">

            <h1><asp:Label ID="labelNavn" runat="server" Text="Navn"></asp:Label></h1>
            <asp:Label ID="labelInfo" Font-Bold="true" runat="server" Text="Info" Visible="False"></asp:Label>
            <asp:Label ID="labelOppgaver" runat="server" Visible="False"></asp:Label>
    
        <div class="listboxTest">
            <asp:ListBox ID="lbOppgaver" CssClass="form-control" Height="100px" Width="500px" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbOppgaver_SelectedIndexChanged" Rows="6" ></asp:ListBox>
        </div>
      
            <asp:Label ID="labelKommentarer" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="labelLogg" runat="server" Visible="False"></asp:Label>
        <div class="listboxTest">
            <asp:ListBox ID="lbKommentarer" CssClass="form-control" Height="100px" Width="500px" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbKommentarer_SelectedIndexChanged" Rows="6"></asp:ListBox>
        </div>

            
            <asp:Label ID="labelHistorikk" runat="server"></asp:Label>
        <div class="listboxTest">
            <asp:ListBox ID="lbLogg" CssClass="form-control" Height="100px" Width="500px" runat="server" Visible="False" Rows="6"></asp:ListBox>
        </div>

            <asp:Label ID="labelFullfort" runat="server" Visible="False"></asp:Label>

        <div class="listboxTest">
            <asp:ListBox ID="lbFFullfort" CssClass="form-control" Height="100px" Width="500px" runat="server" Visible="False" Rows="6"></asp:ListBox>
        </div>
            <asp:Label ID="labelPrefs" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="buttonPrefs" CssClass="btn btn-primary" runat="server" OnClick="btnPrefs_Click" Text="Endre instillinger" Visible="False"/>

        </div>
    </div>
</asp:Content>
