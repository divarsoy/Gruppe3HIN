<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="AdministrasjonAvTeam.aspx.cs" Inherits="SysUt14Gr03.AdministrasjonAvTeam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
    <h1>Administrering av Team</h1>
    <br />
        <div class="checkbox">
    <asp:CheckBoxList ID="cbl_team"  runat="server" /> 
        </div>
        <asp:Button ID="bt_endreTeam" CssClass="btn btn-primary" runat="server" Text="Endre Team" OnClick="bt_endreTeam_Click" />
        <asp:Button ID="bt_arkivereTeam" CssClass="btn btn-danger" runat="server" Text="Arkivere Team" OnClick="bt_arkivereTeam_Click" />
        <asp:Button ID="bt_aktivereTeam" CssClass="btn btn-primary" runat="server" Text="Aktiver Team" OnClick="bt_aktiverTeam_Click" />
    </div>
    <div>
        <h2><asp:Label ID="lblMessage" Visible="false" runat ="server" Text="Sikker på at du vil arkivere disse teamene?"></asp:Label></h2>
        <asp:Button ID="yes" Visible="false" runat="server" Text="Ja" CssClass="btn btn-primary" OnClick="yes_Click" />
        <asp:Button ID="no" Visible="false" runat="server" Text="Nei" CssClass="btn btn-danger" OnClick="no_Click" />
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
