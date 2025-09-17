using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Library.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string url)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(url);
        }
        public IDatabase GetDb(int dbindex)
        {
            return _connectionMultiplexer.GetDatabase(dbindex);
        }
    }
}
