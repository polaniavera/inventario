using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class InventarioFisico
    {
        public int Id { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public Almacen IdAlmacenNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
