using System;
using System.Threading.Tasks;

//Mongo
using MongoDB.Bson;
using MongoDB.Driver;

namespace C3DLayersDBTool
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient();

            //Get database
            IMongoDatabase database = client.GetDatabase("C3DLayers");

            GetDatabaseNames(client).GetAwaiter();
            Console.ReadLine();
        }

        private static async Task GetDatabaseNames(MongoClient client)
        //Get all databases names from server
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var databaseDocuments = await cursor.ToListAsync();
                foreach (var databaseDocument in databaseDocuments)
                {
                    Console.WriteLine(databaseDocument["name"]);
                }
            }
        }
    }
}
