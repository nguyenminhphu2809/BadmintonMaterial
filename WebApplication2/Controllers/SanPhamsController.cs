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

namespace WebApplication2.Controllers
{
    public class SanPhamsController : Controller
    {
        private CT25Team17Entities db = new CT25Team17Entities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.NhomSanPham);
            return View(sanPhams.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        public ActionResult Picture(string MaSP)
        {
            var path = Server.MapPath(PICTURE_PATH);
            return File(path + MaSP, "images");
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model,HttpPostedFileBase picture)
        {
            CheckThongTin(model);
            if (ModelState.IsValid)
            {
                if(picture != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        db.SanPhams.Add(model);
                        db.SaveChanges();

                        var path = Server.MapPath(PICTURE_PATH);
                        picture.SaveAs(path + model.MaSP);

                        scope.Complete();
                        return RedirectToAction("Index");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("","Chưa chọn ảnh");
                }
                
            }

            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", model.MaNhom);
            return View(model);
        }

        private const string PICTURE_PATH = "~/images/";

        private void CheckThongTin(SanPham sanPham)
        {
            //Kiem tra MaSP
            var regexItem = new Regex("^[a-z A-Z 0-9 ]*$");

            foreach(var item in db.SanPhams)
            {
                if(sanPham.MaSP == item.MaSP)
                {
                    ModelState.AddModelError("MaSP", "Mã sản phẩm đã tồn tại.");
                }
            }
            if (sanPham.MaSP == null)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm không được để trống.");
            }
            else
            {
                if (sanPham.MaSP.Length < 5 || sanPham.MaSP.Length >10)
                {
                    ModelState.AddModelError("MaSP", "Mã sản phẩm phải có từ 5 đến 10 ký tự.");
                }
                else
                {
                    if(sanPham.MaSP.IndexOf(" ") >= 0)
                    {
                        ModelState.AddModelError("MaSP", "Mã sản phẩm không chứa khoản trắng.");
                    }
                    else
                    {
                        if (regexItem.IsMatch(sanPham.MaSP) == false)
                        {
                            ModelState.AddModelError("MaSP", "Mã sản phẩm chỉ chứa ký tự alphabet(a-z,A-Z,0-9).");
                        }
                    }
                }
            }
            //Kiem tra TenSP
            
            if (sanPham.TenSP == null)
            {
                ModelState.AddModelError("TenSP", "Tên sản phẩm không được để trống.");
            }
            else
            {
                //if (sanPham.TenSP.First().Equal)
                //{
                //    ModelState.AddModelError("TenSP", "Tên sản phẩm phải có ký tự đầu tiên là chữ.");
                //}
            }
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", sanPham.MaNhom);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,ThuongHieu,MaNhom,MoTa,GiaSP,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.NhomSanPhams, "MaNhom", "TenNhom", sanPham.MaNhom);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}