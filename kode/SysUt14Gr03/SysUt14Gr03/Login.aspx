<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SysUt14Gr03.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentTemplate" Runat="Server">
    <div class="container">
        <h1>Logg inn</h1>
        <div class="ettellerannet">
            <div class ="form-group">
                <label for="epostInput" class="col-sm-2 control-label">E-postadresse</label>
                <div class="col-sm-4">
                    <input type="email" runat="server" class="form-control" id="UserName" placeholder="Tast inn E-Postadressen din">
                    <%-- <asp:TextBox ID="UserName" runat="server"></asp:TextBox> --%>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="form-group">
                <label for="passordInput" class="col-sm-2 control-label">Passord</label>
                <div class="col-sm-4">
                    <input type="password" runat="server" class="form-control" id="Password" placeholder="Tast inn ditt passord">
                    <%-- <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox> --%>
                </div>
            </div>
            <div class="clearfix"></div>

            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-10">
                    <div class="checkbox">
                        <label>
                            <input type="checkbox" id="RememberMe" runat="server"> Husk meg
                        </label>
                    </div>
                </div>
                <%-- <asp:CheckBox ID="RememberMe" runat="server" Text="Husk meg" /> --%>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button CssClass="btn btn-primary" ID="LoginButton" runat="server" Text="Logg inn" OnClick="LoginButton_Click" />
            </div>
        </div>
 
        <div class="form-group">
            <asp:Label ID="InvalidCredentialsMessage" runat="server" ForeColor="Red" Text="Feil brukernavn eller passord"
                Visible="False"></asp:Label> 
        </div>
    </div> 
</asp:Content>
