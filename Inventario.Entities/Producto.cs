using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            InventarioFisico = new HashSet<InventarioFisico>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

        public ICollection<InventarioFisico> InventarioFisico { get; set; }
    }
}
