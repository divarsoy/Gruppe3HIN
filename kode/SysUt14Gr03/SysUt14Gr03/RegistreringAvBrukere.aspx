<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistreringAvBrukere.aspx.cs" Inherits="SysUt14Gr03.RegistreringAvBrukere" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Registrer ny bruker</h1>
    <div class="col-md-1">
        <p>Etternavn</p>
        <p>Fornavn</p>
        <p>Epost</p>
    </div>
    <div class="col-md-11">
        <p><asp:TextBox ID="tb_reg_etternavn" runat="server" Width="166px"></asp:TextBox></p>
        <p><asp:TextBox ID="tb_reg_fornavn" runat="server"  Width="168px"></asp:TextBox></p>
        <p><asp:TextBox ID="tb_reg_epost" runat="server"  Width="168px"></asp:TextBox></p>
        <p><asp:Button ID="bt_adm_reg" runat="server"  Text="Registrer Bruker" OnClick="bt_adm_reg_Click" /></p>
        <p><asp:Label ID="FeilmeldingEtternavn" runat="server" ForeColor="Red" Text="Etternavn kan ikke være lenger enn 256 tegn" Visible="False"></asp:Label> </p>
        <p><asp:Label ID="FeilMeldingFornavn" runat="server" ForeColor="Red" Text="Fornavn kan ikke være lenger enn 256 tegn" Visible="False"></asp:Label> </p>
        <p><asp:Label ID="FeilMeldingEpost" runat="server" ForeColor="Red" Text="Epost kan ikke være lenger enn 256 tegn og må ikke være registrert tidligere" Visible="False"></asp:Label> </p>
        <p><asp:Label ID="BrukerRegistrert" runat="server" ForeColor="Red" Text="Ny bruker har blitt registrert" Visible="False"></asp:Label> </p>
    </div>
        
</asp:Content>

