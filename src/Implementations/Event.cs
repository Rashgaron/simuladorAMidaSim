using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Event : IEvent
    {

        public int entitatsTractades = 0;
        public EventType type;
        public int time;
        public IElemento @object;
        public Event(IElemento elemento, int time, EventType type)
        {
            this.@object = elemento;
            this.time = time;
            this.type = type; 
        }
        

    }
}