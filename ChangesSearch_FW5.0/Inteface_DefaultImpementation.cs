using System;

namespace ConsoleApp_NetCore
{
    
    class Inteface_DefaultImpementation
    {
        static void Main1(string[] args)
        {
            IMovable tom = new Person();
            Car tesla = new Car();
            tom.Move();     // Walking
            tesla.Move();   // Driving
            Console.ReadLine();
        }
    }

    interface IMovable
    {
        void Move()
        {
            Console.WriteLine("Walking");
        }
    }

    class Person : IMovable
    {
       
    }

     

    class Car : IMovable
    {
        public void Move()
        {
            Console.WriteLine("Driving");
        }
    }

}
