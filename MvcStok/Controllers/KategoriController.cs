using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity; //tablolara ulaşmak için
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities(); //buradaki tablolara ulaşmak için bir db nesnesi oluştuuryorm
        public ActionResult Index(int sayfa=1) //sayfanın başlangıç değeri 1
        {
            //var degerler = db.TBLKATEGORILERs.ToList(); //değerlerde kategoriler tablosunu listeleyeceğiz
            var degerler = db.TBLKATEGORILERs.ToList().ToPagedList(sayfa, 4);
            return View(degerler); //ve değerleri döndürdük
        }
        [HttpGet]
        public ActionResult YeniKategori() {
            return View();
        } 
        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILERs.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILERs.Find(id);
            db.TBLKATEGORILERs.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORILERs.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var ktg = db.TBLKATEGORILERs.Find(p1.KATEGORIID);
            ktg.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}