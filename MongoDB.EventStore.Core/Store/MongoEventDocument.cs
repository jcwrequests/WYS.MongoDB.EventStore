
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDB.EventStore.Core.Store
{
    public class MongoEventDocument
    {
        public IIdentity id { get; set; }
        public long version { get; set; }
        public List<IEvent> events { get; set; }
    }
}
