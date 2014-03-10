<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArkiverOppg.aspx.cs" Inherits="SysUt14Gr03.CancelOppg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br/><br/>
    <asp:GridView RowStyle-HorizontalAlign="Center" ID="gridViewOppgave" runat="server" AutoGenerateColumns="false" DataKeyNames="Gruppe_id" OnRowEditing="gridViewGroup_RowEditing" OnRowCancelingEdit="gridViewGroup_RowCancelingEdit" OnRowUpdating="gridViewGroup_RowUpdating" ShowFooter="False" ShowHeader="True">         
        <Columns>
            <asp:TemplateField HeaderText="Tittel">
                <ItemTemplate>
                      <asp:Label ID="lbTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estimat">
                <ItemTemplate>
                      <asp:Label ID="lbEstimat" runat="server" Text='<%#Bind("Estimat") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbEstimat" runat="server" Text='<%#Bind("Estimat") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Brukt tid">
                <ItemTemplate>
                      <asp:Label ID="lbBruktTid" runat="server" Text='<%#Bind("Brukttid") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbBruktTid" runat="server" Text='<%#Bind("Brukttid") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tidsfrist">
                <ItemTemplate>
                      <asp:Label ID="lbTidsfrist" runat="server" Text='<%#Bind("Tidsfrist") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbTidsfrist" runat="server" Text='<%#Bind("Tidsfrist") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Opprettet">
                <ItemTemplate>
                      <asp:Label ID="lbOpprettet" runat="server" Text='<%#Bind("Opprettet") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbOpprettet" runat="server" Text='<%#Bind("Opprettet") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prosjekt">
                <ItemTemplate>
                      <asp:Label ID="lbProsjekt" runat="server" Text='<%#Bind("Prosjekt") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbProsjekt" runat="server" Text='<%#Bind("Prosjekt") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                      <asp:Label ID="lbStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbStatus" runat="server" Text='<%#Bind("Status") %>'></asp:TextBox>
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
