using Farmalivio_Farmacia.Filter;
using Farmalivio_Farmacia.helpers;
using Farmalivio_Farmacia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Farmalivio_Farmacia.Controllers
{
    public class PerfilController : Controller
    {
        private Farmacia_FarmalivioEntities db = new Farmacia_FarmalivioEntities();
        // GET: Perfil
        [HttpGet]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Index()
        {
            var UserLogin = (usuario)Session["User"];
            int idUser = 0;
            idUser = Convert.ToInt32(UserLogin.id_usuario);
            if (idUser!=0)
            {
                usuario modelo = new usuario();
                modelo = db.usuario.Where(x => x.id_usuario == idUser).SingleOrDefault();
                return View(modelo);
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        //, HttpPostedFileBase img
        public ActionResult Index(usuario usuario)
        {
            usuario devolver = usuario;
            try
            {
                var user = db.usuario.Where(x => x.id_usuario == usuario.id_usuario).SingleOrDefault();
                //user.usuario_cedula = usuario.usuario_cedula;
                user.nombre_user = usuario.nombre_user;
                user.apellido_user = usuario.apellido_user;
                user.email = usuario.email;
                user.usuario_cedula = usuario.usuario_cedula;

                usuario.contrasena = user.contrasena;
                usuario.id_rol = user.id_rol;
                usuario.estado = user.estado;

                //bool rpta = usuario.guardaImg(user, img);
                //string archivo = DateTime.Now.ToString("yyyymmddHHmmss") + Path.GetExtension(img.FileName);
                //string path = Server.MapPath("~/Uploads/");
                //if (Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //img.SaveAs(path + archivo);
                //user.imagen = archivo;
                                
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
            }
            catch (Exception)
            {
                ModelState.AddModelError("newcontrasena", "Hubo un error al guardar");
                return View(devolver);
            }

        }

        [HttpGet]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Password()
        {

            return View();
        }

        [HttpPost]
        [AuthorizeUser(rol: "Vendedor", catalogo: "Categorias")]
        public ActionResult Password(NewPassword pass)
        {
            NewPassword claspass = new NewPassword();

            if (pass.newcontrasena.Length > 15)
            {
                ModelState.AddModelError("newcontrasena", "Maximo de caracteres 15");
            }

            if (pass.newcontrasena.Length < 6)
            {
                ModelState.AddModelError("newcontrasena", "Minimo de caracteres 6");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (pass.newcontrasena== pass.newcontrasena2)
                    {
                        var UserLogin = (usuario)Session["User"];
                        int idUser = Convert.ToInt32(UserLogin.id_usuario);
                        pass.contrasena = HelperHash.MD5(pass.contrasena);
                        usuario user = db.usuario.Where(x => x.id_usuario == idUser).SingleOrDefault();
                        if (user.contrasena == pass.contrasena)
                        {
                            pass.newcontrasena = HelperHash.MD5(pass.newcontrasena);
                            user.contrasena = pass.newcontrasena;

                            bool rpta = claspass.guardarpass(user);
                            if (rpta)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                return View(pass);
                            }
                            
                        }
                        else
                        {
                            ModelState.AddModelError("contrasena", "La contraseña es incorrecta");
                        }

                        
                    }
                    else
                    {
                        //ModelState.AddModelError("newcontrasena", "Lcontraseña es incorrecta");
                        ModelState.AddModelError("newcontrasena2", "La nueva contraseña no coincide");
                    }                   
                }
                catch (Exception)
                {

                    throw;
                }                
            }

            return View(pass);
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