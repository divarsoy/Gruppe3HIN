﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.SysAdm.Master" AutoEventWireup="true" CodeBehind="Rettigheter.aspx.cs" Inherits="SysUt14Gr03.Rettigheter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <h1>Legge til/Endre rettigheter</h1>
        <br />

            <asp:GridView ID="GridViewRettigheter" class="table table-hover table-bordered" width="500px" runat="server" AutoGenerateColumns="False" DataKeyNames="rettighet_id" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" ShowFooter="True" ShowHeader="False">
                
                <Columns>
                    
                    <asp:TemplateField>
                        
                        <ItemTemplate>
                            <asp:Label ID="lblRettighetNavn" runat="server" Text='<%# Bind("RettighetNavn") %>' />
                        </ItemTemplate>
                            
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRettighetNavn" runat="server" Text='<%# Bind("RettighetNavn") %>' />
                            </EditItemTemplate>
                                
                                <FooterTemplate>
                                    <label for="leggTilNy">Legg til rettighet: </label>
                                    <asp:TextBox ID="txtNyRettighetNavn" runat="server" />
                                    <asp:Button ID="btnLagre" class="btn btn-primary" runat="server" Text="Lagre ny" OnClick="btnLagre_Click"/>
                                </FooterTemplate>
                    
                    </asp:TemplateField>
                        <asp:CommandField ShowEditButton="true" />
                
                </Columns>
            
            </asp:GridView>

            

    </div>

</asp:Content>