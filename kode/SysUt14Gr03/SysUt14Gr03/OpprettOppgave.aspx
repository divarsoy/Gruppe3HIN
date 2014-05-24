<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class ="container">
        
        <h1>Opprett Oppgave</h1>

        <div class="form-group">
            <div class="row">
                <div style="display: inline-block" class="col-md-4">
                        <label for="tbID">Oppgave ID</label>
                        <asp:TextBox ID="tbID" CssClass="form-control" runat="server" placeholder="ID på oppgaven"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorOppgaveID" Display="Dynamic" runat="server" CssClass="feilMelding" ErrorMessage="Må fylles ut!<br />" ControlToValidate="tbID"></asp:RequiredFieldValidator>
                        
                        <label for="tbTittel">Oppgave Tittel</label>
                        <asp:TextBox ID="tbTittel" CssClass="form-control" runat="server" placeholder="Tittel på oppgaven"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorTittel" Display="Dynamic" runat="server" CssClass="feilMelding" ErrorMessage="Må fylles ut!<br />" ControlToValidate="tbTittel"></asp:RequiredFieldValidator>
                        
                        <label for ="tbBeskrivelse">Beskrivelse</label>    
                        <asp:Textbox ID="tbBeskrivelse" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Beskrivelse av oppgaven"></asp:Textbox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorBeskrivelse" Display="Dynamic" runat="server" CssClass="feilMelding" ErrorMessage="Må fylles ut!<br />" ControlToValidate="tbBeskrivelse"></asp:RequiredFieldValidator>

                        <label for="tbKrav">Krav</label>    
                        <asp:TextBox ID="tbKrav" CssClass="form-control" runat="server"  placeholder="Krav til oppgaven"></asp:TextBox>

                        <label for="TbEstimering">Estimert tid</label>
                        <asp:TextBox ID="TbEstimering" TextMode="Time" CssClass="form-control" runat="server"  placeholder="Tid i formatet 00:00"></asp:TextBox>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorEstimering" Display="Dynamic" runat="server" CssClass="feilMelding" ErrorMessage="Må fylles ut!<br />" ControlToValidate="tbEstimering"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator id="RegularExpressionValidatorEstimertTid" Display="Dynamic" runat="server" CssClass="feilMelding" ControlToValidate="tbEstimering" ErrorMessage="Tidsestimatet må være riktig format!<br />" ValidationExpression="^(2[0-3]|1[0-2]|0[0-9]):[0-5][0-9]$"></asp:RegularExpressionValidator>

                        <label for ="ddlFaser">Fase</label>
                        <asp:DropDownList ID="ddlFaser" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator id="RequiredFieldValidatorFaser" runat="server" ErrorMessage="Fase må velges!</br>" CssClass="feilMelding" ControlToValidate="ddlFaser" InitialValue="0"></asp:RequiredFieldValidator>

                        <label for="ddlBrukere">Tilgjengelige Brukere</label>
                        <asp:DropDownList ID="ddlBrukere" CssClass="form-control" runat="server"></asp:DropDownList>
                        <label for="lbBrukere">Valgte brukere</label>
                        <asp:ListBox ID="lbBrukere" CssClass="form-control" runat="server" Height="100px"  Enabled="True"></asp:ListBox>
                        <asp:Button ID="btnBrukere" CssClass="btn btn-primary" Text="Legg Til Brukere" runat="server" OnClick="btnBrukere_Click" />
                        <asp:Button ID="btnFjernBruker" CssClass="btn btn-warning" runat="server" Text="Fjern bruker" OnClick="btnFjernBruker_Click" />
                        <br />
                        <asp:Label ID="lblFeil" runat="server" Visible="false" Text=""></asp:Label>
                        <br />
                    
                </div>
        
                <div class="col-md-4 col-md-offset-1">
                    <asp:Calendar Width="300px" ID="cal" BorderColor="Blue" ShowGridLines="true" NextMonthText="Neste" PrevMonthText="Forrige" TodayDayStyle-BackColor="Blue" TodayDayStyle-ForeColor="White" WeekendDayStyle-ForeColor="Red" runat="server" ></asp:Calendar>
                    <br />
                    <label for="tbFrist">Tidsfrist på oppgaven</label>
                    <asp:TextBox ID="tbFrist" CssClass="form-control" width="280px" runat="server" placeholder="Dato i formatet 31.12.2014"></asp:TextBox>
                    <asp:CompareValidator id="CompareValidator6" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFrist"
                        ErrorMessage="<br />Du må taste inn en gyldig dato" CssClass="feilMelding"></asp:CompareValidator>
                    <br />
                    <asp:Button ID="btnSett" CssClass="btn btn-primary" runat="server" Text="Sett Tidsfrist" OnClick="btnSett_Click" />
                </div>
            </div>
        </div>
        <div class="col-sm-2">
            <label >Status på oppgave</label>
        </div>
        <asp:DropDownList ID="ddlStatus" style="display: inline-block" CssClass="form-control" Width="150px" runat="server"></asp:DropDownList>
            <br />
        <div class="col-sm-2">
            <label >Prioritet</label>
        </div>
            <asp:DropDownList ID="ddlPrioritet" style="display: inline-block" CssClass="form-control" Width="80px" runat="server"></asp:DropDownList>
            <br />
            <asp:Button ID="btnOpprett" CssClass="btn btn-primary" runat="server" OnClick="btnOpprett_Click" Text="Opprett Oppgave" />   
        </div>
    <br />
    <h3>Oversikt over oppgaver i prosjektet</h3>
    <asp:PlaceHolder runat="server" ID="oppgaveListeTable"></asp:PlaceHolder>
             
</asp:Content>
