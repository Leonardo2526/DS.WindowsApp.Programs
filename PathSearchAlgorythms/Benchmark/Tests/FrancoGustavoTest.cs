using BenchmarkDotNet.Attributes;
using DSUtils.GridMap;
using System;

namespace Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class FrancoGustavoTest
    {
        public static int Count;

        [Benchmark]
        public void FGTest()
        {
            //IMap Map = new DSUtils.GridMap.d2.Map2d100();
            IMap Map = new DSUtils.GridMap.d3.Map3d20();
            Count = FrancoGustavo.FGAlgorythm.GetPathByMap(Map, new PathRequiment1()).Count;

            //Console.WriteLine(Count);
        }


    }
}
