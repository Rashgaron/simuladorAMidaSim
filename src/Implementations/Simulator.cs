using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorAMedida.src.Distributions;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Simulator : ISimulator
    {
        int maxTimeBetweenArrivals;
        int currentTime;
        int maxTime;
        List<Event> eventList = new List<Event>();
        Creator creator;
        Barbero barbero;
        Sink sink;
        public int tiempoDeCorte = 10; 
        public int MAX_OCUP = 4; 
        public int TIME_BETWEEN_ARRIVALS = 2; 
        public int MAX_SIMULATION_TIME = 200;
        public List<List<int>> horario;

        public Simulator()
        {
            Event simulationStart = new Event(this, 0, EventType.SimulationStart, null, null);
            eventList.Add(simulationStart);
        } 

        public void Run()
        {
            currentTime = 0;
            Configurar();
            CrearModel();
            PrintDatosConfigurados();

            while (currentTime < maxTime)
            {
                eventList.Sort((x, y) => x.time.CompareTo(y.time));
                Event e = eventList.First();
                eventList.Remove(e);
                currentTime = e.time;
                e.@object.TractarEsdeveniment(e);
            }
            PrintStatistics();
        }
        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.SimulationStart)
            {
                creator.SimulationStart();
                barbero.SimulationStart();
                sink.SimulationStart();
            }
        }

        public void AfegirEsdeveniment(Event e)
        {
            eventList.Add(e);
        }

        public int GetTimeOfNextDie()
        {
            List<Event> events = eventList.Where(e => e.type == EventType.Muere).ToList();
            if(events.Count > 0){
                int minTime = events.Min(e => e.time);
                return minTime;
            }
            return -1;
        }
        
#region Estadisticos
        private void PrintStatistics()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("==========================");
            Console.WriteLine($"Entidades eliminadas: {this.sink.entitatsEliminades}");
            Console.WriteLine($"Tiempo medio de vida: {this.sink.tiempoMedioDeVida}");
            Console.WriteLine($"Gente aburrida: {this.barbero.genteAburrida}");
            Console.WriteLine($"Gente que no ha podido entrar en la peluquería: {this.creator.noHaPodidoEntrar}");
            Console.WriteLine("==========================");
        }
#endregion
#region Configuracion
        private void ReadHorarioBarberos(int nBarberos)
            {
                horario = new List<List<int>>();
                for(int i = 0; i < nBarberos; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Entrada del horario del barbero {i} (0 - {maxTime})");
                    List<int> horarioBarbero = new List<int>();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    int entrada = Convert.ToInt32(Console.ReadLine());
                    horarioBarbero.Add(entrada);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Salida del horario del barbero {i} ({entrada} - {maxTime})");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    int salida = Convert.ToInt32(Console.ReadLine());
                    horarioBarbero.Add(salida);
                    horario.Add(horarioBarbero);
                }
            }
        private void PrintDatosConfigurados()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("==========================");
                Console.WriteLine($"Simulacion iniciada con {maxTime} segundos de simulación");
                Console.WriteLine($"Tiempo maximo entre llegadas: {maxTimeBetweenArrivals}");
                Console.WriteLine($"Tiempo de corte: {tiempoDeCorte}");
                int index = 0;
                horario.ForEach((x) => {
                    Console.WriteLine($"Barbero {index}: {x[0]} - {x[1]}");
                    index ++;
                });
                Console.WriteLine("==========================");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey();
            }
        private void Configurar()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introdueix el temps màxim de la simulació:");
                Console.ForegroundColor = ConsoleColor.Gray;
                maxTime = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introduce el tiempo de corte:");
                Console.ForegroundColor = ConsoleColor.Gray;
                tiempoDeCorte = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introduce el tiempo máximo entre llegadas:");
                Console.ForegroundColor = ConsoleColor.Gray;
                maxTimeBetweenArrivals = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Introduce la cantidad de barberos que quieres:");
                Console.ForegroundColor = ConsoleColor.Gray;
                int barberos = Convert.ToInt32(Console.ReadLine());
                ReadHorarioBarberos(barberos); 
            }

#endregion
#region Funciones de creación de modelo

        private void CrearModel()
        {
            creator = new Creator(this, maxTimeBetweenArrivals);
            barbero = new Barbero(this, tiempoDeCorte, horario);
            sink = new Sink(this);
            barbero.InitBarbero(sink);
            creator.InitCreator(barbero, new Exponential(1, this.maxTimeBetweenArrivals));
        }
#endregion
#region ISimulator
        public int currentOcup { get; set; }
        public StateType state { get; set; }
        public void SimulationStart(){}

#endregion

    }
}