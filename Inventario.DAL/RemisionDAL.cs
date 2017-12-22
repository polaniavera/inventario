namespace Inventario.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Inventario.Entities;

    public class RemisionDAL : BaseDAL
    {
        public List<RemisionDetalleModel> GetAllProveedor()
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                string estadoStr = "";

                //var rs = (from remisionEntrada in context.RemisionEntrada
                //          join remisionDetalle in context.RemisionEntradaDetalle on remisionEntrada.Id equals remisionDetalle.IdRemisionEntrada
                //          join proveedor in context.Proveedor on remisionEntrada.IdProveedor equals proveedor.Id
                //          join almacen in context.Almacen on remisionEntrada.IdAlmacen equals almacen.Id
                //          join producto in context.Producto on remisionDetalle.IdProducto equals producto.Id
                //          select new RemisionDetalleModel
                //          {
                //              Id = remisionEntrada.Id,
                //              Codigo = remisionEntrada.Codigo,
                //              FechaDocumento = remisionEntrada.FechaDocumento,
                //              IdProveedor = remisionEntrada.IdProveedor,
                //              nombreProveedor = proveedor.Nombre,
                //              IdAlmacen = remisionEntrada.IdAlmacen,
                //              nombreAlmacen = almacen.Nombre,
                //              Estado = remisionEntrada.Estado,
                //              IdDetalleRemision = remisionDetalle.Id,
                //              IdProducto = remisionDetalle.IdProducto,
                //              nombreProducto = producto.Nombre,
                //              PrecioVenta = producto.PrecioVenta,
                //              Cantidad = remisionDetalle.Cantidad
                //          }).ToList<RemisionDetalleModel>();

                var rs = (from remisionEntrada in context.RemisionEntrada
                          join proveedor in context.Proveedor on remisionEntrada.IdProveedor equals proveedor.Id
                          join almacen in context.Almacen on remisionEntrada.IdAlmacen equals almacen.Id
                          select new RemisionDetalleModel
                          {
                              Id = remisionEntrada.Id,
                              Codigo = remisionEntrada.Codigo,
                              FechaDocumento = remisionEntrada.FechaDocumento,
                              IdProveedor = remisionEntrada.IdProveedor,
                              nombreProveedor = proveedor.Nombre,
                              IdAlmacen = remisionEntrada.IdAlmacen,
                              nombreAlmacen = almacen.Nombre,
                              Estado = remisionEntrada.Estado
                          }).ToList<RemisionDetalleModel>();

                foreach (var rsult in rs)
                {
                    if (rsult.Estado == 1)
                    {
                        estadoStr = "Registrada";
                    }
                    else if (rsult.Estado == 2)
                    {
                        estadoStr = "Confirmada";
                    }
                    else
                    {
                        estadoStr = "Anulada";
                    }

                    //rs.SingleOrDefault(x => x.Id == rsult.Id).EstadoStr = estadoStr;
                    var items = rs.Where(x => x.Id == rsult.Id);
                    foreach (var item in items)
                    {
                        item.EstadoStr = estadoStr;
                    }
                }
                return rs;
            }
        }

        public object UpdateAnular(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var updaterminput = (from remisionEntrada in context.RemisionEntrada
                                     where remisionEntrada.Id == id
                                     select remisionEntrada).FirstOrDefault();
                updaterminput.Estado = 3;
                context.SaveChanges();

                return 0;
            }
        }

        public object UpdateConfirmar(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var updaterminput = (from remisionEntrada in context.RemisionEntrada
                                     where remisionEntrada.Id == id
                                     select remisionEntrada).FirstOrDefault();
                var updatermDetail = (from remisionEntradaDetail in context.RemisionEntradaDetalle
                                     where remisionEntradaDetail.IdRemisionEntrada == id
                                     select remisionEntradaDetail).ToList();

                foreach (var detail in updatermDetail)
                {
                    var invetarioFisico = (from invFisico in context.InventarioFisico
                                           where invFisico.IdAlmacen == updaterminput.IdAlmacen
                                           && invFisico.IdProducto == detail.IdProducto
                                           select invFisico).FirstOrDefault();
                    if (invetarioFisico != null)
                    {
                        invetarioFisico.Cantidad = invetarioFisico.Cantidad + detail.Cantidad;
                    }
                    else
                    {
                        InventarioFisico invFisico = new InventarioFisico();
                        invFisico.IdAlmacen = updaterminput.IdAlmacen;
                        invFisico.IdProducto = detail.IdProducto;
                        invFisico.Cantidad = detail.Cantidad;
                        var rs = context.InventarioFisico.Add(invFisico);
                    }
                }

                updaterminput.Estado = 2;
                context.SaveChanges();

                return 0;
            }
        }

        public RemisionDetalleModel GetRemisionById(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var rs = (from remisionEntrada in context.RemisionEntrada
                          join remisionDetalle in context.RemisionEntradaDetalle on remisionEntrada.Id equals remisionDetalle.IdRemisionEntrada
                          join proveedor in context.Proveedor on remisionEntrada.IdProveedor equals proveedor.Id
                          join almacen in context.Almacen on remisionEntrada.IdAlmacen equals almacen.Id
                          join producto in context.Producto on remisionDetalle.IdProducto equals producto.Id
                          where remisionEntrada.Id == id
                          select new RemisionDetalleModel
                          {
                              Id = remisionEntrada.Id,
                              Codigo = remisionEntrada.Codigo,
                              FechaDocumento = remisionEntrada.FechaDocumento,
                              IdProveedor = remisionEntrada.IdProveedor,
                              nombreProveedor = proveedor.Nombre,
                              IdAlmacen = remisionEntrada.IdAlmacen,
                              nombreAlmacen = almacen.Nombre,
                              Estado = remisionEntrada.Estado,
                              IdDetalleRemision = remisionDetalle.Id,
                              IdProducto = remisionDetalle.IdProducto,
                              nombreProducto = producto.Nombre,
                              PrecioVenta = producto.PrecioVenta,
                              Cantidad = remisionDetalle.Cantidad
                          }).FirstOrDefault();

                var items = (from remisionDetalle in context.RemisionEntradaDetalle
                             join producto in context.Producto on remisionDetalle.IdProducto equals producto.Id
                             where remisionDetalle.IdRemisionEntrada == id
                             select new { producto.Nombre, remisionDetalle.Cantidad, remisionDetalle.IdProducto }).ToList();

                if (rs != null)
                {
                    rs.Cantidades = new List<int>();
                    rs.IdProductos = new List<int>();
                    rs.nombreProductos = new List<string>();
                }

                foreach (var item in items)
                {
                    rs.Cantidades.Add(item.Cantidad);
                    rs.IdProductos.Add(item.IdProducto);
                    rs.nombreProductos.Add(item.Nombre);
                }

                return rs;
            }
        }

        public RemisionDetalleModel GetRemisionEditById(int id)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var rs = (from remisionEntrada in context.RemisionEntrada
                          join remisionDetalle in context.RemisionEntradaDetalle on remisionEntrada.Id equals remisionDetalle.IdRemisionEntrada
                          join proveedor in context.Proveedor on remisionEntrada.IdProveedor equals proveedor.Id
                          join almacen in context.Almacen on remisionEntrada.IdAlmacen equals almacen.Id
                          join producto in context.Producto on remisionDetalle.IdProducto equals producto.Id
                          where remisionEntrada.Id == id
                          select new RemisionDetalleModel
                          {
                              Id = remisionEntrada.Id,
                              Codigo = remisionEntrada.Codigo,
                              FechaDocumento = remisionEntrada.FechaDocumento,
                              IdProveedor = remisionEntrada.IdProveedor,
                              nombreProveedor = proveedor.Nombre,
                              IdAlmacen = remisionEntrada.IdAlmacen,
                              nombreAlmacen = almacen.Nombre,
                              Estado = remisionEntrada.Estado,
                              IdDetalleRemision = remisionDetalle.Id,
                              IdProducto = remisionDetalle.IdProducto,
                              nombreProducto = producto.Nombre,
                              PrecioVenta = producto.PrecioVenta,
                              Cantidad = remisionDetalle.Cantidad
                          }).FirstOrDefault();

                var items = (from remisionDetalle in context.RemisionEntradaDetalle
                             join producto in context.Producto on remisionDetalle.IdProducto equals producto.Id
                             where remisionDetalle.IdRemisionEntrada == id
                             select new ProductosRemision
                             {
                                 Nombre = producto.Nombre,
                                 Cantidad = remisionDetalle.Cantidad,
                                 IdProducto = remisionDetalle.IdProducto
                             }).ToList();

                
                var listProductos = (from producto in context.Producto
                                     select producto).ToList();

                List<ProductosRemision> itemsProductos = new List<ProductosRemision>();
                for (int i = 0; i < listProductos.Count; i++)
                {
                    itemsProductos.Add(new ProductosRemision()
                    {
                        Cantidad = 0,
                        IdProducto = listProductos[i].Id,
                        Nombre = listProductos[i].Nombre
                    });
                }

                for (int j=0; j<items.Count; j++)
                {
                    for (int i=itemsProductos.Count-1; i >= 0; i--)
                    {
                        if (items[j].IdProducto == itemsProductos[i].IdProducto)
                        {
                            itemsProductos.RemoveAt(i);
                        }
                    }
                }

                items = items.Union(itemsProductos).ToList();

                if (rs != null)
                {
                    rs.Cantidades = new List<int>();
                    rs.IdProductos = new List<int>();
                    rs.nombreProductos = new List<string>();
                    foreach (var item in items)
                    {
                        rs.Cantidades.Add(item.Cantidad);
                        rs.IdProductos.Add(item.IdProducto);
                        rs.nombreProductos.Add(item.Nombre);
                    }
                }

                return rs;
            }
        }

        public int SetRemision(RemisionEntrada rmInput, List<RemisionEntradaDetalle> rmDetail)
            {
                using (InventarioDBContext context = new InventarioDBContext())
                {
                    try
                    {
                        context.RemisionEntrada.Add(rmInput);
                        context.SaveChanges();
                        var rs = (from a in context.RemisionEntrada
                                  where a.Codigo == rmInput.Codigo
                                  && a.Estado == rmInput.Estado
                                  && a.IdAlmacen == rmInput.IdAlmacen
                                  && a.IdProveedor == rmInput.IdProveedor
                                  && a.FechaDocumento == rmInput.FechaDocumento
                                  select a).FirstOrDefault();

                        for (int i = 0; i < rmDetail.Count; i++)
                        {
                            rmDetail[i].IdRemisionEntrada = rs.Id;
                            context.RemisionEntradaDetalle.Add(rmDetail[i]);
                        }
                        
                        context.SaveChanges();
                        return rs.Id;
                    }
                    catch (Exception exp)
                    {
                        string msg;
                        msg = exp.Message;
                        return 0;
                    }
                }
            }

        public int UpdateRemision(int id, RemisionEntrada rminput, List<RemisionEntradaDetalle> rmdetail)
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var updaterminput = (from remisionEntrada in context.RemisionEntrada
                                       where remisionEntrada.Id == id
                                       select remisionEntrada).FirstOrDefault();
                updaterminput.Codigo = rminput.Codigo;
                updaterminput.FechaDocumento = rminput.FechaDocumento;
                updaterminput.IdProveedor = rminput.IdProveedor;
                updaterminput.IdAlmacen = rminput.IdAlmacen;
                updaterminput.Estado = rminput.Estado;

                var updatermdetail = (from remisionEntradaDetail in context.RemisionEntradaDetalle
                                     where remisionEntradaDetail.IdRemisionEntrada == id
                                     select remisionEntradaDetail).ToList();

                for (int i = 0; i < updatermdetail.Count; i++)
                {
                    updatermdetail[i].Cantidad = rmdetail[i].Cantidad;
                }

                for (int j = updatermdetail.Count; j < rmdetail.Count; j++)
                {
                    context.RemisionEntradaDetalle.Add(rmdetail[j]);
                }

                context.SaveChanges();

                return 0;
            }
        }

        public List<Almacen> GetAllAlmacenes()
        {
            using (InventarioDBContext context = new InventarioDBContext())
            {
                var rs = (from almacen in context.Almacen
                            select almacen).ToList();
                return rs;
            }
        }
    }
}
