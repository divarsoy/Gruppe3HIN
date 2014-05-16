<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AktiverKonto.aspx.cs" Inherits="SysUt14Gr03.AktiverKonto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="headerCenter">
            <hr />
        
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblUsername" AssociatedControlID="Username" runat="server">Brukernavn:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Username" runat="server"></asp:TextBox>
               
             </div>
             <asp:Label ID="lblFeilBrukernavn" runat="server" Visible="false"></asp:Label>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Username" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>
               
   
  

                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblAftername" AssociatedControlID="Aftername" runat="server">Etternavn:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Aftername" runat="server"></asp:TextBox>
            </div>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Aftername" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

   
        
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblFirstname" AssociatedControlID="Firstname" runat="server">Fornavn:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Firstname" runat="server"></asp:TextBox>
            </div>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Firstname" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

     
       
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblEmail" AssociatedControlID="Email" runat="server">Epost:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Email" runat="server"></asp:TextBox>
            </div>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Email" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblImadress" AssociatedControlID="Im_adress" runat="server">IM Adresse:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Im_adress" runat="server"></asp:TextBox>
            </div>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Im_adress" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>

    
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblPassword" AssociatedControlID="Password" runat="server">Passord:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="Password" TextMode="Password" runat="server"></asp:TextBox>
            </div>
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Password" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>
            
                <asp:Label CssClass="col-md-2 col-md-offset-3" ID="lblConfirmPassword" AssociatedControlID="Password" runat="server">Bekreft passord:</asp:Label>
            <div class="col-md-4">
                <asp:TextBox CssClass="form-control" ID="ConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                <asp:Label ID="lblPassord" runat="server" Visible="false"></asp:Label>
            </div>
          
                <asp:RequiredFieldValidator ForeColor="#cc0000" CssClass="col-md-2" runat="server" ControlToValidate="Password" ErrorMessage="Feltet kan ikke være tomt"></asp:RequiredFieldValidator>
            <div style="display:inline-block" class="col-md-2 col-md-offset-5">
                <asp:Button CssClass="btn btn-primary" ID="ConfirmButton" runat="server" Text="Fullfør registrering" OnClick="ConfirmButton_Click" />
            </div> 
        </div>
    </div>
</asp:Content>
