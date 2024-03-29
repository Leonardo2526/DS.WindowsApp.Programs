﻿using AStarAlgorythm;
using DS.System;
using System;
using System.Collections.Generic;

namespace Implementation
{
    class Input1CLZ4 : IDataInput
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public Location MaxGridPoint { get; set; }
        public List<Location> Unpassablelocations { get; set; } = new List<Location>();

        public Input1CLZ4()
        {
            int goalCoords = 49;
            int maxPointCoords = 50;

            Start = new Location(0, 0, 0);
            Goal = new Location(goalCoords, goalCoords, goalCoords);
            MaxGridPoint = new Location(maxPointCoords, maxPointCoords, maxPointCoords);

            for (var z = 0; z < MaxGridPoint.Z; z++)
            {
                for (var x = 15; x < 20; x++)
                {
                    for (var y = 10; y < 20; y++)
                        Unpassablelocations.Add(new Location(x, y, z));

                }
            }

            AStar.WidthClearanceRCS = 4;
            AStar.HeightClearanceRCS = 4;

        }


    }
}
