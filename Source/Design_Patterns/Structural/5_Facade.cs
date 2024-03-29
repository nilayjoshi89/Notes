﻿
using Demo.Common.Library;

namespace Design_Patterns.StructuralPatterns
{
    public class FacadeDemo : DemoBase
    {
        public override string Name => "Structural.Facade";

        public override string ShortSummary => @"A single class that represents an entire subsystem
    - Simplified interface to complex subsystem
	- It manages all complexity of calling/collating subsystem";

        public override Task Run()
        {
            ComputerFacade computerFacade = new ComputerFacade();
            computerFacade.StartComputer();

            return Task.CompletedTask;
        }
    }

    class CPU
    {
        public void Start() => Console.WriteLine("CPU is starting");

        public void Execute() => Console.WriteLine("CPU is executing");
    }

    class Memory
    {
        public void Load() => Console.WriteLine("Memory is loading");
    }

    class HardDrive
    {
        public void Read() => Console.WriteLine("Hard Drive is reading");
    }

    class ComputerFacade
    {
        private readonly CPU cpu;
        private readonly Memory memory;
        private readonly HardDrive hardDrive;

        public ComputerFacade()
        {
            cpu = new CPU();
            memory = new Memory();
            hardDrive = new HardDrive();
        }

        public void StartComputer()
        {
            Console.WriteLine("Starting computer...");
            cpu.Start();
            memory.Load();
            hardDrive.Read();
            cpu.Execute();
            Console.WriteLine("Computer started successfully");
        }
    }
}
