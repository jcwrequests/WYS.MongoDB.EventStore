using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.EventStore;
using MongoDB.EventStore.Core.Store;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;

namespace EventStore.Tests
{
    [TestClass]
    public class EventStore
    {
        private MongoClient client;
        private Events.SampleID id = new Events.SampleID(1);

        [TestInitialize]
        public void CREATE_CLIENT()
        {
            client = new MongoClient();
            //client.GetServer().GetDatabase("EventStore").Drop();
            BsonClassMap.RegisterClassMap<Events.SampleEvent>();
            BsonClassMap.RegisterClassMap<Events.SampleID>();
        }
        [TestMethod]
        public void STORE_NEW_EVENTS_ON_NEW_DOCUMENT()
        {
            MongoEventStore store = new MongoEventStore(client);
            
            Events.SampleEvent _event = new Events.SampleEvent{Message = "Test",id = id};
            IList<IEvent> events = new List<IEvent>();
            events.Add(_event);
            store.AppendToStream(id, 1, events);
            
        }
        [TestMethod]
        public void STORE_NEW_EVENTS_ON_EXISTSING_DOCUMENT()
        {
             MongoEventStore store = new MongoEventStore(client);
             Events.SampleEvent _event = new Events.SampleEvent { Message = "Test", id = id };
             IList<IEvent> events = new List<IEvent>();
             events.Add(_event);
             store.AppendToStream(id, 1, events);

             Events.SampleEvent _event2 = new Events.SampleEvent { Message = "Test 2", id = id };
             IList<IEvent> events2 = new List<IEvent>();
             events2.Add(_event2);
             store.AppendToStream(id, 2, events2);
        }

        [TestMethod]
        public void RETREIVE_ALL_EVENTS()
        {
             MongoEventStore store = new MongoEventStore(client);
             var events = store.LoadEvents(0, int.MaxValue);
             Assert.IsFalse(events.Count == 0);
        }
        [TestMethod]
        public void RETREIVE_EVENT_STREAM_FOR_AN_EXISTING_DOCUMENT()
        {
             MongoEventStore store = new MongoEventStore(client);
             var events = store.LoadEventStream(id);
             Assert.IsFalse(events == null);
        }
    }
}
