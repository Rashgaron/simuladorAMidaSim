using System;
using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Event : IEvent 
    {
        public double Time { get; set; }
        public EventTypes Type { get; set; }
        
        public Event(double time, EventTypes type)
        {
            this.Time = time;
            this.Type = type;
        }

        public IEvent Process()
        {
            Console.Write("Procesando ...");
            return this;
        }
    }
}