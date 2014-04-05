<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AktiverKonto.aspx.cs" Inherits="SysUt14Gr03.AktiverKonto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="lblAktivert" Visible="false" runat="server"></asp:Label>
    </p>
    <p>
     
        <asp:Label ID="lblUsername" AssociatedControlID="Username" runat="server">Brukernavn:</asp:Label>
        <asp:TextBox ID="Username" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Username" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>
        <asp:Label ID="lblBrukernavnFeil" runat="server" Visible="false"></asp:Label>
    </p>
    <p>

        <asp:Label ID="lblAftername" AssociatedControlID="Aftername" runat="server">Etternavn:</asp:Label>
        <asp:TextBox ID="Aftername" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Aftername" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    </p>
    <p>
        
        <asp:Label ID="lblFirstname" AssociatedControlID="Firstname" runat="server">Fornavn:</asp:Label>
        <asp:TextBox ID="Firstname" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Firstname" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    </p>
    <p> 
       
        <asp:Label ID="lblEmail" AssociatedControlID="Email" runat="server">Epost:</asp:Label>
        <asp:TextBox ID="Email" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    </p>
    <p>
        <asp:Label ID="lblImadress" AssociatedControlID="Im_adress" runat="server">IM Adresse:</asp:Label>
        <asp:TextBox ID="Im_adress" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Im_adress" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    </p>
    <p>
        <asp:Label ID="lblPassword" AssociatedControlID="Password" runat="server">Passord:</asp:Label>
        <asp:TextBox ID="Password" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    </p>
    <p>
        <asp:Button ID="ConfirmButton" runat="server" Text="Fullfør registrering" OnClick="ConfirmButton_Click" /> 
    </p>
</asp:Content>
