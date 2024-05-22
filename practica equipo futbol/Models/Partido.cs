using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol.Models
{
    public class Partido : IPartido
    {
        private Equipo equipoLocal;
        private Equipo equipoVisitante;

        public Partido(Equipo equipoLocal, Equipo equipoVisitante)
        {
            this.equipoLocal = equipoLocal;
            this.equipoVisitante = equipoVisitante;
        }
        public string simularPartido()
        {
            Random rnd = new Random();
            int golesLocal = rnd.Next(0, 5); // Genera un número aleatorio entre 0 y 4 (inclusive).
            int golesVisitante = rnd.Next(0, 5);

            return $"{equipoLocal.nombre} {golesLocal} - {golesVisitante} {equipoVisitante.nombre}";
        }
    }
}
