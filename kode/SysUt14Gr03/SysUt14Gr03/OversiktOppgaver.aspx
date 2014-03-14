<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OversiktOppgaver.aspx.cs" Inherits="SysUt14Gr03.OversiktOppgaver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Liste over oppgaver</h1>
    <Table class="table">
        <tr>
            <th>Tittel</th>
            <th>Prosjekt_id</th>
            <th>Estimat</th>
            <th>Gjenstående tid</th>
            <th>Brukt tid</th>
            <th>Status</th>
            <th>Bruker</th>
        </tr>

        <asp:ListView ID="ListView1" runat="server" ItemType="SysUt14Gr03.Models.Oppgave"> 
            <ItemTemplate>
                <tr>
                    <td><%#:Item.Tittel%></td>
                    <td><%#:Item.Prosjekt_id %></td>
                    <td><%#:Item.Estimat %></td>
                    <td><%#:Item.RemainingTime %></td>
                    <td><%#:Item.BruktTid%></td>
                    <td><%#:Item.Status_id%></td>
                    <td><%#:Item.Brukere%></td>
                     

                </tr>
            </ItemTemplate>
        </asp:ListView>
    </Table>
</asp:Content>
