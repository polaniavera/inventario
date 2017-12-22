namespace Inventario.BLL
{
    using Inventario.DAL;
    using Inventario.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Clase de remisiones
    /// </summary>
    public class RemisionBLL
    {
        public List<RemisionDetalleModel> GetAllRemision()
        {
            return new RemisionDAL().GetAllProveedor();
        }

        public List<Almacen> GetAllAlmacenes()
        {
            return new RemisionDAL().GetAllAlmacenes();
        }

        public int SetRemision(RemisionDetalleModel collection)
        {
            RemisionEntrada rmInput = new RemisionEntrada();
            rmInput.Codigo= collection.Codigo;
            rmInput.FechaDocumento = collection.FechaDocumento;
            rmInput.IdProveedor = collection.IdProveedor;
            rmInput.IdAlmacen = collection.IdAlmacen;
            rmInput.Estado = 1;

            var productos = new ProductoBLL().GetAllProductos();
            List<RemisionEntradaDetalle> rmDetail = new List<RemisionEntradaDetalle>();
            
            for (int i = 0; i< productos.Count; i++)
            {
                //rmDetail[i].IdProducto = productos[i].Id;
                //rmDetail[i].Cantidad = collection.Cantidades[i];
                //rmDetail[i].IdRemisionEntrada = collection.Id;

                rmDetail.Add(new RemisionEntradaDetalle()
                {
                    IdProducto = productos[i].Id,
                    Cantidad = collection.Cantidades[i],
                    IdRemisionEntrada = collection.Id
                });
            }

            //rmDetail.IdProducto = collection.IdProducto;
            //rmDetail.IdRemisionEntrada = collection.Id;
            //rmDetail.Cantidad = collection.Cantidad;
            return new RemisionDAL().SetRemision(rmInput, rmDetail);
        }

        
        public int ConfirmRemision(RemisionDetalleModel collection)
        {
            RemisionEntrada rmInput = new RemisionEntrada();
            rmInput.Codigo = collection.Codigo;
            rmInput.FechaDocumento = collection.FechaDocumento;
            rmInput.IdProveedor = collection.IdProveedor;
            rmInput.IdAlmacen = collection.IdAlmacen;
            rmInput.Estado = 2;

            List<RemisionEntradaDetalle> rmDetail = new List<RemisionEntradaDetalle>();
            var productos = new ProductoBLL().GetAllProductos();
            for (int i = 0; i < productos.Count; i++)
            {
                rmDetail.Add(new RemisionEntradaDetalle()
                {
                    IdProducto = productos[i].Id,
                    Cantidad = collection.Cantidades[i],
                    IdRemisionEntrada = collection.Id
                });
            }

            //rmDetail.IdProducto = collection.IdProducto;
            //rmDetail.IdRemisionEntrada = collection.Id;
            //rmDetail.Cantidad = collection.Cantidad;
            return new RemisionDAL().SetRemision(rmInput, rmDetail);
        }

        public int UpdateRemision(int id, RemisionDetalleModel collection)
        {
            RemisionEntrada rmInput = new RemisionEntrada();
            rmInput.Id = id;
            rmInput.Codigo = collection.Codigo;
            rmInput.FechaDocumento = collection.FechaDocumento;
            rmInput.IdProveedor = collection.IdProveedor;
            rmInput.IdAlmacen = collection.IdAlmacen;
            rmInput.Estado = 1;

            var productos = new ProductoBLL().GetAllProductos();
            List<RemisionEntradaDetalle> rmDetail = new List<RemisionEntradaDetalle>();

            for (int i = 0; i < productos.Count; i++)
            {
                rmDetail.Add(new RemisionEntradaDetalle()
                {
                    IdProducto = productos[i].Id,
                    Cantidad = collection.Cantidades[i],
                    IdRemisionEntrada = collection.Id
                });
            }
            //RemisionEntradaDetalle rmDetail = new RemisionEntradaDetalle();
            //rmDetail.IdRemisionEntrada = id;
            //rmDetail.IdProducto = collection.IdProducto;
            //rmDetail.IdRemisionEntrada = collection.Id;
            //rmDetail.Cantidad = collection.Cantidad;

            return new RemisionDAL().UpdateRemision(id, rmInput, rmDetail);
        }

        public RemisionDetalleModel GetRemisionById(int id)
        {
            return new RemisionDAL().GetRemisionById(id);
        }

        public RemisionDetalleModel GetRemisionEditById(int id)
        {
            return new RemisionDAL().GetRemisionEditById(id);
        }

        public object UpdateConfirmar(int id)
        {
            return new RemisionDAL().UpdateConfirmar(id);
        }


        public object UpdateAnular(int id)
        {
            return new RemisionDAL().UpdateAnular(id);
        }
    }
}
