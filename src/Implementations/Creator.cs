using SimuladorAMedida.src.Enums;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src.Implementations
{
    public class Creator : IElemento
    {
        public int entitatsCreades;
        public StateType state = StateType.Service;
        public Simulator simulator;
        public int parameter;

        public Silla silla;

        public Creator(Simulator simulator, int parameter)
        {
            this.simulator = simulator;
            this.parameter = parameter;
        }

        public void InitCreator(Silla silla)
        {
            this.silla = silla;
        }

        public void SimulationStart()
        {
            entitatsCreades = 0;
            ProperaArribada(0);
        }

        public void TractarEsdeveniment(Event e)
        {
            if(e.type == EventType.NextArribal)
                ProcessNextArrival(e);
        }

        private void ProperaArribada(int time)
        {
            int tempsEntreArribades = parameter;
            simulator.AfegirEsdeveniment(new Event(this, time + tempsEntreArribades, EventType.NextArribal));
        }

        private void ProcessNextArrival(Event e)
        {
            ProperaArribada(e.time);

            entitatsCreades++;

            simulator.AfegirEsdeveniment(new Event(silla, e.time, EventType.SillaOcupada));
        }
    }
}