using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventario.BLL;
using Inventario.Entities;

namespace Inventario.MVC.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            var proveedores = new ProveedorBLL().GetAllProveedor();
            return View(proveedores);
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {
            var proveedor = new ProveedorBLL().GetProveedorById(id);
            return View(proveedor);
        }

        // GET: Proveedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proveedor collection)
        {
            try
            {
                var crearProveedor = new ProveedorBLL().SetProveedor(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            var proveedor = new ProveedorBLL().GetProveedorById(id);
            return View(proveedor);
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Proveedor collection)
        {
            try
            {
                // TODO: Add update logic here

                var actualizarProveedor = new ProveedorBLL().UpdateProveedor(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            var proveedor = new ProveedorBLL().GetProveedorById(id);
            return View(proveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Proveedor collection)
        {
            try
            {                
                var deleteProveedor = new ProveedorBLL().DeleteProveedor(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Mensaje"] = "No se puede eliminar el proveedor por que ya está siendo usado en una remisión";
                var proveedor = new ProveedorBLL().GetProveedorById(id);
                return View(proveedor);
            }
        }
    }
}