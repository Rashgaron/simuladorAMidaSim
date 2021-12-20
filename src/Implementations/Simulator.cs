using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public StateType state { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int currentOcup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int tiempoDeCorte = 10; 
        public int MAX_OCUP = 4; 
        public int TIME_BETWEEN_ARRIVALS = 2; 
        public int MAX_SIMULATION_TIME = 200;
        public List<List<int>> horario;

        public Simulator()
        {
            Event simulationStart = new Event(this, 0, EventType.SimulationStart, null);
            eventList.Add(simulationStart);
        } 

        public void Run()
        {
            currentTime = 0;
            Configurar();
            CrearModel();

            while (currentTime < maxTime)
            {
                eventList.Sort((x, y) => x.time.CompareTo(y.time));
                Event e = eventList.First();
                eventList.Remove(e);
                currentTime = e.time;
                e.@object.TractarEsdeveniment(e);
            }

            Console.WriteLine($"Entidades eliminadas: {this.sink.entitatsEliminades}");
        }

        private void Configurar()
        {
            Console.WriteLine("Introdueix el temps màxim de la simulació:");
            maxTime = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce el tiempo de corte:");
            tiempoDeCorte = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce el tiempo máximo entre llegadas:");
            maxTimeBetweenArrivals = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introduce la cantidad de barberos que quieres:");
            int barberos = Convert.ToInt32(Console.ReadLine());
            horario = new List<List<int>>();
            for(int i = 0; i < barberos; i++)
            {
                Console.WriteLine($"Entrada del horario del barbero {i} (0 - {maxTime})");
                List<int> horarioBarbero = new List<int>();
                int entrada = Convert.ToInt32(Console.ReadLine());
                horarioBarbero.Add(entrada);
                Console.WriteLine($"Salida del horario del barbero {i} ({entrada} - {maxTime})" );
                int salida = Convert.ToInt32(Console.ReadLine());
                horarioBarbero.Add(salida);
                horario.Add(horarioBarbero);
            }

            Console.WriteLine($"Simulacion iniciada con {maxTime} segundos de simulación");
            Console.WriteLine($"Tiempo de corte: {tiempoDeCorte}");

            int index = 0;
            horario.ForEach((x) => {
                Console.WriteLine($"Barbero {index}: {x[0]} - {x[1]}");
                index ++;
            });

            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ReadKey();

        }

        private void CrearModel()
        {
            creator = new Creator(this, maxTimeBetweenArrivals);
            barbero = new Barbero(this, tiempoDeCorte, horario);
            sink = new Sink(this);
            barbero.InitBarbero(sink);
            creator.InitCreator(barbero);
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

        public void SimulationStart()
        {
            throw new NotImplementedException();
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
    }
}