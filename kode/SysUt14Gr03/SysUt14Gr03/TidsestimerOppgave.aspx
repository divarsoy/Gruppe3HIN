<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.Master" AutoEventWireup="true" CodeBehind="TidsestimerOppgave.aspx.cs" Inherits="SysUt14Gr03.TidsestimerOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentUtvikler" runat="server">
    <h1>Tidsestimer oppgave</h1>
    <p>
        <asp:ListBox ID="lsbOppgaver" runat="server"></asp:ListBox>
        <br />
        <asp:Button ID="btnVisEstimat" runat="server" OnClick="btnVisEstimat_Click" Text="Vis estimat" />
        <br />
    </p>
    <br />
    <br />
    <p>
        <asp:Label ID="Label1" runat="server" Text="Tidsestimat"></asp:Label>
        <asp:TextBox ID="txtEstimat" runat="server" TextMode="Number"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="timer"></asp:Label>
        <br />
        <asp:RangeValidator runat="server" Type="Integer" 
        MinimumValue="0" MaximumValue="99" ControlToValidate="txtEstimat" 
        ErrorMessage="Timeantall skal være mellom 0 and 99" />
        <br />
        <asp:Label ID="Feilmelding" runat="server" visible="false" Text="Feil"></asp:Label>

    </p>
    <p>
        <asp:Button ID="btnLagreEstimat" runat="server" OnClick="btnLagreEstimat_Click" Text="Lagre estimat" />
    </p>
</asp:Content>
