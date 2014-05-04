<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="GodkjennTimer.aspx.cs" Inherits="SysUt14Gr03.GodkjennTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>

    <asp:GridView CssClass="table table-striped" RowStyle-HorizontalAlign="Center" ID="gvwTimer" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwTimer_RowDataBound" OnRowCommand="gvwTimer_RowCommand">
      
           <Columns>

            <asp:TemplateField  HeaderText="Detaljer">
                
                <ItemTemplate>
                    
                    <asp:Label ID="lblTittel" runat="server" Text='<%#Container.DataItem %>'></asp:Label>          
                </ItemTemplate>
         
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Godkjenn">
                <ItemTemplate>
                    <asp:Button ID="btnGodkjenn" runat="server" Text="Godkjenn" CssClass="btn btn-success"
                        CommandName="Godkjenn" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
               
            </asp:TemplateField>

               <asp:TemplateField  HeaderText="Korriger">
                <ItemTemplate>
                    <asp:Button ID="btnKorriger" runat="server" Text="Korriger" CssClass="btn btn-warning"
                        CommandName="Korriger" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                </ItemTemplate>
               
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
