using System;

namespace HSL
{
    class Example
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HslColor c1 = HslColor.red;
            Console.WriteLine(c1.ToString());

            HslColor c2 = HslColor.white;
            Console.WriteLine(c2.ToString());

            HslColor c3 = HslColor.interpolateHsl(c1,c2,0.75);
            Console.WriteLine(c3.ToString());

            double[] xa = HslColor.red;
            doubleArrayOutput(xa);

            HslColor xb = new double[4]{1.0, 0.5, 0.33, 1.0};
            Console.WriteLine(xb.ToString());

            Console.WriteLine("Test HSL to RGB: ");
            double[] red = HslColor.HSLToRGB(c1);
            doubleArrayOutput(red);

            return;
        }

        private static void doubleArrayOutput(double[] xa){
            Console.Write("(");
            for(int i=0;i< xa.Length; i++ ){
                Console.Write( xa[i].ToString() );
                if(i!=xa.Length-1){
                    Console.Write(" ; ");
                }
            }
            Console.Write(")");
            Console.WriteLine();
        }
    }
}
