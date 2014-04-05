<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="VisBruker.aspx.cs" Inherits="SysUt14Gr03.VisBruker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="headerCenter">

            <h1><asp:Label ID="lblNavn"  runat="server" Text="Navn"></asp:Label></h1>
            <hr />
            <asp:Label ID="lblInfo" Font-Bold="true" runat="server" Text="Info" Visible="False"></asp:Label>
            <hr />
            <asp:Label ID="lblOppgaver" runat="server" Visible="False"></asp:Label>
    
        <div class="listboxTest">
            <asp:ListBox ID="lsbOppgaver" CssClass="form-control" Height="100px" Width="500px" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbOppgaver_SelectedIndexChanged" ></asp:ListBox>
        </div>
       
            <asp:Label ID="lblKommentarer" runat="server" Visible="False"></asp:Label>

        <div class="listboxTest">
            <asp:ListBox ID="lsbKommentarer" CssClass="form-control" Height="100px" Width="500px" AutoPostBack="True" runat="server" Visible="False" OnSelectedIndexChanged="lsbKommentarer_SelectedIndexChanged"></asp:ListBox>
        </div>

            <asp:Label ID="lblLogg" runat="server" Visible="False"></asp:Label>

        <div class="listboxTest">
            <asp:ListBox ID="lsbLogg" CssClass="form-control" Height="100px" Width="500px" runat="server" Visible="False"></asp:ListBox>
        </div>

            <asp:Label ID="lblFullfort" runat="server" Visible="False"></asp:Label>

        <div class="listboxTest">
            <asp:ListBox ID="lsbFFullfort" CssClass="form-control" Height="100px" Width="500px" runat="server" Visible="False"></asp:ListBox>
        </div>

            <asp:Label ID="lblPrefs" runat="server" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnPrefs" CssClass="btn btn-primary" runat="server" OnClick="btnPrefs_Click" Text="Endre instillinger" Visible="False"/>

        </div>
    </div>
</asp:Content>
