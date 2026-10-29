using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Dominio
{
    public class HistorialIncidencia
    {
        public int IdHistorial { get; set; }
        public Incidencia Incidencia { get; set; }
        public DateTime Fecha { get; set; }
        public Usuario Usuario { get; set; }
        public EstadoIncidencia? EstadoAnterior { get; set; }
        public EstadoIncidencia EstadoNuevo { get; set; }
        public string Comentario { get; set; }
    }
}
