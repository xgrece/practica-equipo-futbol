using practica_equipo_futbol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace practica_equipo_futbol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //cargar equipo
            Equipo equipo1 = new Equipo("Juan", "Mariano");

            //cargar jugadores
            Jugador jugador1 = new Jugador("Valentino", 9);
            Jugador jugador2 = new Jugador("Thiago", 5);

            //agregar los jugadores
            equipo1.agregarJugador(jugador1);
            equipo1.agregarJugador(jugador2);

            //---------------------------------------------------------------------------------------------------
            //mostrar informacion
            Console.WriteLine($"equipo: {equipo1.nombre}");
            Console.WriteLine($"entrenador: {equipo1.entrenador}");
            Console.WriteLine("jugadores:");
            foreach (var jugador in equipo1.Jugadores)
            {
                Console.WriteLine($"jugador: {jugador.Nombre}, numero: {jugador.Numero}");
            }

            //---------------------------------------------------------------------------------------------------
            //buscar los jugadores
            bool buscarJugador1 = equipo1.BuscarJugador("Valentino");
            bool buscarJugador2 = equipo1.BuscarJugador("Juan");

            //mostrar informacion sobre la busqueda
            Console.WriteLine($"jugador Valentino: {buscarJugador1}");
            Console.WriteLine($"jugador Juan: {buscarJugador2}");

            //---------------------------------------------------------------------------------------------------
            //eliminar jugador
            Jugador jugadorABorrar = equipo1.Jugadores.Find(j => j.Nombre == "Valentino");
            if (jugadorABorrar != null)
            {
                // Si se encontró, eliminarlo del equipo
                equipo1.borrarJugador(jugadorABorrar);
            }

            //---------------------------------------------------------------------------------------------------
            //crear equipo visitante y local
            Equipo equipoLocal = new Equipo("Los pibes", "Luis");
            Equipo equipoVisitante = new Equipo("Villeros", "Jose");

            // Crear una instancia de la clase Partido con los equipos local y visitante
            Partido partido = new Partido(equipoLocal, equipoVisitante);

            // Simular el partido y obtener el resultado
            string resultadoPartido = partido.simularPartido();

            // Mostrar el resultado del partido
            Console.WriteLine($"Resultado del partido: {resultadoPartido}");
            //---------------------------------------------------------------------------------------------------
        }

    }
}
