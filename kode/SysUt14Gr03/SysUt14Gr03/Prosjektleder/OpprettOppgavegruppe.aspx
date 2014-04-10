<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="OpprettOppgavegruppe.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgavegruppe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <h1>Opprett oppgavegruppe</h1>
    <br />
    Navn på gruppe:<asp:TextBox ID="txtNavn" runat="server"></asp:TextBox>
    <br />
    <hr />
    <asp:GridView CssClass="table" ID="gvwOppgaver" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwOppgaver_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Oppgavegruppe">
                <ItemTemplate>
                    <asp:CheckBox ID="chbGruppe" runat="server" />
                </ItemTemplate>          
                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Oppgave">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Oppgave_id") %>'></asp:Label>
                    <asp:Label ID="lblTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>          
                </ItemTemplate>
                 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Prioritet">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="lblMelding" runat="server" Visible="false"></asp:Label>
    <br />

    <asp:Button ID="btnOpprett" runat="server" Text="Lagre" OnClick="btnOpprett_Click" />
    <asp:Button ID="Button2" runat="server" Text="Button" />
    <br />

</asp:Content>
