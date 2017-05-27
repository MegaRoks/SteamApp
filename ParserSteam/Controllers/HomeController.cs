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
            XDocument xdoc;
            Profile result;
            if (long.TryParse(steamIDS, out d))
            {
                xdoc = XDocument.Load("http://steamcommunity.com/profiles/" + steamIDS + "/?xml=1");
            }
            else
            {
                xdoc = XDocument.Load("http://steamcommunity.com/id/" + steamIDS + "/?xml=1");
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
            catch(Exception ex)
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