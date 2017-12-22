using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class RemisionEntradaDetalle
    {
        public int Id { get; set; }
        public int IdRemisionEntrada { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public RemisionEntrada IdRemisionEntradaNavigation { get; set; }
    }
}
