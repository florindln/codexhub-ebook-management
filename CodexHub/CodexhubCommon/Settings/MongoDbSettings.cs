using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodexhubCommon.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public string Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
