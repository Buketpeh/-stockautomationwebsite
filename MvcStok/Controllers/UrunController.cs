using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLERs.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILERs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILERs.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
            db.TBLURUNLERs.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index"); //kaydetme işlemi gerçekleştikten sonra index sayfasına gidicek
           
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLERs.Find(id);
            db.TBLURUNLERs.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLERs.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILERs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urn = db.TBLURUNLERs.Find(p.URUNID);
            urn.URUNAD = p.URUNAD;
            urn.MARKA = p.MARKA;
            //urn.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILERs.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI= ktg.KATEGORIID;
            urn.FIYAT = p.FIYAT;
            urn.STOK = p.STOK;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}