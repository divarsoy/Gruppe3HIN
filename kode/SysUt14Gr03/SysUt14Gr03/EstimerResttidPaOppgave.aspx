<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstimerResttidPaOppgave.aspx.cs" Inherits="SysUt14Gr03.EstimerResttidPaOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Estimer resterende tid på oppgave</h2>
    <p> 
        <asp:DropDownList ID="ddlOppgaver" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnInfo" runat="server" Text="Vis info" OnClick="btnInfo_Click" />
    </p>
    <p> 
        <asp:Label ID="lblEstimat" runat="server" Visible="false"></asp:Label>
    </p>
    <p> 
        <asp:Label ID="lblBrukt" runat="server" Visible="false"></asp:Label>
    </p>
    <p> 
        <asp:TextBox ID="txtTimer" runat="server" MaxLength="3" TextMode="Number"></asp:TextBox>
    </p>
    <p> 
        <asp:RangeValidator runat="server" Type="Integer" 
        MinimumValue="0" MaximumValue="99" ControlToValidate="txtTimer" 
        ErrorMessage="Timeantall skal være mellom 0 and 99" />
    </p>
    <p> 
        <asp:Label ID="lblFeil" runat="server" Visible="false"></asp:Label>
    </p>
    <p> 
        <asp:Button ID="btnLagre" runat="server" OnClick="btnLagre_Click" Text="Lagre tid" />
    </p>
</asp:Content>
