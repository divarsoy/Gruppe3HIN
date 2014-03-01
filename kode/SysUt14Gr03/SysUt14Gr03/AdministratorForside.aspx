<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministratorForside.aspx.cs" Inherits="SysUt14Gr03.AdministratorForside" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Forside for Administrator</h1>

    <div class="kolonner col-md-12">
        <p>Velkommen, Administrator!</p>

        <p><asp:Button ID="tempButton" runat="server" Text="Administrer!(Ikke trykk)" /></p>
        
    </div>
    <div class="kolonner col-md-4">
        <p>Administrere brukere</p>
    </div>
    <div class="kolonner col-md-4">
        <p>Opprette Team</p>
        <p>
            <asp:Button ID="bt_nav_opprettTeam" runat="server" OnClick="bt_nav_opprettTeam_Click" Text="Opprett Team" />
        </p>
    </div>
    <div class="kolonner col-md-4">
        <p class ="redText">Feilmeldinger</p>
    </div>
    <div class="kolonner col-md-10">
        <h1>Mer kommer etterhvert! :-)</h1>
        
    </div>
    <div class="kolonner col-md-2">
       
    </div>
    <p>...</p>
</asp:Content>