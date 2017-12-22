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
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            var productos = new ProductoBLL().GetAllProductos();
            return View(productos);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            var producto = new ProductoBLL().GetProductoBLLById(id);
            return View(producto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto collection)
        {
            try
            {
                var crearProducto = new ProductoBLL().SetProducto(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            var producto = new ProductoBLL().GetProductoBLLById(id);
            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Producto collection)
        {
            try
            {
                var actualizarProveedor = new ProductoBLL().UpdateProducto(id, collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            var producto = new ProductoBLL().GetProductoBLLById(id);
            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Producto collection)
        {
            try
            {
                var deleteProducto = new ProductoBLL().DeleteProducto(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewData["Mensaje"] = "No se puede eliminar el producto por que ya está siendo usado en una remisión";
                var producto = new ProductoBLL().GetProductoBLLById(id);
                return View(producto);
            }
        }
    }
}