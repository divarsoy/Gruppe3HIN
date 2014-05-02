<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Utvikler.master" AutoEventWireup="true" CodeBehind="GodkjennTimer.aspx.cs" Inherits="SysUt14Gr03.GodkjennTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LogoTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MenyTemplate" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label ID="lblTittel" runat="server"></asp:Label>
    </h1>

    <asp:GridView CssClass="table table-striped" RowStyle-HorizontalAlign="Center" ID="gvwTimer" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvwTimer_RowDataBound">
      
           <Columns>

            <asp:TemplateField  HeaderText="Timeregistrering">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("Bruker") %>'></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("Opprettet") %>'></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text='<%#Bind("Start") %>'></asp:Label>
                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("Stopp") %>'></asp:Label>
                    <asp:Label ID="lblTittel" runat="server" Text='<%#Bind("Tid") %>'></asp:Label>          
                </ItemTemplate>
         
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="Godkjenn">
                <ItemTemplate>
                    <asp:Button ID="btnGodkjenn" runat="server" Text="Godkjenn" />
                </ItemTemplate>
               
            </asp:TemplateField>

               <asp:TemplateField  HeaderText="Korriger">
                <ItemTemplate>
                    <asp:Button ID="btnKorriger" runat="server" Text="Button" />
                </ItemTemplate>
               
            </asp:TemplateField>


            
        </Columns>
    </asp:GridView>
</asp:Content>
