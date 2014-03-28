<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="OpprettProsjekt.aspx.cs" Inherits="SysUt14Gr03.OpprettProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h2>Opprett Prosjekt</h2>
   <p>Prosjektnavn:  <asp:TextBox ID="tbProsjektnavn" runat="server" Height="25px" ></asp:TextBox>
    
   </p>
   <p>Prosjektleder: <asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList>
   </p>
    <p>Team:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         <asp:DropDownList ID="dropTeam" runat="server" Height="22px"></asp:DropDownList>
    <asp:Calendar ID="cal" runat="server" Width="293px"></asp:Calendar>
   <p>Startdato:     <asp:TextBox ID="tbStart" runat="server" Height="23px" ></asp:TextBox><asp:Button ID="btnStart" Text="Sett Startdato" OnClick="btnStart_Click" runat="server" Width="112px" Height="30px" />
    </p>
   <p>Sluttdato:     <asp:TextBox ID="tbSlutt" runat="server" Height="23px" Width="122px" ></asp:TextBox><asp:Button ID="btnSlutt" Text="Sett Sluttdato" OnClick="btnSlutt_Click" runat="server" Width="112px" Height="30px" />
    </p>
    <asp:Button ID="btnLagre" runat="server" Text="Lagre" OnClick="btnLagre_Click" Width="193px"/>
    <asp:Label ID="lblFeil" runat="server" Visible="False"></asp:Label>
</asp:Content>
