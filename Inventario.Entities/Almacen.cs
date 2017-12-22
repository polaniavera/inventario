using System;
using System.Collections.Generic;

namespace Inventario.Entities
{
    public partial class Almacen
    {
        public Almacen()
        {
            InventarioFisico = new HashSet<InventarioFisico>();
            RemisionEntrada = new HashSet<RemisionEntrada>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public ICollection<InventarioFisico> InventarioFisico { get; set; }
        public ICollection<RemisionEntrada> RemisionEntrada { get; set; }
    }
}
