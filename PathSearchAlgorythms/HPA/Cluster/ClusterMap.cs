using DSUtils;
using DSUtils.GridMap;

namespace HPA
{
    struct ClusterMap : IMap
    {
        public Location Start { get; set; }
        public Location Goal { get; set; }
        public int[,,] Matrix { get; set; }
    }
}
