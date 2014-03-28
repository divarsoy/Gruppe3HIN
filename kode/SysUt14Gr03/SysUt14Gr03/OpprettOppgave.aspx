<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Opprett Oppgave</h2>
    <h3>Prosjekt: </h3><asp:Label ID="lblProsjekt" runat="server"></asp:Label>
    <p>Tittel: <asp:TextBox ID="tbTittel" runat="server" Height="25px"></asp:TextBox></p>
    <p> Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server" Height="39px" Width="132px"></asp:TextBox></p>
   <p>Krav: <asp:TextBox ID="tbKrav" runat="server" Height="25px"></asp:TextBox></p>
    <p> Estimering<asp:TextBox ID="TbEstimering" runat="server" Height="25px" Width="160px"></asp:TextBox></p>
    <p> <asp:Calendar ID="cal" runat="server" Width="293px"></asp:Calendar>
    <asp:TextBox ID="tbFrist" runat="server"></asp:TextBox><asp:Button ID="btnSett" runat="server" Text="Sett Tidsfrist" OnClick="btnSett_Click" /></p>
    <p> Tilgjengelige Brukere<asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList></p>
    <asp:ListBox ID="lbBrukere" runat="server" Width="157px" Enabled="False"></asp:ListBox>
    <asp:Label ID="lblFeil" runat="server" Visible="false" Text=""></asp:Label>
    <asp:Button ID="btnBrukere" Text="Legg Til Brukere" runat="server" OnClick="btnBrukere_Click" />
    <h3>Oversikt over oppgaver i prosjektet</h3>
  
       <asp:GridView ID="GridViewOppg" AutoGenerateColumns="false" runat="server" CssClass="table">
           <Columns>
               <asp:TemplateField HeaderText="Prosjekt">
                   <ItemTemplate>
                       <asp:Label ID="lbProsjekt" runat="server" Text='<%#Bind("Prosjekt.Navn") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="Oppgaver i valgt prosjekt">
                   <ItemTemplate>
                       <asp:Label ID="lbTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </asp:GridView>

  Status<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
   <p> Prioritet<asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList></p>
    <asp:label id="lblCheck" visible="false" runat="server" ></asp:label>
    <asp:Button ID="btnOpprett" runat="server" OnClick="btnOpprett_Click" Text="Opprett Oppgave" />
</asp:Content>
