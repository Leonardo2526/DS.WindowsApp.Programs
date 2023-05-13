using System.Collections.Generic;
using DS.PathSearch.GridMap;
using DS.RevitLib.Utils.Collisions.Detectors;
using DS.RevitLib.Utils.Various;

namespace FrancoGustavo 
{ 
    public class FGAlgorythm
    {
        public static List<PathFinderNode> GetPathByMap(IMap Map, IPathRequiment pathRequiment,
            CollisionDetectorByTrace collisionDetector, 
            PointConverter pointConverter)
        {
            List<PathFinderNode> path = new List<PathFinderNode>();

            List<sbyte[,]> searchPlanes = ElementPosition.GetPlane(Map.Start, Map.Goal);

            PathFinder mPathFinder = new PathFinder(Map.Matrix, pathRequiment, collisionDetector, pointConverter);
            foreach (sbyte[,] item in searchPlanes)
            {
                path = mPathFinder.FindPath(Map.Start, Map.Goal, item);

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
