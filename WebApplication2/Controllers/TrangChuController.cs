﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;


namespace WebApplication2.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public CT25Team17Entities db = new CT25Team17Entities();
        public ActionResult Index()
        {
            var sp = db.SanPhams.ToList().Take(4);
            ViewBag.hinh = ".png";

           
            return View(sp);
        }
        
    }
}