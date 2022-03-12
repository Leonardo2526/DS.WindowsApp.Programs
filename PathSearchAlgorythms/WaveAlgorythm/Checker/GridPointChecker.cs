﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.System;

namespace WaveAlgorythm
{
    class GridPointChecker
    {
        public GridPointChecker(Location nextPoint)
        {
            NextPoint = nextPoint;
        }

        public Location NextPoint { get; set; }

        public void Check()
        {
            PointsCheker pointsCheker = new PointsCheker();

            if (!WaveExpander.Grid.ContainsKey(NextPoint))
            {

                bool checkUnpassablePoint = pointsCheker.IsPointPassable(NextPoint);
                if (checkUnpassablePoint)
                {
                    // распространяем волну
                    NeighboursPasser.D = NeighboursPasser.CurrentD + 1;
                    WaveExpander.Grid.Add(NextPoint, NeighboursPasser.D);
                    NeighboursPasser.A++;
                }
            }
        }
    }
}
