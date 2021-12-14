using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorAMedida.src.Implementations;

namespace SimuladorAMedida.src.Interfaces
{
    public interface ISimulator : IElemento
    {
        public void Run();
    }
}