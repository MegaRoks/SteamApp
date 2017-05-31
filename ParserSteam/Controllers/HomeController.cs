using ParserSteam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Net;

namespace ParserSteam.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                IPHostEntry i = Dns.GetHostEntry("www.google.com");
                ViewBag.result = 1;
            }
            catch
            {
                ViewBag.result = 2;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Info(string steamIDS)
        {
            long d;
            XDocument xdoc;
            Profile result;
            if (long.TryParse(steamIDS, out d))
            {
                try
                {
                    xdoc = XDocument.Load("http://steamcommunity.com/profiles/" + steamIDS + "/?xml=1");
                }
                catch
                {
                    return View("~/Views/Home/Index.cshtml");
                    ViewBag.result = 2;
                }
            }
            else
            {
                try
                {
                    xdoc = XDocument.Load("http://steamcommunity.com/id/" + steamIDS + "/?xml=1");
                }
                catch
                {
                    return View("~/Views/Home/Index.cshtml");
                    ViewBag.result = 2;
                }
            }
            try
            {
                result = FromXml(xdoc);
                if (result.Groups != null)
                {
                    if (result.Groups.Group.Count > 3)
                    {
                        FillGroups(result);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                ViewBag.error = ex.Message;
            }

            return View(result);
        }

        private Profile FromXml(XDocument xd)
        {
            XmlSerializer s = new XmlSerializer(typeof(Profile));
            return (Profile)s.Deserialize(xd.CreateReader());
        }

        private void FillGroups(Profile profile)
        {
            XmlSerializer s = new XmlSerializer(typeof(MemberList));
            if (profile.Groups.Group.Count > 3)
            {
                for (int i = 3; i < profile.Groups.Group.Count; i++)
                {
                    string groupID64 = profile.Groups.Group[i].GroupID64;
                    XDocument xdoc = XDocument.Load("http://steamcommunity.com/gid/" + groupID64 + "/memberslistxml/?xml=1");
                    profile.Groups.Group[i] = ((MemberList)s.Deserialize(xdoc.CreateReader())).GroupDetails;
                }
            }
        }
    }
}