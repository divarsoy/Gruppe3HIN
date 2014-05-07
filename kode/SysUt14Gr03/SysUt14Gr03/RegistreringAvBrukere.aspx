<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="RegistreringAvBrukere.aspx.cs" Inherits="SysUt14Gr03.RegistreringAvBrukere" %>

<asp:Content ID="Content1" class="form-horizontal" role="form" ViewStateMode="Enabled" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

    <h1>Opprett ny bruker</h1>
        <br />
            <div class="form-group">
                <label for="ddlRettighet" class="col-sm-1 control-label">Rettighet</label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlRettighet" runat="server" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem Text="Velg Rettighet" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix"></div>
            <div class="form-group">
                <label for="inputEtternavn" class="col-sm-1 control-label">Etternavn</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tb_reg_etternavn" placeholder="Tast inn brukers etternavn!" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
            <div class="clearfix"></div>


            <div class="form-group">
                <label for="inputFornavn" class="col-sm-1 control-label">Fornavn</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tb_reg_fornavn" placeholder="Tast inn brukers fornavn!" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
            </div>
            <div class="clearfix"></div>

                

            <div class="form-group">
                <label for="inputEpost" class="col-sm-1 control-label">Epost</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="tb_reg_epost" placeholder="Tast inn brukers e-post!" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>                   
            <div class="clearfix"></div>
                
            <div class="form-group">
                <div class="col-sm-offset-1 col-sm-4">
                    <p><asp:Button ID="bt_adm_reg" CssClass="btn btn-primary" runat="server" Text="Registrer Bruker" OnClick="bt_adm_reg_Click" /></p>
                </div>
            </div>
            <div class="clearfix"></div>
    </div>
        
</asp:Content>

