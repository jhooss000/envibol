using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs.Asistencia
{
    public class SAsistenciaDto
    {
        public int IdbioAsistencia { get; set; }
        public int Uid { get; set; }
        public long UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public int Punch { get; set; }
        public string IpBiometrico { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime Fecha { get; set; }
    }

}
