<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvProsjekt.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvProsjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Endre Prosjekt</h2>
    <asp:GridView ID="gridViewProsjekt" cssClass="table table-hover  table-bordered" runat="server" AutoGenerateColumns="false" DataKeyNames="Prosjekt_id" OnRowDataBound="gridViewProsjekt_RowDataBound" OnRowUpdating="gridViewProsjekt_RowUpdating" OnRowCancelingEdit="gridViewProsjekt_RowCancelingEdit" OnRowEditing="gridViewProsjekt_RowEditing">
         <Columns>
            <asp:TemplateField HeaderText="ProsjektNavn">
                <ItemTemplate>
                      <asp:Label ID="lbProsjektnavn" runat="server" Text='<%#Bind("Navn") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbProsjektnavn" runat="server" Text='<%#Bind("Navn") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prosjektleder">
                <ItemTemplate>
                    <asp:Label ID="lbProsjektleder" runat="server" Text='<%#Bind("Bruker.Fornavn") %>'></asp:Label>
                </ItemTemplate>
                  <EditItemTemplate>
                      <asp:TextBox ID="tbProsjektleder" runat="server" Text='<%#Bind("Bruker.Fornavn") %>'></asp:TextBox>
                 </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Startdato">
                <ItemTemplate>
                    <asp:Label ID="lbStart" runat="server" Text='<%#Bind("Startdato") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbStart" runat="server"  Text='<%#Bind("Startdato") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sluttdato">
                <ItemTemplate>
                    <asp:Label ID="lbSlutt" runat="server" Text='<%#Bind("Sluttdato") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbSlutt" runat="server"  Text='<%#Bind("Sluttdato") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Team">
                <ItemTemplate>
                <asp:Label ID="lbTeam" runat="server" Text='<%#Bind("Team.Navn") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbTeam" runat="server" Text='<%#Bind("Team.Navn") %>'></asp:TextBox>
                    <asp:DropDownList ID="ddlTeam" runat="server" AppendDataBoundItems="true">
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
            <asp:TemplateField HeaderText="Gå til team">
                <ItemTemplate>
                    <asp:HyperLink ID="asp" Text="AdministrasjonAvTeam" runat="server"></asp:HyperLink> 
                </ItemTemplate>
                <EditItemTemplate>

                </EditItemTemplate>
            </asp:TemplateField>
             <asp:CommandField ShowEditButton="true" />
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblFeil" runat="server" Visible="false"></asp:Label>
</asp:Content>
