<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArkiverGruppe.aspx.cs" Inherits="SysUt14Gr03.ArkiverGruppe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/><br/>
    <asp:GridView RowStyle-HorizontalAlign="Center" ID="gridViewGroup" runat="server" AutoGenerateColumns="false" DataKeyNames="Gruppe_id" OnRowEditing="gridViewGroup_RowEditing" OnRowCancelingEdit="gridViewGroup_RowCancelingEdit" OnRowUpdating="gridViewGroup_RowUpdating" ShowFooter="False" ShowHeader="True">         
        <Columns>
            <asp:TemplateField HeaderText="Team Navn">
                <ItemTemplate>
                      <asp:Label ID="lbNavn" runat="server" Text='<%#Bind("Navn") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbNavn" runat="server" Text='<%#Bind("Navn") %>'></asp:TextBox>
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
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
</asp:Content>