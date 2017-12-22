namespace Inventario.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Inventario.BLL;
    using Inventario.DAL;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Inventario.Entities;

    public class RemisionController : Controller
    {
        // GET: Remision
        public ActionResult Index()
        {
            var remisiones = new RemisionBLL().GetAllRemision();
            return View(remisiones);
        }

        // GET: Remision/Details/5
        public ActionResult Details(int id)
        {
            var rs = new RemisionBLL().GetRemisionById(id);
            return View(rs);
        }

        // GET: Remision/Create
        public ActionResult Create()
        {
            var almacenes = new RemisionBLL().GetAllAlmacenes();
            var proveedores = new ProveedorBLL().GetAllProveedor();
            var productos = new ProductoBLL().GetAllProductos();
           
            this.ViewData["Almacenes"] = almacenes;
            this.ViewData["Proveedores"] = proveedores;
            this.ViewData["Productos"] = productos;

            Producto prod = new Producto();

            // Build your models (example)
            List<SelectListItem> models = new List<SelectListItem>();
            models.Add(new SelectListItem() { Text = prod.Nombre, Value = prod.Descripcion });
            //models.Add(new SelectListItem() { Text = "Maruti Suzuki", Value = "Maruti Suzuki" });
            // Store your model in the ViewBag
            ViewBag.ProductList = models;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RemisionDetalleModel collection, string submitButton)
        {
            try
            {
                switch (submitButton)
                {
                    case "Guardar":
                        var rs = new RemisionBLL().SetRemision(collection);
                        return RedirectToAction(nameof(Index));
                    case "Guardar y Confirmar":
                        var idConfirm = new RemisionBLL().ConfirmRemision(collection);
                        var re = new RemisionBLL().UpdateConfirmar(idConfirm);
                        return RedirectToAction(nameof(Index));
                    default:
                        return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Remision/Edit/5
        public ActionResult Edit(int id)
        {
            var almacenes = new RemisionBLL().GetAllAlmacenes();
            var proveedores = new ProveedorBLL().GetAllProveedor();
            var productos = new ProductoBLL().GetAllProductos();

            this.ViewData["Almacenes"] = almacenes;
            this.ViewData["Proveedores"] = proveedores;
            this.ViewData["Productos"] = productos;

            var rs = new RemisionBLL().GetRemisionEditById(id);

            return View(rs);
        }

        // POST: Remision/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RemisionDetalleModel collection)
        {
            try
            {
                var rs = new RemisionBLL().UpdateRemision(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // POST: Remision/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirmar(int id)
        {
            try
            {
                var rs = new RemisionBLL().UpdateConfirmar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Remision/Details/5
        public ActionResult Anular(int id)
        {
            var rs = new RemisionBLL().GetRemisionById(id);
            return View(rs);
        }

        // POST: Remision/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anular(int id, RemisionDetalleModel collection)
        {
            try
            {

                var rs = new RemisionBLL().UpdateAnular(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Remision/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Remision/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public JsonResult cargarAlmacenes()
        {
            var rs = new RemisionBLL().GetAllAlmacenes();
            return this.Json(rs);
        }

        public JsonResult cargarProductos()
        {
            var rs = new ProductoBLL().GetAllProductos();
            return this.Json(rs);
        }

        public JsonResult cargarProveedores()
        {
            var rs = new ProveedorBLL().GetAllProveedor();
            return this.Json(rs);
        }
    }
}