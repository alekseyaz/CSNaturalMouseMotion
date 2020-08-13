using CSNaturalMouseMotion.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaturalMouseMotion.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            MouseMotionFactory factory;
            //factory = new MouseMotionFactory();


            //factory = FactoryTemplates.createGrannyMotionFactory();
            //factory = FactoryTemplates.createDemoRobotMotionFactory(100);
            //factory = FactoryTemplates.createFastGamerMotionFactory();
            //factory = FactoryTemplates.createAverageComputerUserMotionFactory();
            factory = FactoryTemplates.createFastGamerMotionFactory();




            MouseMotion motion = factory.build(350, 450);
            MouseMotion motionBack = factory.build(1900, 1000);

            for (int i = 0; i < 5; ++i)
            {
                motion.move();
                Thread.Sleep(50);
                motionBack.move();
                Thread.Sleep(50);
            }


            //MouseMotion motion = factory.build(350, 450);
            //MouseMotion motionBack = factory.build(1900, 1000);

            //for (int i = 0; i < 4; ++i)
            //{
            //    motion.move();
            //    Thread.Sleep(50);
            //    motionBack.move();
            //    Thread.Sleep(50);
            //}





            Console.Read();
        }
    }
}
