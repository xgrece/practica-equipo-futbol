using practica_equipo_futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica_equipo_futbol
{
    //---------------------------------------------------------------------------------------------------
    //clase jugador
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }

        //constructor
        public Jugador(string nombre, int numero)
        {
            this.Nombre = nombre;
            this.Numero = numero;
        }

        // Devuelve el nombre del jugador
        public override string ToString()
        {
            return this.Nombre;
        }
    }


    //---------------------------------------------------------------------------------------------------
    //clase equipo 
    public class Equipo
    {
        public string nombre {  get; set; }
        public string entrenador { get; set; }
        public List<Jugador> Jugadores { get; set; }

        //constructor
        public Equipo(string nombre, string entrenador)
        {
            this.nombre = nombre;
            this.entrenador = entrenador;
            Jugadores = new List<Jugador>();
        }

        //---------------------------------------------------------------------------------------------------
        //metodos (agregar,buscar,borrar)
        public void agregarJugador(Jugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public bool BuscarJugador(string nombre)
        {
            foreach (var jugador in Jugadores)
            {
                if (jugador.Nombre == nombre) {  return true; }
            }
            return false;
        }

        public void borrarJugador(Jugador jugador)
        {

            Jugadores.Remove(jugador);
            Console.WriteLine($"se elimino el jugador {jugador} correctamente");
        }
    }

    //---------------------------------------------------------------------------------------------------
    //clase equipo local
    public class EquipoLocal : Equipo, IPartido
    {
        //constructor
        public EquipoLocal(string nombre, string entrenador) : base(nombre, entrenador)
        {
            // Llama al constructor de la clase base Equipo con los argumentos proporcionados
        }
        public string simularPartido()
        {
            return "Simulación del partido como equipo local";
        }

        // Devuelve el nombre del jugador
        public override string ToString()
        {
            return this.nombre;
        }
    }


    //---------------------------------------------------------------------------------------------------
    //clase equipo visitante
    public class EquipoVisitante : Equipo, IPartido
    {
        //constructor
        public EquipoVisitante(string nombre, string entrenador) : base(nombre, entrenador)
        {
            // Llama al constructor de la clase base Equipo con los argumentos proporcionados
        }

        public string simularPartido()
        {
            // Lógica para simular el partido como equipo visitante
            return "Simulación del partido como equipo visitante";
        }

        // Devuelve el nombre del jugador
        public override string ToString()
        {
            return this.nombre; 
        }
    }
}
