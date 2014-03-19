<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArkiverOppg.aspx.cs" Inherits="SysUt14Gr03.CancelOppg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Opprett Oppgave</h2>
    <p>Oppgave id: <asp:TextBox ID="tbOppgave_id" runat="server" ReadOnly="true"></asp:TextBox></p>
    <p>Tittel: <asp:TextBox ID="tbTittel" runat="server" Height="25px"></asp:TextBox></p>
    <p> Beskrivelse av oppgave: <asp:TextBox ID="tbBeskrivelse" runat="server" Height="39px" Width="132px"></asp:TextBox></p>
   <p>Krav: <asp:TextBox ID="tbKrav" runat="server" Height="25px"></asp:TextBox></p>
    <p> Estimering<asp:TextBox ID="TbEstimering" runat="server" Height="25px" Width="160px"></asp:TextBox></p>
    <p> Tilgjengelige Brukere<asp:DropDownList ID="ddlBrukere" runat="server"></asp:DropDownList></p>
    <asp:ListBox ID="lbBrukere" runat="server" Width="157px" Enabled="False"></asp:ListBox>
    <asp:Label ID="lblFeil" runat="server" Visible="false" Text=""></asp:Label>
    <asp:Button ID="btnBrukere" Text="Legg Til Brukere" runat="server" OnClick="btnBrukere_Click" />
       <asp:GridView ID="GridViewOppg" AutoGenerateColumns="false" runat="server">
           <Columns>
               <asp:TemplateField HeaderText="Tittel">
                   <ItemTemplate>
                       <asp:Label ID="lbTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="Prosjekt">
                   <ItemTemplate>
                       <asp:Label ID="lbProsjekt" runat="server" Text='<%#Bind("Prosjekt.Navn") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </asp:GridView>
   <p> 
       &nbsp;</p>
    <p>Status<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList></p>
   <p> Prioritet<asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList></p>
   <p> Prosjekt<asp:DropDownList ID="ddlProsjekt" runat="server"></asp:DropDownList></p>
    <p>Aktiv: <asp:CheckBox ID="cbAktiv" runat="server" /></p>
    <asp:label id="lblCheck" visible="false" runat="server" ></asp:label>
    <asp:Button ID="btnEndre" runat="server" OnClick="btnEndre_Click" Text="Endre Oppgave" />
</asp:Content>
