using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.Points;
using DS.PathSearch;
using DS.PathSearch.GridMap;
using FrancoGustavo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace PathFindTest
{
    class PathFindBuilder
    {
        public List<Point3D> PathCoords { get; set; }

        public List<PathFinderNode> GetPath(Point3D startPoint, Point3D endPoint, List<Point3D> unpassablePoints,
            IPathRequiment pathRequiment, ITraceCollisionDetector collisionDetector, IPointConverter pointConverter)
        {         
            var uCS2startPoint = pointConverter.ConvertToUCS2(new Point3D(startPoint.X, startPoint.Y, startPoint.Z));
            var uCS2endPoint = pointConverter.ConvertToUCS2(new Point3D(endPoint.X, endPoint.Y, endPoint.Z));

            var uCS2startPointInd = uCS2startPoint.Round(1);
            var uCS2endPointInd = uCS2endPoint.Round(1);

            IMap map = new MapCreator();
            map.Start = new Location((int)uCS2startPointInd.X, (int)uCS2startPointInd.Y, (int)uCS2startPointInd.Z);
            map.Goal = new Location((int)uCS2endPointInd.X, (int)uCS2endPointInd.Y, (int)uCS2endPointInd.Z);
            var uCS1BasePoint = new Point3D(ElementInfo.MaxBoundPoint.X, ElementInfo.MaxBoundPoint.Y, ElementInfo.MaxBoundPoint.Z);
            var uCS2maxPoint = pointConverter.ConvertToUCS2(new Point3D(uCS1BasePoint.X, uCS1BasePoint.Y, uCS1BasePoint.Z));
            var uCS2maxPointInt = uCS2maxPoint.Round(1);

            map.Matrix = new int[(int)uCS2maxPointInt.X, (int)uCS2maxPointInt.Y, (int)uCS2maxPointInt.Z];

            //foreach (StepPoint unpass in InputData.UnpassStepPoints)
            //    map.Matrix[unpass.X, unpass.Y, unpass.Z] = 1;

            List<PathFinderNode> pathNodes = FGAlgorythm.GetPathByMap(map, pathRequiment, collisionDetector, pointConverter);

            return pathNodes;
        }
    }
}
