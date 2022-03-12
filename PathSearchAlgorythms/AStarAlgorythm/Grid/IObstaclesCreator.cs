using DSUtils;

namespace AStarAlgorythm

{
    interface IObstaclesCreator
    {
        IWeightedGraph<Location> Create();
    }
}