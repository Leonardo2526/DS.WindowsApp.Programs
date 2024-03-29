﻿using System;
using System.Collections.Generic;

namespace FindIntersection
{
    class Program
    {

        static readonly List<Line> Line_XY = new List<Line>()
        {
            new Line { X1 = 1, Y1 = 10, X2 = 5, Y2 = 13},
            new Line { X1 = 9, Y1 = 12, X2 = 10, Y2 = 1}
        };

        static void Main(string[] args)
        {
            if (IfIntersectionAvailable() == false)
            {
                Console.WriteLine("There are No avilable intersecions.");
                return;
            }

            double Xa = 0;
            double Ya = 0;

            //Check if lines are vertical
            if (Line_XY[0].X1 == Line_XY[0].X2 || Line_XY[1].X1 == Line_XY[1].X2)
                GetCoordinatesIfVertical(ref Xa, ref Ya);
            else
            {
                //Get coefficients values of line equation 
                double A1 = (Line_XY[0].Y1 - Line_XY[0].Y2) / (Line_XY[0].X1 - Line_XY[0].X2);
                double A2 = (Line_XY[1].Y1 - Line_XY[1].Y2) / (Line_XY[1].X1 - Line_XY[1].X2);
                double b1 = Line_XY[0].Y1 - (A1 * Line_XY[0].X1);
                double b2 = Line_XY[1].Y1 - (A2 * Line_XY[1].X1);

                //Parallel segments
                if (A1 == A2)
                {
                    Console.WriteLine("Lines are parallel.");
                    return;
                }

                //Get intersection coordinates
                Xa = (b2 - b1) / (A1 - A2);
                Ya = A1 * Xa + b1;
            }



            //Transform float to string with double precision
            string XaOut = String.Format("{0:0.00}", Xa);
            string YaOut = String.Format("{0:0.00}", Ya);

            //Output
            Console.WriteLine(XaOut);
            Console.WriteLine(YaOut);

            //Check if intersecion is out of bounds
            if (CheckIntersection(Xa, Ya) == false)
                Console.WriteLine("Intersection is out of bound");
            else
                Console.WriteLine("Intersection is present.");


            Console.ReadLine();
        }

        static bool CheckIntersection(double Xa, double Ya)
        {
            if (Xa == 0)
            {
                if (Ya < Math.Max(Math.Min(Line_XY[0].Y1, Line_XY[0].Y2), Math.Min(Line_XY[1].Y1, Line_XY[1].Y2)))
                    return false;
                else if (Ya > Math.Min(Math.Max(Line_XY[0].Y1, Line_XY[0].Y2), Math.Max(Line_XY[1].Y1, Line_XY[1].Y2)))
                    return false;
            }
            else
            {
                if (Xa < Math.Max(Math.Min(Line_XY[0].X1, Line_XY[0].X2), Math.Min(Line_XY[1].X1, Line_XY[1].X2)))
                    return false;
                else if (Xa > Math.Min(Math.Max(Line_XY[0].X1, Line_XY[0].X2), Math.Max(Line_XY[1].X1, Line_XY[1].X2)))
                    return false;
            }

            return true;

        }

        static bool IfIntersectionAvailable()
        {
            //Check if intersection is available
            if (Math.Max(Line_XY[0].X1, Line_XY[0].X2) < Math.Min(Line_XY[1].X1, Line_XY[1].X2) |
                Math.Max(Line_XY[0].Y1, Line_XY[0].Y2) < Math.Min(Line_XY[1].Y1, Line_XY[1].Y2))
                return false;
            else if (Math.Max(Line_XY[1].X1, Line_XY[1].X2) < Math.Min(Line_XY[0].X1, Line_XY[0].X2) |
                Math.Max(Line_XY[1].Y1, Line_XY[1].Y2) < Math.Min(Line_XY[0].Y1, Line_XY[0].Y2))
                return false;
            return true;
        }


        static void GetCoordinatesIfVertical(ref double Xa, ref double Ya)
        {
            if (Line_XY[0].X1 == Line_XY[0].X2)
            {
                Xa = Line_XY[0].X1;

                double A2 = (Line_XY[1].Y1 - Line_XY[1].Y2) / (Line_XY[1].X1 - Line_XY[1].X2);
                double b2 = Line_XY[1].Y1 - (A2 * Line_XY[1].X1);
                Ya = A2 * Xa + b2;
            }
            else
            {
                Xa = Line_XY[1].X1;
                double A1 = (Line_XY[0].Y1 - Line_XY[0].Y2) / (Line_XY[0].X1 - Line_XY[0].X2);
                double b1 = Line_XY[0].Y1 - (A1 * Line_XY[0].X1);

                Ya = A1 * Xa + b1;
            }
        }

    }
}
