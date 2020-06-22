using System;

namespace BoomMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            Gun newGun = BoomMaker.BuildGun();
            Console.WriteLine("Here's your new gun:");
            Console.WriteLine(newGun.getComponentsString());
            Console.WriteLine(newGun.getAttributesString());
        }
    }
}
