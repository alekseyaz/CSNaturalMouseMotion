//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Text;
//using System.Threading;

//namespace NaturalMouseMotion.Tools
//{
//    public class SystemDiagnosis
//    {

//        public static void validateMouseMovement()
//        {
//            try
//            {
//                java.awt.Robot robot = new java.awt.Robot();
//                SystemDiagnosis.validateMouseMovement(new DefaultSystemCalls(robot), new DefaultMouseInfoAccessor());
//            }
//            catch (java.awt.AWTException e)
//            {
//                throw new RuntimeException(e);
//            }

//        }

//        public static void validateMouseMovement(SystemCalls system, MouseInfoAccessor accessor)
//        {
//            java.awt.Dimension dimension = system.getScreenSize();
//            for (int y = 0; (y < dimension.height); y += 50)
//            {
//                for (int x = 0; (x < dimension.width); x += 50)
//                {
//                    system.setMousePosition(x, y);
//                    try
//                    {
//                        Thread.Sleep(1);
//                    }
//                    catch (InterruptedException e)
//                    {
//                        Thread.CurrentThread.Interrupt();
//                    }

//                    Point p = accessor.getMousePosition();
//                    if (((x != p.x)
//                                || (y != p.y)))
//                    {
//                        throw new IllegalStateException(("Tried to move mouse to ("
//                                        + (x + (", "
//                                        + (y + ("). Actually moved to ("
//                                        + (p.x + (", "
//                                        + (p.y + (")" + ("This means NaturalMouseMotion is not able to work optimally on this system as the cursor move " + "calls may miss the target pixels on the screen.")))))))))));
//                    }

//                }

//            }

//        }
//    }
//}
