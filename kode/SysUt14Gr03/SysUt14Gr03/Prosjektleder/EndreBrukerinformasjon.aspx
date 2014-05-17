<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.Master" AutoEventWireup="true" CodeBehind="EndreBrukerinformasjon.aspx.cs" Inherits="SysUt14Gr03.EndreBrukerinformasjon" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p></p>
    <h2>Endre Brukerinformasjon</h2>
    <p></p>
    
    <asp:GridView RowStyle-HorizontalAlign="Center" cssClass="table table-hover  table-bordered" ID="gridViewEndre" runat="server" AutoGenerateColumns="false" DataKeyNames="Bruker_id" OnRowEditing="gridViewEndre_RowEditing" OnRowCancelingEdit="gridViewEndre_RowCancelingEdit" OnRowUpdating="gridViewEndre_RowUpdating" OnRowCommand="gridViewEndre_RowCommand" ShowFooter="False" ShowHeader="True">

        <Columns>
            <asp:TemplateField HeaderText="Etternavn">
                <ItemTemplate>
                      <asp:Label ID="lbEtternavn" runat="server" Text='<%#Bind("Etternavn") %>'></asp:Label>                
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbEtternavn" runat="server" Text='<%#Bind("Etternavn") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fornavn">
                <ItemTemplate>
                    <asp:Label ID="lbFornavn" runat="server" Text='<%#Bind("Fornavn") %>'></asp:Label>
                </ItemTemplate>
                  <EditItemTemplate>
                      <asp:TextBox ID="tbFornavn" runat="server" Text='<%#Bind("Fornavn") %>'></asp:TextBox>
                  </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Epost">
                <ItemTemplate>
                    <asp:Label ID="lbEpost" runat="server" Text='<%#Bind("Epost") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbEpost" runat="server"  Text='<%#Bind("Epost") %>'></asp:TextBox>
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
            <asp:TemplateField HeaderText="Aktiveringslink" Visible="false">
        <EditItemTemplate>
            <asp:Button ID="btnSendNy"  
            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>
            " CommandName="Send" runat="server" Text="Send Ny" />
        </EditItemTemplate>
        </asp:TemplateField> 
             <asp:CommandField ShowEditButton="true" />
        </Columns>
      
    </asp:GridView>
  
       
</asp:Content>
