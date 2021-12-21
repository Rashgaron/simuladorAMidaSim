using System;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Sink : IElemento
    {
        public Simulator simulator;
        public int entitatsEliminades;
        public int tiemposDeVida;

        public int tiempoMedioDeVida { get => tiemposDeVida / entitatsEliminades; }

        public Sink (Simulator simulator)
        {
            this.simulator = simulator;
        }

        public StateType state { get; set; }
        public int currentOcup { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SimulationStart()
        {
            entitatsEliminades = 0;
            tiemposDeVida = 0;
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.Muere)
            {
                entitatsEliminades ++;
                e.conexion.currentOcup --;
                tiemposDeVida += e.time - e.cliente.Created_at;
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.WriteLine($"El cliente con id {e.cliente.Id} ha abandonado la peluquería en {e.time}");
            }
        }
    }
}