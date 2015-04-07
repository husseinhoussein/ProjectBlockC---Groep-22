﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Web.Security;
using TheGreenery.Models;
using TheGreenery.DBcontrollers;

namespace TheGreenery.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginResult(int? klantnr, String mail, String wachtwoord)
        {
            LoginDBController gebruiker = new LoginDBController();
            Session["LoggedIn"] = null;
            Klant gUser = gebruiker.LogInSelect(klantnr, mail, wachtwoord);
            try
            {
                if (gUser != null)
                {
                    if (gUser.mail == @mail && gUser.wachtwoord == @wachtwoord)
                    {
                        Session["LoggedIn"] = gUser.Code_Klant;
                        String url = "/Home/Mijngegevens";
                        return Redirect(url);
                    }
                }
            }
            catch (FormatException)
            {
                ViewData["Ero"] = "you dun goofed";
                return View("LogIn");
            }
            return View();
        }



        public ActionResult Registreer()
        {
            return View();
        }

        public ActionResult geregistreerd(String voorletters, String tussenvoegsel, String achternaam, String adres, String postcode, String woonplaats, String telefoonnr, String mail, String wachtwoord, String wachtwoord_herhalen)
        {
            Klant klant = new Klant();
            klant.setVoorletters(voorletters);
            klant.setTussenvoegsel(tussenvoegsel);
            klant.setAdres(adres);
            klant.setAchternaam(achternaam);
            klant.setPostocde(postcode);
            klant.setWoonplaats(woonplaats);
            klant.setTelefoonnr(telefoonnr);
            klant.setMail(mail);
            klant.setWachtwoord(wachtwoord);
            klant.setWachtwoordHerhalen(wachtwoord_herhalen);

            if (mail != null && !mail.Equals(""))
            {
                RegistrerenDBController registrerenController = new RegistrerenDBController();
                registrerenController.InsertRegistratie(klant);

            }
            return View();

        }

        public ActionResult GegevensGewijzigd(String voorletters, String tussenvoegsel, String achternaam, String adres, String postcode, String woonplaats, String telefoonnr, String mail)
        {
            Klant klant = new Klant();

            klant.setVoorletters(voorletters);
            klant.setTussenvoegsel(tussenvoegsel);
            klant.setAdres(adres);
            klant.setAchternaam(achternaam);
            klant.setPostocde(postcode);
            klant.setWoonplaats(woonplaats);
            klant.setTelefoonnr(telefoonnr);
            klant.setMail(mail);

            //if (klantnr == 1 && !klantnr.Equals(@klantnr))
            //{
            MijnGegevensDBController MijnGegevens = new MijnGegevensDBController();
            MijnGegevens.GegevensAanpassen(klant);

            //}
            return View();

        }

        public ActionResult MijnGegevens(int? klantnr, String mail, String wachtwoord)
        {
            MijnGegevensDBController sc = new MijnGegevensDBController();
            List<Klant> klant = sc.getKlantbyID(klantnr, mail, wachtwoord);
            return View(klant);

        }

        public ActionResult MijnBestellingen(int? bestellingnr)
        {
            MijnBestellingenDBController sc = new MijnBestellingenDBController();
            List<Bestelling> bestelling = sc.getAllBestellingenByDate(bestellingnr);
            return View(bestelling);
        }


    }
}



