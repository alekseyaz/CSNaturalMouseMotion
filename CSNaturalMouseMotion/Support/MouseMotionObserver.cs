using NLog;
using System;
using Zaac.CSNaturalMouseMotion.Interface;

namespace Zaac.CSNaturalMouseMotion.Support
{
    class MouseMotionObserver : IMouseMotionObserver
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        public void Observe(int xPos, int yPos)
        {

            //Можем здесь реализовать наблюдение за координатами
            //log.Info("Observe ({}, {}))", xPos, yPos);
        }
    }
}
