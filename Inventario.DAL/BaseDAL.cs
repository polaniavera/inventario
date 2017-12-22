namespace Inventario.DAL
{    
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Base para el context de las otras clases
    /// </summary>
    public class BaseDAL
    {
        /// <summary>
        /// Agrega el context del sistema para usarla en las demas clases
        /// </summary>
        /// <returns>El context del sistema</returns>
        public InventarioDBContext GetContext()
        {
            InventarioDBContext context = new InventarioDBContext();
            return context;
        }
    }
}
