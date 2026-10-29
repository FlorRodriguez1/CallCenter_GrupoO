using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Dominio
{
    public class SolicitudReapertura
    {
        public int IdSolicitud { get; set; }
        public Incidencia Incidencia { get; set; }
        public Usuario Solicitante { get; set; }
        public string Motivo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public EstadoSolicitud Estado { get; set; }
        public Usuario Supervisor { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public string ComentarioRespuesta { get; set; }
    }
}
