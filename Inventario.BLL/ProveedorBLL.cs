namespace Inventario.BLL
{
    using Inventario.DAL;
    using Inventario.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Clase proveedor
    /// </summary>
    public class ProveedorBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Proveedor> GetAllProveedor()
        {
            return new ProveedorDAL().GetAllProveedor();
        }

        public int SetProveedor(Proveedor collection)
        {
            return new ProveedorDAL().SetProveedor(collection); 
        }

        public int UpdateProveedor(int id, Proveedor collection)
        {
            return new ProveedorDAL().UpdateProveedor(id, collection);
        }

        public Proveedor GetProveedorById(int id)
        {
            return new ProveedorDAL().GetProveedorById(id);
        }

        public int DeleteProveedor(int id, Proveedor collection)
        {
            return new ProveedorDAL().DeleteProveedor(id, collection);
        }
    }
}
