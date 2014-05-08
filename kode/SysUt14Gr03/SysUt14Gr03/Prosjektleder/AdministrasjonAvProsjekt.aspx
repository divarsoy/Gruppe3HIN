<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvProsjekt.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Endre Prosjekt</h2>
    <asp:GridView ID="gridViewProsjekt" cssClass="table table-hover  table-bordered" runat="server" AutoGenerateColumns="false" DataKeyNames="Prosjekt_id" OnRowDataBound="gridViewProsjekt_RowDataBound" OnRowUpdating="gridViewProsjekt_RowUpdating" OnRowCancelingEdit="gridViewProsjekt_RowCancelingEdit" OnRowEditing="gridViewProsjekt_RowEditing">
         <Columns>
            <asp:TemplateField HeaderText="ProsjektNavn">
                <ItemTemplate>
                      <asp:Label ID="lbProsjektnavn" runat="server" Visible="false" Text='<%#Bind("Prosjekt_id") %>'></asp:Label>
                      <asp:HyperLink ID="pLink" runat="server"></asp:HyperLink> 
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbProsjektnavn" runat="server" Text='<%#Bind("Navn") %>'></asp:TextBox>
                    <asp:Label ID="lbProsjektnavn" runat="server" Visible="false" Text='<%#Bind("Prosjekt_id") %>'></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prosjektleder">
                <ItemTemplate>
                    <asp:Label ID="lbProsjektleder" runat="server" Text='<%#Bind("Bruker.Fornavn") %>'></asp:Label>
                </ItemTemplate>
                  <EditItemTemplate>
                      <asp:Label ID="lblProsjektleder" Visible="false" runat="server" Text='<%#Bind("Bruker_id") %>'></asp:Label>
                      <asp:DropDownList ID="ddlLeder" runat="server"></asp:DropDownList>
                 </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Startdato">
                <ItemTemplate>
                    <asp:Label ID="lbStart" runat="server" Text='<%#Bind("Startdato", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbStart" TextMode="Date" runat="server"  Text='<%#Bind("Startdato", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sluttdato">
                <ItemTemplate>
                    <asp:Label ID="lbSlutt" runat="server" Text='<%#Bind("Sluttdato" , "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbSlutt" TextMode="Date" runat="server"  Text='<%#Bind("Sluttdato", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>           
              <asp:TemplateField HeaderText="Team">
                <ItemTemplate>
                <asp:Label ID="lbTeam" runat="server" Text='<%#Bind("Team.Navn") %>'></asp:Label>
                    <asp:Label ID="lblTeam_id" runat="server" Visible="false" Text='<%#Bind("Team_id") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lbTeam" runat="server" Visible="false" Text='<%#Bind("Team_id") %>'></asp:Label>
                    <asp:DropDownList ID="ddlTeam" runat="server" >
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Aktiv">
                <ItemTemplate>
                    <asp:CheckBox ID="cbAktiv" Checked='<%#Bind("Aktiv") %>' Enabled="false" runat="server" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="cboxAktiv" Checked='<%#Bind("Aktiv") %>' runat="server" />
                     </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Endre Team">
                <ItemTemplate>
                    <asp:HyperLink ID="asp" Text="AdministrasjonAvTeam" runat="server"></asp:HyperLink> 
                </ItemTemplate>
                <EditItemTemplate>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Endre/legg til Fase">
                 <ItemTemplate>
                     <asp:HyperLink ID="hlFase" Text="AdministrasjonAvFaseTilProsjekt" runat="server"></asp:HyperLink>
                 </ItemTemplate>
             </asp:TemplateField>
           
             <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
