﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.Master" AutoEventWireup="true" CodeBehind="EndreBrukerinformasjonSomAdministrator.aspx.cs" Inherits="SysUt14Gr03.EndreBrukerinformasjonSomAdministrator" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <h2>Oversikt brukere</h2>  
    <asp:GridView RowStyle-HorizontalAlign="Center" ID="gridViewEndre" runat="server" OnRowDataBound="gridViewEndre_RowDataBound" CssClass="table" AutoGenerateColumns="false" DataKeyNames="Bruker_id" OnRowEditing="gridViewEndre_RowEditing" OnRowCancelingEdit="gridViewEndre_RowCancelingEdit" OnRowUpdating="gridViewEndre_RowUpdating" OnRowCommand="gridViewEndre_RowCommand" ShowFooter="False" ShowHeader="True">
        <Columns>
            <asp:TemplateField HeaderText="Etternavn">
                <ItemTemplate>
                      <asp:Label ID="lblEtternavn" runat="server" Visible="true" Text='<%#Bind("Etternavn") %>' ></asp:Label>  
                      <asp:HyperLink ID="EtternavnLink" runat="server"></asp:HyperLink> 
              
                </ItemTemplate>          
                <EditItemTemplate>
                    <asp:TextBox ID="tbEtternavn" runat="server" Text='<%#Bind("Etternavn") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fornavn">
                <ItemTemplate>
                    <asp:Label ID="lblFornavn" runat="server" Visible="true" Text='<%#Bind("Fornavn") %>'></asp:Label>
                    <asp:HyperLink ID="FornavnLink" runat="server"></asp:HyperLink> 

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
             <asp:TemplateField HeaderText="Brukernavn">
                <ItemTemplate>
                    <asp:Label ID="lblBrukernavn" runat="server" Text='<%#Bind("Brukernavn") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="IM">
                <ItemTemplate>
                    <asp:Label ID="lblIM" runat="server" Text='<%#Bind("IM") %>'></asp:Label>
                </ItemTemplate>
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
            <asp:TemplateField HeaderText="Rolle">
                <ItemTemplate>
                    <asp:Label ID="lblRettighet" runat="server" ></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlRettighet" runat="server"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:CommandField ShowEditButton="true" />
        </Columns>      
    </asp:GridView>       
</asp:Content>
