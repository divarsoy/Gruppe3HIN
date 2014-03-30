﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class RegistreringAvBrukere : System.Web.UI.Page
    {
        private string etternavn;
        private string fornavn;
        private string epost;
        private bool emailUnq = true;
        private string password = "blahimmel";
        private MailMessage msg;
    
        private string subject;
        //private Classes.sendEmail sendMsg
        private string ActivationUrl;
        private string email;

        protected void Page_Load(object sender, EventArgs e)
        {
   
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost)
        {
            using (var db = new Context())
            {
                var nyBruker = new Bruker { Etternavn = etternavn, Fornavn = fornavn, Epost = epost, Brukernavn = "", IM = "", Token = "", Aktivert = false, Aktiv = false, opprettet = DateTime.Now };
                db.Brukere.Add(nyBruker);
                db.SaveChanges();
            }
        } 

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            if (tb_reg_etternavn.Text.Length < 256)
                etternavn = tb_reg_etternavn.Text;
            else
                FeilmeldingEtternavn.Visible = true;
            if (tb_reg_fornavn.Text.Length < 256)
                fornavn = tb_reg_fornavn.Text;
            else
                FeilMeldingFornavn.Visible = true;

            using (var context = new Context())
            {
                
                var query = context.Brukere.FirstOrDefault(bruker =>bruker.Epost == tb_reg_epost.Text);
                if (query != null)
                {
                    emailUnq = false;
//                    string _epost = query.Epost;
                }
                    
               /* string nyEpost = tb_reg_epost.Text;
                if(_epost == tb_reg_epost.Text)
                {
                    emailUnq = false;
                } */
            }
                
          /*  for (int i = 0; i < Queries.GetAlleAktiveBrukere().Count; i++)
            {
                using (var context = new Context())
                {
                    Bruker _bruker = context.Brukere.Find(i);
                    Epost _epost = _bruker.Epost;

                    if (_epost == tb_reg_epost.Text)
                        emailUnq = false;
                }
                
            } */

            if (emailUnq && tb_reg_epost.Text.Length < 256) {
                epost = tb_reg_epost.Text;
             opprettBruker(etternavn, fornavn, epost);
                //AktiverKonto.SetBrukerFelter(fornavn, etternavn, epost);
                EpostFullforReg();
          
            }    
            else
                FeilMeldingEpost.Visible = true;
        }

        public void EpostFullforReg()
        {
            try
            {
                Guid token = Guid.NewGuid();
                //   msg = new MailMessage();
                email = tb_reg_epost.Text;
                subject = "Bekreftelses epost for konto aktivering";

                //Rett link tips?
<<<<<<< HEAD:kode/SysUt14Gr03/SysUt14Gr03/RegistreringAvBrukere.aspx.cs
                ActivationUrl = Server.HtmlEncode("http://malmen.hin.no/SysUt14Gr03/AktiverKonto?Epost=" + email + "&Token=" + token);
=======
                ActivationUrl = Server.HtmlEncode("http://Malmen.hin.no/SysUt14Gr03/AktiverKonto?Epost=" + email + "&Token=" + token);
>>>>>>> 0a73fc480dceb589743a5201f761963b7fe04288:kode/SysUt14Gr03/SysUt14Gr03/Prosjektleder/RegistreringAvBrukere.aspx.cs
            
                //sendEmail.sendEpost(email, msg, subject, ActivationUrl, null, null);
                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                msg.To.Add(epost);

                msg.Subject = subject;

                msg.Body = "Hei " + tb_reg_fornavn.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);

                if(ActivationUrl != null)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('En link for å aktivere brukerkontoen er sendt til brukereposten');", true);
                tb_reg_epost.Text = string.Empty;
                tb_reg_etternavn.Text = string.Empty;
                tb_reg_fornavn.Text = string.Empty;
                tb_reg_fornavn.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
   
          
        }

    }
}