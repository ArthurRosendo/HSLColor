using System;

namespace HSL
{
    class Example
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World! All of this are just tests");
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

            c1.fromRGBA(red);
            c1 = c1.getSRGBA();
            Console.WriteLine(c1.ToString());

            Console.WriteLine("");
            HslColor testbic = new HslColor(0,0,0);
            testbic.fromBicone(HslColor.red.getHue(), 1.0, HslColor.red.getLightness(),HslColor.red.getAlpha());
            testbic = HslColor.interpolateHsl(testbic, HslColor.black);
            testbic = HslColor.interpolateHsl(testbic, HslColor.black);
            Console.Write("Testbicone: ");
            Console.WriteLine(testbic.ToString());
            Console.WriteLine("Ref:     "+ HslColor.interpolateHsl(HslColor.interpolateHsl(HslColor.red,HslColor.black),HslColor.black));

            // HslColor c4 = HslColor.yellow;
            // Console.WriteLine("yel = " + c4.ToString());
            // c4 = HslColor.interpolateHsl(c4, HslColor.red);
            // Console.WriteLine("orange = " + c4.ToString());
            // c4 = HslColor.interpolateHsl(c4, HslColor.black);
            // Console.WriteLine("brown = " + c4.ToString());
            // Console.Write("Bicone test = ");
            // doubleArrayOutput( c4.getBicone());

            // HslColor radianTest = HslColor.black;
            // radianTest.setHueAngleRadians(6*Math.PI+Math.PI/2); //90 = 0.25
            // Console.WriteLine("0.25 = " +radianTest.ToString());
            // radianTest.setHueAngleRadians((-6*Math.PI-Math.PI/2)); //270 = 0.75
            // Console.WriteLine("0.75 = " +radianTest.ToString());

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
