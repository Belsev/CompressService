﻿using System.ServiceProcess;

namespace TrackService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TrackService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
