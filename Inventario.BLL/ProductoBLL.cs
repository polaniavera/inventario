namespace Inventario.BLL
{
    using Inventario.DAL;
    using Inventario.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProductoBLL
    {
        public List<Producto> GetAllProductos()
        {
            return new ProductoDAL().GetAllProductos();
        }

        public object SetProducto(Producto collection)
        {
            return new ProductoDAL().SetProducto(collection);
        }

        public Producto GetProductoBLLById(int id)
        {
            return new ProductoDAL().GetProductoBLLById(id);
        }

        public object UpdateProducto(int id, Producto collection)
        {
            return new ProductoDAL().UpdateProducto(id, collection);
        }

        public object DeleteProducto(int id, Producto collection)
        {
            return new ProductoDAL().DeleteProducto(id, collection);
        }
    }
}
