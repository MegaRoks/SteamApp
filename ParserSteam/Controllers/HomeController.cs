using ParserSteam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            long d;
            if (long.TryParse(steamIDS, out d))
            {
                XDocument xdoc = XDocument.Load("http://steamcommunity.com/profiles/" + steamIDS + "/?xml=1");
                var q = FromXml(xdoc);
                return View(q);
            }
            else
            {
                XDocument xdoc = XDocument.Load("http://steamcommunity.com/id/" + steamIDS + "/?xml=1");
                var q = FromXml(xdoc);
                return View(q);
            }             
        }

        private Profile FromXml(XDocument xd)
        {
            XmlSerializer s = new XmlSerializer(typeof(Profile));
            return (Profile)s.Deserialize(xd.CreateReader());
        }
    }
}