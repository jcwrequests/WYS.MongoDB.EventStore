using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.EventStore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDB.EventStore.Core.Store
{
    public class MongoEventStore : IEventStore
    {
        private MongoClient client;
        public MongoEventStore(MongoClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            this.client = client;
            BsonClassMap.RegisterClassMap<MongoEventDocument>();
        }
        public EventStream LoadEventStream(IIdentity id)
        {
            var server = this.client.GetServer();
            var db = server.GetDatabase("EventStore");
            var query = Query<MongoEventDocument>.EQ(s => s.id, id);
            var events = db.GetCollection<MongoEventDocument>("Events");
            var doc = events.FindOneAs<MongoEventDocument>(query);
            if (doc == null) return null;
            return new EventStream { Events = doc.events, Version = doc.version };
        }

        public EventStream LoadEventStream(IIdentity id, long skipEvents, int maxCount)
        {
            var server = this.client.GetServer();
            var db = server.GetDatabase("EventStore");
            var query = Query<MongoEventDocument>.EQ(s => s.id, id);
            var events = db.GetCollection<MongoEventDocument>("Events");
            var doc = events.FindOneAs<MongoEventDocument>(query);
            if (doc == null) return null;
            return new EventStream { Events = doc.events.
                                              Skip((int)skipEvents).
                                              Take(maxCount).
                                              ToList(), 
                                    Version = doc.version };
        }

        public void AppendToStream(IIdentity id, long expectedVersion, IList<IEvent> newEvents)
        {
            var server = this.client.GetServer();
            var db = server.GetDatabase("EventStore");
            var query = Query<MongoEventDocument>.EQ(s => s.id, id);
            
            var events = db.GetCollection<MongoEventDocument>("Events");
            var doc = events.FindOneAs<MongoEventDocument>(query);
            if (doc == null) events.Insert<MongoEventDocument>(new MongoEventDocument
            {
                events = newEvents.ToList<IEvent>(),
                id = id,
                version = 1
            });
            if (doc != null)
            {
                doc.events.AddRange(newEvents);
                doc.version  += 1;
                events.Save(doc);
            }


        }

        public IList<IEvent> LoadEvents(long skipEvents, int maxCount)
        {
            var server = this.client.GetServer();
            var db = server.GetDatabase("EventStore");
            var events = db.GetCollection<MongoEventDocument>("Events");
            var docs = events.FindAllAs<MongoEventDocument>();
            if (docs == null) return null;
            return docs.ToList().SelectMany(doc => doc.events).ToList();
        }

        public event NewEventsArrivedHandler NewEventsArrived;
    }
}
