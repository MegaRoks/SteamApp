using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ParserSteam.Controllers
{
    public class HomeController : Controller
    {       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Info(string steamIDS)
        {
            XDocument xdoc = XDocument.Load("http://steamcommunity.com/profiles/" + steamIDS + "/?xml=1");
            ViewBag.steamID64 = xdoc.Element("profile").Element("steamID64").Value;
            ViewBag.steamID = xdoc.Element("profile").Element("steamID").Value;
            ViewBag.onlineState = xdoc.Element("profile").Element("onlineState").Value;
            ViewBag.stateMessage = xdoc.Element("profile").Element("stateMessage").Value;
            ViewBag.privacyState = xdoc.Element("profile").Element("privacyState").Value;
            ViewBag.memberSince = xdoc.Element("profile").Element("memberSince").Value;
            ViewBag.location = xdoc.Element("profile").Element("location").Value;
            ViewBag.avatarFull = xdoc.Element("profile").Element("avatarFull").Value;
            ViewBag.vacBanned = xdoc.Element("profile").Element("vacBanned").Value;
            ViewBag.tradeBanState = xdoc.Element("profile").Element("tradeBanState").Value;
            ViewBag.isLimitedAccount = xdoc.Element("profile").Element("isLimitedAccount").Value;
            ViewBag.summary = xdoc.Element("profile").Element("summary").Value;
            ViewBag.gameLink = xdoc.Element("profile").Element("mostPlayedGames").Element("mostPlayedGame").Element("gameLink").Value;
            ViewBag.gameLogo = xdoc.Element("profile").Element("mostPlayedGames").Element("mostPlayedGame").Element("gameLogo").Value;
            ViewBag.hoursPlayed = xdoc.Element("profile").Element("mostPlayedGames").Element("mostPlayedGame").Element("hoursPlayed").Value;
            ViewBag.hoursOnRecord = xdoc.Element("profile").Element("mostPlayedGames").Element("mostPlayedGame").Element("hoursOnRecord").Value;
            return View();
        }
    }
}