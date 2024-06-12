using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol.Models
{
    public class EquipoVisitante : Equipo, IPartido
    {
        // Constructor
        public EquipoVisitante(string nombre, string entrenador) : base(nombre, entrenador) { }

        public string SimularPartido()
        {
            return "Simulación del partido como equipo visitante";
        }

        // Devuelve el nombre del jugador
        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
