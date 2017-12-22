namespace Inventario.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Inventario.DAL;
    using Inventario.Entities;
    using System.Linq;

    /// <summary>
    /// Clase proveedor
    /// </summary>
    public class ProveedorDAL : BaseDAL
    {
        /// <summary>
        /// Retorna todos los proveedores almacenados
        /// </summary>
        /// <returns></returns>
        public List<Proveedor> GetAllProveedor()
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var listProveedor = (from proveedor in context.Proveedor
                                     select proveedor).ToList();
                return listProveedor;
            }
        }

        public int UpdateProveedor(int id, Proveedor collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var updateProveedor = (from proveedor in context.Proveedor
                                       where proveedor.Id == id
                                       select proveedor).FirstOrDefault();
                updateProveedor.Codigo = collection.Codigo;
                updateProveedor.Nombre = collection.Nombre;
                updateProveedor.Direccion = collection.Direccion;
                updateProveedor.Telefono = collection.Telefono;
                context.SaveChanges();
                return 0;
            }
        }

        public int DeleteProveedor(int id, Proveedor collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var itemToRemove = context.Proveedor.SingleOrDefault(x => x.Id == id);
                if (itemToRemove != null)
                {
                    context.Proveedor.Remove(itemToRemove);
                    context.SaveChanges();
                }
                return 0;
            }
        }

        public Proveedor GetProveedorById(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var proveedorRs = (from proveedor in context.Proveedor
                                   where proveedor.Id == id
                                   select proveedor).FirstOrDefault();
                return proveedorRs;
            }
        }

        public int SetProveedor(Proveedor collection)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                try
                {
                    var editUsers = context.Proveedor.Add(collection);
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
    }
}
