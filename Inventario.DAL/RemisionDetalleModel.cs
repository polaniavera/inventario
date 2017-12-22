

namespace Inventario.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RemisionDetalleModel
    {

        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int IdProveedor { get; set; }
        public string nombreProveedor { get; set; }
        public int IdAlmacen { get; set; }
        public string nombreAlmacen { get; set; }
        public int Estado { get; set; }
        public string EstadoStr { get; set; }

        public int IdDetalleRemision { get; set; }
        public int IdProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }

        public List<int> Cantidades { get; set; }
        public List<int> IdProductos { get; set; }
        public List<string> nombreProductos { get; set; }

    }
}
