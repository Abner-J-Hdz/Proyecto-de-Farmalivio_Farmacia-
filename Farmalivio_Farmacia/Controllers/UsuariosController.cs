using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farmalivio_Farmacia.Models;
using System.Data.Entity.Validation;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Filter;

namespace Farmalivio_Farmacia.Controllers
{
    public class UsuariosController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();

        // GET: Usuarios
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Index()
        {
            var usuario = db.usuario.Include(u => u.rol).Where(x=>x.id_usuario!=1);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create()
        {
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1");
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Create([Bind(Include = "id_usuario,nombre_user,apellido_user,email,contrasena,estado,imagen,id_rol, usuario_cedula")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.contrasena = "Contrasena123";


                    usuario.contrasena = HelperHash.MD5(usuario.contrasena);
                    usuario.estado = true;
                    db.usuario.Add(usuario);
                 
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //catch (Exception)
                //{
                //    ModelState.AddModelError("nombre_user", "Erro al guardar");
                //    //throw;
                //}
                catch (DbEntityValidationException ex)
                {
                    List<string> errorMessages = new List<string>();
                    List<string> newMessages = new List<string>();
                    //((System.Data.Entity.Validation.DbEntityValidationException)$dbEx).EntityValidationErrors;

                    foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }
                    //respuesta.Respuesta = false;
                    //respuesta.ListaError = errorMessages;
                    newMessages = errorMessages;
                }

            }
            

            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1", usuario.id_rol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1", usuario.id_rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Edit([Bind(Include = "id_usuario,nombre_user,apellido_user,email,id_rol,usuario_cedula")] usuario usuario)
        {
            usuario devolver = new usuario();
            devolver = usuario;
            try
            {
                    var user = db.usuario.Where(x => x.id_usuario == usuario.id_usuario).SingleOrDefault();
                user.nombre_user = usuario.nombre_user;
                user.apellido_user = usuario.apellido_user;
                user.email = usuario.email;
                user.id_rol = usuario.id_rol;
                user.usuario_cedula = usuario.usuario_cedula;

                //usuario.contrasena = user.contrasena;
                //usuario.imagen = user.imagen;
                
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                //ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1", usuario.id_rol);
                //return View(usuario);
                //throw;
            }
            //ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1", usuario.id_rol);
            ViewBag.id_rol = new SelectList(db.rol, "id_rol", "rol1", usuario.id_rol);
            return View(devolver);

        }
        [AuthorizeUser(rol: "Administrador", catalogo: "Generico")]
        public ActionResult Changestate(int id)
        {
            int ID = id;
            var obtener = new usuario();
            try
            {
                obtener = db.usuario.Where(x => x.id_usuario == ID).SingleOrDefault();
                if (obtener != null)
                {
                    if (obtener.estado == true)
                    {
                        obtener.estado = false;
                        db.Entry(obtener).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        obtener.estado = true;
                        db.Entry(obtener).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    //db.SaveChanges();
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                //((System.Data.Entity.Validation.DbEntityValidationException)$dbEx).EntityValidationErrors
            }

            return RedirectToAction("Index");
        }

        // GET: Usuarios/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    usuario usuario = db.usuario.Find(id);
        //    if (usuario == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(usuario);
        //}

        //// POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    usuario usuario = db.usuario.Find(id);
        //    db.usuario.Remove(usuario);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
