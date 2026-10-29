using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Incidencia
    {
        public int IdIncidencia { get; set; }
        public string Problematica { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public DateTime? FechaCierre { get; set; }
        public EstadoIncidencia Estado { get; set; }
        public string ComentarioRespuesta { get; set; }
        public string ComentarioCierre { get; set; }
        public Cliente Cliente { get; set; }
        public TipoIncidencia Tipo { get; set; }
        public Prioridad Prioridad { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public List<HistorialIncidencia> Historial { get; set; }
    }
}
