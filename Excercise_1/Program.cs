﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise_1
{
    class Program
    {
       
        public static void Main(string[] args)
        {
            FunctionsContainer funcList = new FunctionsContainer();     // Creating the mission conatiner
            funcList["Double"] = val => val * 2;                    // Double the Value
            funcList["Triple"] = val => val * 3;                    // Triple the Value
            funcList["Square"] = val => val * val;                  // Square the Value
            funcList["Sqrt"] = val => Math.Sqrt(val);               // Taking the square root
            funcList["Plus2"] = val => val + 2;                    // Double the Value

            // This handler will output the screen every mission that was activated and it's value
            EventHandler<double> LogHandler = (sender, val) =>
            {
                IMission mission = sender as IMission;

                if (mission != null)
                {
                    Console.WriteLine($"Mission of Type: {mission.Type} with the Name {mission.Name} returned {val}");
                }
            };

            EventHandler<double> SqrtHandler = (sender, val) =>
            {
                // This function will Create a sqrt mission and will continue to sqrt until a number less than 2
                SingleMission sqrtMission = new SingleMission(funcList["Sqrt"], "SqrtMission");

                double newVal;
                do
                {
                    newVal = sqrtMission.Calculate(val);     // getting the new Val
                    Console.WriteLine($"sqrt({val}) = {newVal}");

                    val = newVal;                           // Storing the new Val;
                } while (val > 2);
                Console.WriteLine("----------------------------------------");
            };

            ComposedMission mission1 = new ComposedMission("mission1")
                .Add(funcList["Square"])
                .Add(funcList["Sqrt"]);

            ComposedMission mission2 = new ComposedMission("mission2")
                .Add(funcList["Triple"])
                .Add(funcList["Plus2"])
                .Add(funcList["Sqrt"]);

            SingleMission mission3 = new SingleMission(funcList["Double"], "mission3");

            ComposedMission mission4 = new ComposedMission("mission4")
                .Add(funcList["Triple"])
                .Add(funcList["Stam"])              // Notice that this function does not exist and still it works
                .Add(funcList["Plus2"]);

            funcList["Stam"] = val => val + 100;
            SingleMission mission5 = new SingleMission(funcList["Stam"], "mission5");

           
            double i = mission2.Calculate(5);
            Console.WriteLine(i);

            Console.ReadKey();

        }
    }
}
