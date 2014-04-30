<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Prosjektleder.master" AutoEventWireup="true" CodeBehind="OpprettOppgave.aspx.cs" Inherits="SysUt14Gr03.OpprettOppgave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class ="container">
        
        <h1>Opprett Oppgave</h1>
            

        <div class="form-group">
            <h3 style="display: inline-block">Prosjekt: <asp:Label ID="lblProsjekt" runat="server"></asp:Label> </h3>     
        </div>

        <div class="form-group">
            <div class="row">
                <div style="display: inline-block" class="col-md-4">
                    
                        <asp:TextBox ID="tbID" CssClass="form-control" runat="server" placeholder="ID på oppgave!"></asp:TextBox>
                        <asp:TextBox ID="tbTittel" CssClass="form-control" runat="server" placeholder="Tittel på oppgave!"></asp:TextBox>
                        <asp:Textbox ID="tbBeskrivelse" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Beskrivelse av oppgaven!"></asp:Textbox>
                        <asp:TextBox ID="tbKrav" CssClass="form-control" runat="server"  placeholder="Krav til oppgaven!"></asp:TextBox>
                        <asp:TextBox ID="TbEstimering" TextMode="Number" CssClass="form-control" runat="server"  placeholder="Estimering av tid på oppgave!"></asp:TextBox>
                        <asp:DropDownList ID="ddlFaser" CssClass="form-control" runat="server"></asp:DropDownList>
                        &nbsp&nbsp&nbsp<label>Tilgjengelige Brukere</label>
                        <asp:DropDownList ID="ddlBrukere" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:ListBox ID="lbBrukere" CssClass="form-control" runat="server" Height="100px"  Enabled="True"></asp:ListBox>
                        <asp:Button ID="btnBrukere" CssClass="btn btn-primary" Text="Legg Til Brukere" runat="server" OnClick="btnBrukere_Click" />
                        <asp:Button ID="btnFjernBruker" CssClass="btn btn-warning" runat="server" Text="Fjern bruker" OnClick="btnFjernBruker_Click" />
                        <br />
                        &nbsp&nbsp&nbsp<asp:Label ID="lblFeil" runat="server" Visible="false" Text=""></asp:Label>
                    
                </div>
        
                <div class="col-md-4 col-md-offset-1">
                    <asp:Calendar Width="300px" ID="cal" BorderColor="Blue" ShowGridLines="true" NextMonthText="Neste" PrevMonthText="Forrige" TodayDayStyle-BackColor="Blue" TodayDayStyle-ForeColor="White" WeekendDayStyle-ForeColor="Red" runat="server" ></asp:Calendar>
                    <br />
                    <asp:TextBox ID="tbFrist" CssClass="form-control" width="280px" runat="server" placeholder="Tidsfrist på oppgave!"></asp:TextBox>
                    <asp:CompareValidator id="CompareValidator6" runat="server" Display="Dynamic" Type="Date" Operator="DataTypeCheck" ControlToValidate="tbFrist"
                        ErrorMessage="<br />Du må taste inn en gyldig dato" CssClass="feilMelding"></asp:CompareValidator>
                    <br />
                    <asp:Button ID="btnSett" CssClass="btn btn-primary" runat="server" Text="Sett Tidsfrist" OnClick="btnSett_Click" />
                </div>
            </div>
        </div>
    <br />
    <h3>Oversikt over oppgaver i prosjektet</h3>
        
       <asp:GridView ID="GridViewOppg" AutoGenerateColumns="false" runat="server" CssClass="table table-striped">
           <Columns>
               <asp:TemplateField HeaderText="Prosjekt">
                   <ItemTemplate>
                       <asp:Label ID="lbProsjekt" runat="server" Text='<%#Bind("Prosjekt.Navn") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="Oppgaver i valgt prosjekt">
                   <ItemTemplate>
                       <asp:Label ID="lbTittel" runat="server" Text='<%#Bind("Tittel") %>'></asp:Label>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </asp:GridView>

        

                
            
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
   
        
</asp:Content>
