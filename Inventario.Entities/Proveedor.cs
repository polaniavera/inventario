using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            RemisionEntrada = new HashSet<RemisionEntrada>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public ICollection<RemisionEntrada> RemisionEntrada { get; set; }
    }
}
