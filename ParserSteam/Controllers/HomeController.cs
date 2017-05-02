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
        public ActionResult Index()
        {
            XDocument xdoc = XDocument.Load("http://steamcommunity.com/profiles/76561198049827773/?xml=1");
            ViewBag.steamID64 = xdoc.Element("profile").Element("steamID64").Value;
            ViewBag.steamID = xdoc.Element("profile").Element("steamID").Value;
            ViewBag.onlineState = xdoc.Element("profile").Element("onlineState").Value;
            ViewBag.avatarIcon = xdoc.Element("profile").Element("avatarIcon").Value;
            ViewBag.memberSince = xdoc.Element("profile").Element("memberSince").Value;
            ViewBag.location = xdoc.Element("profile").Element("location").Value;
            return View();
        }
    }
}