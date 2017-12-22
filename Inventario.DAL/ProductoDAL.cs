
namespace Inventario.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Inventario.Entities;

    public class ProductoDAL : BaseDAL
    {
        public List<Producto> GetAllProductos()
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var listProducto = (from producto in context.Producto
                                     select producto).ToList();
                return listProducto;
            }
        }

        public object SetProducto(Producto collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                try
                {
                    var editUsers = context.Producto.Add(collection);
                    context.SaveChanges();
                    return 0;
                }
                catch (Exception exp)
                {
                    string msg;
                    msg = exp.Message;
                    return 1;
                }
            }
        }

        public object DeleteProducto(int id, Producto collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var itemToRemove = context.Producto.SingleOrDefault(x => x.Id == id);
                if (itemToRemove != null)
                {
                    context.Producto.Remove(itemToRemove);
                    context.SaveChanges();
                }
                return 0;
            }
        }

        public object UpdateProducto(int id, Producto collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var updateProducto = (from producto in context.Producto
                                       where producto.Id == id
                                       select producto).FirstOrDefault();

                updateProducto.Codigo = collection.Codigo;
                updateProducto.Nombre = collection.Nombre;
                updateProducto.Descripcion = collection.Descripcion;
                updateProducto.PrecioVenta = collection.PrecioVenta;               
                updateProducto.StockMinimo = collection.StockMinimo;
                updateProducto.StockMaximo= collection.StockMaximo;
                context.SaveChanges();
                return 0;
            }
        }

        public Producto GetProductoBLLById(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var productoRs = (from producto in context.Producto
                                   where producto.Id == id
                                   select producto).FirstOrDefault();
                return productoRs;
            }
        }
    }
}
