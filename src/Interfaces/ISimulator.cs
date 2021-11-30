using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorAMedida.src.Implementations;

namespace SimuladorAMedida.src.Interfaces
{
    public interface ISimulator
    {
        public void Run();
        public void SimulationStart();
        public void SimulationEnd();
        public void AddEvent(SimuladorAMedida.src.Implementations.Event e);
        public SimuladorAMedida.src.Implementations.Event FirstEvent();
        bool CanSimulate();
        double ActualTime();
    }
}