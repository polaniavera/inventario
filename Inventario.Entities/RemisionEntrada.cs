using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class RemisionEntrada
    {
        public RemisionEntrada()
        {
            RemisionEntradaDetalle = new HashSet<RemisionEntradaDetalle>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaDocumento { get; set; }
        public int IdProveedor { get; set; }
        public int IdAlmacen { get; set; }
        public int Estado { get; set; }

        public Almacen IdAlmacenNavigation { get; set; }
        public Proveedor IdProveedorNavigation { get; set; }
        public ICollection<RemisionEntradaDetalle> RemisionEntradaDetalle { get; set; }
    }
}
