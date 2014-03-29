<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeam.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
    <h1>Administrering av Team</h1>
    <br />
        <div class="checkbox">
    <asp:CheckBoxList ID="cbl_team"  runat="server" /> 
        </div>
        <asp:Button ID="bt_endreTeam" CssClass="btn btn-primary" runat="server" Text="Endre Team" OnClick="bt_endreTeam_Click" />
        <asp:Button ID="bt_arkivereTeam" CssClass="btn btn-danger" runat="server" Text="Arkivere Team" />
        </div>
        <%-- <asp:GridView ID="gridViewProsjekt" runat="server" AutoGenerateColumns="false" DataKeyNames="Team_id" OnRowUpdating="gridViewProsjekt_RowUpdating" OnRowCancelingEdit="gridViewProsjekt_RowCancelingEdit" OnRowEditing="gridViewProsjekt_RowEditing">
        <Columns>
                <asp:Label ID="lbTeam" runat="server" Text='<%#Bind("Team.Navn") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbTeam" runat="server" Text='<%#Bind("Team.Navn") %>'></asp:TextBox>
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
             <asp:CommandField ShowEditButton="true" />
        </Columns>
    </%> --%>

</asp:Content>
