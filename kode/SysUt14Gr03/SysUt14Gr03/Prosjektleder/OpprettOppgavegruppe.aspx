<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="OpprettOppgavegruppe.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgavegruppe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

    <h1>Opprett oppgavegruppe</h1>
        <hr />
    <br />
    
        <div class="row">
            <label class="col-md-2 control-label">Navn på gruppe:</label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="txtNavn" runat="server" placeholder="Skriv inn ønsket navn på oppgavegruppe!"></asp:TextBox>
            </div>
        </div>
    <hr />
        <div class="row">
            <div class="col-md-6">
    <asp:GridView CssClass="table table-striped" RowStyle-HorizontalAlign="Center" ID="gvwOppgaver" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwOppgaver_RowDataBound">
        <Columns>
            <asp:TemplateField  HeaderText="Oppgavegruppe">
                <ItemTemplate>
                    <asp:CheckBox ID="chbGruppe" runat="server" />
                </ItemTemplate>          
                
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Oppgave">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Oppgave_id") %>'></asp:Label>
                    <asp:Label ID="lblTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>          
                </ItemTemplate>
         
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Prioritet">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlPrioritet" runat="server"></asp:DropDownList>
                </ItemTemplate>
               
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
                </div>
            </div>
    <br />
    &nbsp&nbsp&nbsp<asp:Label ID="lblMelding" runat="server" Visible="false"></asp:Label>
    <br /><br />

    <asp:Button ID="btnOpprett" CssClass="btn btn-primary" runat="server" Text="Lagre" OnClick="btnOpprett_Click" />
    <br />
</div>
</asp:Content>
