using System.Collections.Generic;
using System.Windows.Media.Media3D;
using DS.ClassLib.VarUtils.Collisions;
using DS.ClassLib.VarUtils.GridMap;
using DS.ClassLib.VarUtils.Points;
using DS.PathSearch;
using DS.PathSearch.GridMap;
using IMap = DS.ClassLib.VarUtils.GridMap.IMap;

namespace FrancoGustavo 
{ 
    public class FGAlgorythm
    {
        public static List<PathFinderNode> GetPathByMap(IMap Map, Point3D startPoint, Point3D endPoint, IPathRequiment pathRequiment,
            ITraceCollisionDetector collisionDetector,
            IPointConverter pointConverter)
        {
            List<PathFinderNode> path = new List<PathFinderNode>();

            var startLocationPoints = new Location((int)startPoint.X, (int)startPoint.Y, (int)startPoint.Z);
            var endLocationPoints = new Location((int)endPoint.X, (int)endPoint.Y, (int)endPoint.Z);

            List<sbyte[,]> searchPlanes = ElementPosition.GetPlane(startLocationPoints, endLocationPoints);

            PathFinder mPathFinder = new PathFinder(Map.Matrix, pathRequiment, collisionDetector, pointConverter);
            foreach (sbyte[,] item in searchPlanes)
            {
                path = mPathFinder.FindPath(
                    startLocationPoints,
                     endLocationPoints, item);
                if (path != null)
                    return path; 
            }


            return path; 
        }

        //public static List<PathFinderNode> GetPathByMap(IMap Map, IPathRequiment pathRequiment)
        //{
        //    List<PathFinderNode> path = new List<PathFinderNode>();

        //    List<sbyte[,]> searchPlanes = ElementPosition.GetPlane(Map.Start, Map.Goal);

        //    PathFinder mPathFinder = new PathFinder(Map.Matrix, pathRequiment);
        //    foreach (sbyte[,] item in searchPlanes)
        //    {
        //        path = mPathFinder.FindPath(Map.Start, Map.Goal, item);

        //        if (path != null)
        //            return path;
        //    }


        //    return path;
        //}

        //public static List<PathFinderNode> GetPathByGraph(IGraph graph)
        //{
        //    List<PathFinderNode> path = new List<PathFinderNode>();

        //    PathFinderByGraph mPathFinder = new PathFinderByGraph(graph);
        //    path = mPathFinder.FindPath(graph.Start, graph.Goal);

        //    return path;
        //}
    }
}
