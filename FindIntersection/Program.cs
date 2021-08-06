using System;

namespace FindIntersection
{
    class Program
    {
        static void Main(string[] args)
        {
            //Start and end coordinates of lines
            float X1 = 8;
            float Y1 = 5;
            float X2 = 10;
            float Y2 = 12;
            float X3 = 9;
            float Y3 = 12;
            float X4 = 10;
            float Y4 = 1;

            //Get coefficients values of line equation 
            float A1 = (Y1 - Y2) / (X1 - X2);
            float A2 = (Y3 - Y4) / (X3 - X4);
            float b1 = Y1 - (A1 * X1);
            float b2 = Y3 - (A2 * X3);

            //Get intersection coordinates
            float Xa = (b2 - b1) / (A1 - A2);
            float Ya = A1 * Xa + b1;

            //Transform float to string with double precision
            string XaOut = String.Format("{0:0.00}", Xa);
            string YaOut = String.Format("{0:0.00}", Ya);

            //Output
            Console.WriteLine(XaOut);
            Console.WriteLine(YaOut);

            Console.ReadLine();
        }
    }
}
