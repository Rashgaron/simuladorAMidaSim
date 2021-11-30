using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimuladorAMedida.src.Implementations;
using SimuladorAMedida.src.Interfaces;

namespace SimuladorAMedida.src
{
    public class Simulation
    {
        public static void Run()
        {
            ISimulator simulator = new Simulator();
            // Configure the simulation
            simulator.Run(); 
            // Show statistics

        } 
    }
}