using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum EstadoIncidencia
    {
        Abierto = 1,
        Asignado = 2,
        EnAnalisis = 3,
        Resuelto = 4,
        Reabierto = 5,
        Cerrado = 6
    }
}