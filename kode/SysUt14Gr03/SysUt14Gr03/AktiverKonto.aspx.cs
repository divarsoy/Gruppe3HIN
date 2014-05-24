﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;


namespace SysUt14Gr03
{
    public partial class AktiverKonto : System.Web.UI.Page
    {
        private string brukernavn;
        private string etternavn;
        private string fornavn;
        private string epost;
        private string imAdresse;
        private string token;
        private int bruker_id;
        private bool check = true;
        /// <summary>
        /// I denne klassen aktiverer man en bruker når det blir registrert en ny bruker, den sjekker
        /// først på epost og token, viss det stemmer så bli infoen som ble skrevet i registreringen hentet ut og fylt
        /// i tekstboksene(Fornavn, etternavn og epost). Blir også sjekket på brukernavn og om man har skrevet inn riktig passord to ganger.
        /// Stemmer alt så blir kontoen aktivert og du blir videresendt til login med en flashmelding som gir deg bekreftelse på at kontoen er Aktivert.
        /// </summary>

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(Request.QueryString["Epost"])) & (!string.IsNullOrEmpty(Request.QueryString["Token"])))
            {
                epost = Request.QueryString["Epost"];
                token = Request.QueryString["Token"];
                Session["flashMelding"] = "<h2 align=center> Fyll ut resterende felt for å aktivere kontoen din</h2>";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info;
            }
            else
            {
                Session["flashMelding"] = "<h2 align=center>Det skjedde noe galt, Kontoen din ble ikke aktivert!</h2>";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((!string.IsNullOrEmpty(Request.QueryString["Epost"])) & (!string.IsNullOrEmpty(Request.QueryString["Token"])))
                {
                    epost = Request.QueryString["Epost"];
                    token = Request.QueryString["Token"];
                    ActivateMyAccount();
                }
                else
                {
                    Session["flashMelding"] = "<h2 align=center>Det skjedde noe galt, Kontoen din ble ikke aktivert!</h2>";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                    Response.Redirect("~/Default");
                }
            }
        }

   /*     public static void SetBrukerFelter(string _fornavn, string _etternavn, string _epost) {
            initialFornavn = _fornavn;
            initialEtternavn = _etternavn;
            initialEpost = _epost;
        } */

        private void ActivateMyAccount()
        {

            try
            {
                if ((!string.IsNullOrEmpty(epost)) && (!string.IsNullOrEmpty(token)))
                {
                    using (var context = new Context())
                    {
                        var bruk = context.Brukere
                                    .Where(bruker => bruker.Epost == epost)
                                    .Where(bruker => bruker.Token == token)
                                    .Where(bruker => bruker.Aktivert == false)
                                    .FirstOrDefault();

                        bruker_id = bruk.Bruker_id;
                        Email.Text = epost;
                        Firstname.Text = bruk.Fornavn;
                        Aftername.Text = bruk.Etternavn;
                    }
                   
                }
                else
                {
                    Session["flashMelding"] = "Det skjedde noe galt ved aktivering av din konto. Ta kontakt med <a href='mailto:SysUt14Gr03@gmail.com'>SysUt14Gr03@gmail.com</a>";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                    Response.Redirect("~/Default", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                Response.Redirect("~/Default");
                return;
                
            }
        } 
     
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            lblFeilBrukernavn.Visible = false;
            lblPassord.Visible = false;
            //sjekker om passordene er like
            if (Password.Text == ConfirmPassword.Text)
            {
                //token = Request.QueryString["Token"];
                //passord = MD5Hash(Password.Text);
                //passord = Passord.HashPassord(Password.Text);
                string salt = Hash.GetSalt();
                string hash = Hash.GetHash(Password.Text, salt);
                string nyEpost = Email.Text;
                brukernavn = Username.Text;
                etternavn = Aftername.Text;
                fornavn = Firstname.Text;
                imAdresse = Im_adress.Text;
                // int id = bruker_id;

                using (var db = new Context())
                {
                    var brukerSomSkalLagres = db.Brukere
                                                .Where(bruker => bruker.Epost == epost)
                                                .Where(bruker => bruker.Token == token)
                                                .Where(bruker => bruker.Aktivert == false)
                                                .FirstOrDefault();

                    // Default rettighet er utvikler
                    // string rettighetUtviklerString = Konstanter.rettighet.Utvikler.ToString();
                    // var rettighetUtvikler = db.Rettigheter.Where(rettighet => rettighet.RettighetNavn == rettighetUtviklerString).FirstOrDefault();
                    //Sjekker brukernavn
                    var queryBrukernavn = db.Brukere.FirstOrDefault(b => b.Brukernavn == brukernavn);
                    if (queryBrukernavn != null)
                    {
                        lblFeilBrukernavn.Text = "Brukernavnet er allerede tatt";
                        lblFeilBrukernavn.Visible = true;
                        lblFeilBrukernavn.ForeColor = Color.Red;
                   /*     Session["flashMelding"] = String.Empty;
                        Session["flashMelding"] = "Brukernavnet er allerede tatt";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                       */
                        check = false;
                    }
                    if (check)
                    {
                        brukerSomSkalLagres.Aktiv = true;
                        brukerSomSkalLagres.Aktivert = true;
                        brukerSomSkalLagres.Brukernavn = brukernavn;
                        brukerSomSkalLagres.Epost = nyEpost;
                        brukerSomSkalLagres.Etternavn = etternavn;
                        brukerSomSkalLagres.IM = imAdresse;
                        brukerSomSkalLagres.Passord = hash;
                        brukerSomSkalLagres.Salt = salt;
                        db.SaveChanges();
                       
                      //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Kontoen din er aktivert, du vil bli videresendt til logg inn siden');", true);
                        Session["flashMelding"] += "Kontoen din er aktivert, du kan nå logge inn";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.success;
                        //Response.AddHeader("REFRESH", "2;URL=Login.aspx");
                        Response.Redirect("Login.aspx");
                    }
                }
            }
            else
            {
                /*Session["flashMelding"] = "Vennligst fyll inn like passord";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger; */
                lblPassord.Text = "Passordene må være like!";
                lblPassord.Visible = true;
                lblPassord.ForeColor = Color.Red;
                Username.Text = "";
                Im_adress.Text = "";
                Password.Text = "";
                ConfirmPassword.Text = "";
                //Response.Redirect(Request.RawUrl);
            }
        }
                
    } 
}
