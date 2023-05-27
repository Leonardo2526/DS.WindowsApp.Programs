using FrancoGustavo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PathFindTest
{
    internal static class FrancoGustavoTest
    {
        public static void Run()
        {
            var path = GetPath();
            ShowPath(path);
        }

        private static List<PathFinderNode> GetPath()
        {
            PathFindBuilder pathFinder = new PathFindBuilder();
            List<PathFinderNode> path = pathFinder.GetPath(ElementInfo.StartElemPoint,
            ElementInfo.EndElemPoint, unpassPoints, requirement, collisionDetector, pointConverter);
        }

        private static void ShowPath(List<PathFinderNode> path)
        {
            if (path == null)
                Debug.WriteLine("No available path exist!");
            else
            {

            }
        }
    }
}
