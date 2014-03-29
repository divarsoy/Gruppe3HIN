<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeamBrukere.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeamBrukere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h1 id="Headline_endre_team">Endre team</h1>

           
            <div class="col-md-0">
                
                <label for="lblEndreTeamNavn">Team-Navn </label>
                <asp:TextBox ID="tb_TeamNavn" runat="server" Height="22px" Width="146px"></asp:TextBox>

            </div>
            
    
            
            <div class="col-md-6">
                <asp:CheckBoxList ID="cbl_TeamBrukere" CssClass="epost_pref_tabell" runat="server" OnSelectedIndexChanged="cbl_TeamBrukere_SelectedIndexChanged">
                </asp:CheckBoxList>
            </div>
           

            
            <div class="col-md-6">
                <asp:CheckBoxList ID="cbl_brukere" CssClass="epost_pref_tabell" runat="server">
                </asp:CheckBoxList>
            </div>
            

            
            <div class="col-md-0">
            </div>

           
            <div class="col-md-6">
                 <asp:Button ID="bt_fjerneBruker" CssClass="btn btn-warning" runat="server" Text="Fjern bruker" OnClick="bt_fjerneBruker_Click" />     
            </div>
            
        
            
            <div class="col-md-6">
                 <asp:Button ID="bt_leggeTilBruker" CssClass="btn btn-primary" runat="server" Text="Legg til bruker" OnClick="bt_leggeTilBruker_Click" />  
            </div>
            

    </div>
</asp:Content>
