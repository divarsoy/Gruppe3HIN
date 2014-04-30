<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="AdministrasjonAvFase.aspx.cs" Inherits="SysUt14Gr03.Prosjektleder.AdministrasjonAvFase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Endre/Legge Til Faser</h2>
    <asp:GridView ID="gridViewFase" CssClass="table table-hover  table-bordered" ShowFooter="True" runat="server" AutoGenerateColumns="false" DataKeyNames="Fase_id" OnRowCancelingEdit="gridViewFase_RowCancelingEdit" OnRowEditing="gridViewFase_RowEditing" OnRowUpdating="gridViewFase_RowUpdating" OnRowDataBound="gridViewFase_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="FaseNavn">
                <ItemTemplate>
                    <asp:Label ID="lblFase" runat="server" Visible="true" Text='<%#Bind("Navn") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbFase" runat="server" Text='<%#Bind("Navn") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="tbNyFase" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Start">
                <ItemTemplate>
                    <asp:Label ID="lblStart" runat="server" Visible="true" Text='<%#Bind("Start", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbStart" TextMode="Date" runat="server" Text='<%#Bind("Start", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                    
                </EditItemTemplate>

                <FooterTemplate>
                    <asp:TextBox ID="tbStartny" TextMode="Date" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stopp">
                <ItemTemplate>
                    <asp:Label ID="lblStopp" runat="server" Visible="true" Text='<%#Bind("Stopp", "{0:dd/MM/yyyy}") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbStopp" runat="server" TextMode="Date" Text='<%#Bind("Stopp", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="tbStoppny" TextMode="Date" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Faseleder">
                <ItemTemplate>
                    <asp:Label ID="lblBruker_id" runat="server" Visible="false" Text='<%#Bind("Bruker_id") %>'></asp:Label>
                    <asp:Label ID="lblBrukere" runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="Bruker_id" runat="server" Visible="false" Text='<%#Bind("Bruker_id") %>'></asp:Label>
                    <asp:DropDownList ID="ddlFaseleder" runat="server"></asp:DropDownList>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:DropDownList ID="ddlFaseledere" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnLagre" runat="server" CssClass="btn btn-primary" Text="Lagre Ny" OnClick="btnLagre_Click" />
                </FooterTemplate>
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
    </asp:GridView>
</asp:Content>
