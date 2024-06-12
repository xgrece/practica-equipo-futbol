using practica_equipo_futbol.Models;
using System;
using System.Collections.Generic;

namespace practica_equipo_futbol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*

            // Cargar equipo
            Equipo equipo1 = new Equipo("Juan", "Mariano");

            // Cargar jugadores
            Jugador jugador1 = new Jugador("Valentino", 9);
            Jugador jugador2 = new Jugador("Thiago", 5);

            // Agregar los jugadores
            equipo1.AgregarJugador(jugador1);
            equipo1.AgregarJugador(jugador2);

            // Mostrar información
            Console.WriteLine($"Equipo: {equipo1.Nombre}");
            Console.WriteLine($"Entrenador: {equipo1.Entrenador}");
            Console.WriteLine("Jugadores:");
            foreach (var jugador in equipo1.Jugadores)
            {
                Console.WriteLine($"Jugador: {jugador.Nombre}, Número: {jugador.Numero}");
            }

            // Buscar los jugadores
            bool buscarJugador1 = equipo1.BuscarJugador("Valentino");
            bool buscarJugador2 = equipo1.BuscarJugador("Juan");

            // Mostrar información sobre la búsqueda
            Console.WriteLine($"Jugador Valentino: {buscarJugador1}");
            Console.WriteLine($"Jugador Juan: {buscarJugador2}");

            // Eliminar jugador
            Jugador jugadorABorrar = equipo1.Jugadores.Find(j => j.Nombre == "Valentino");
            if (jugadorABorrar != null)
            {
                // Si se encontró, eliminarlo del equipo
                equipo1.BorrarJugador(jugadorABorrar);
            }

            // Crear equipo visitante y local
            EquipoLocal equipoLocal = new EquipoLocal("Los Pibes", "Luis");
            EquipoVisitante equipoVisitante = new EquipoVisitante("Villeros", "Jose");

            // Crear una instancia de la clase Partido con los equipos local y visitante
            Partido partido = new Partido(equipoLocal, equipoVisitante);

            // Simular el partido y obtener el resultado
            string resultadoPartido = partido.SimularPartido();

            // Mostrar el resultado del partido
            Console.WriteLine($"Resultado del partido: {resultadoPartido}");
            */

            EquipoBD equipoBD = new EquipoBD();

            // 2. Obtener la lista de jugadores de un equipo específico
            Console.WriteLine("Ingrese el ID del equipo para ver sus jugadores:");
            int equipoId = int.Parse(Console.ReadLine());
            var jugadores = equipoBD.GetJugadoresPorEquipoId(equipoId);

            Console.WriteLine($"\nJugadores del equipo con ID {equipoId}:");
            foreach (var jugador in jugadores)
            {
                Console.WriteLine($"ID: {jugador.Id}, Nombre: {jugador.Nombre}, Número: {jugador.Numero}");
            }

            // 3. Obtener la lista de todos los equipos y sus entrenadores
            var equiposConEntrenadores = equipoBD.GetEquiposConEntrenadores();
            Console.WriteLine("\nLista de todos los equipos y sus entrenadores:");
            foreach (var equipo in equiposConEntrenadores)
            {
                Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}");
            }

            // 4. Mostrar número de jugadores por equipo
            Console.WriteLine("\nNúmero de jugadores por equipo:");
            foreach (var equipo in equiposConEntrenadores)
            {
                int numeroJugadores = equipoBD.GetNumeroJugadoresPorEquipo(equipo.Id);
                Console.WriteLine($"Equipo: {equipo.Nombre}, Número de Jugadores: {numeroJugadores}");
            }

            // 5. Obtener la lista de equipos que no tienen jugadores
            var equiposSinJugadores = equipoBD.GetEquiposSinJugadores();
            Console.WriteLine("\nEquipos que no tienen jugadores:");
            foreach (var equipo in equiposSinJugadores)
            {
                Console.WriteLine($"ID: {equipo.Id}, Nombre: {equipo.Nombre}, Entrenador: {equipo.Entrenador}");
            }
        }
        }
    }
