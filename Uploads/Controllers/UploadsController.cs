using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uploads.Models;

namespace Uploads.Controllers
{
    public class UploadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uploads
        public ActionResult list()
        {
            return View(db.Uploads.ToList());
        }

        public ActionResult Index(int? id)
        {
            var file = db.Uploads.Find(id);
            return File(file.Content, file.ContentType);
        }

        // GET: Uploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // GET: Uploads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase MyFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Not valid");
                }

                if (MyFile == null && MyFile.ContentLength == 0)
                {
                    throw new Exception("no file uploaded");
                }

                if(!UploadsHelper.validMimeTypes.Contains(MyFile.ContentType.ToLower()))
                {
                    throw new Exception("invalid mime type");
                }

                var extension = Path.GetExtension(MyFile.FileName);
                if (!UploadsHelper.validExtensions.Contains(extension))
                {
                    throw new Exception("invalid extension");
                }


                var upload = new Upload
                {
                    Name = Path.GetFileName(MyFile.FileName),
                    ContentType = MyFile.ContentType
                };

                using (var reader = new System.IO.BinaryReader(MyFile.InputStream))
                {
                    upload.Content = reader.ReadBytes(MyFile.ContentLength);
                }

                db.Uploads.Add(upload);
                db.SaveChanges();


                ImageResizer.ImageBuilder.Current.

                //ImageBuilder.Current.Build(originalFilePath, versionUploadFolder + Path.GetFileNameWithoutExtension(originalFilePath), new ResizeSettings(string.Format("width={0}", (int)item)), false, true);


                return RedirectToAction("List");

            }
            catch (Exception)
            {

                throw;
            }

            return View();
        }

        // GET: Uploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Size,Name,ContentType,Content")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(upload);
        }

        // GET: Uploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upload upload = db.Uploads.Find(id);
            db.Uploads.Remove(upload);
            db.SaveChanges();
            return RedirectToAction("List");
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
