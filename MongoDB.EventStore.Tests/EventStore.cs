using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB.EventStore;
using MongoDB.EventStore.Core.Store;

namespace EventStore.Tests
{
    [TestClass]
    public class EventStore
    {
        private MongoClient client;

        [TestInitialize]
        public void CREATE_CLIENT()
        {
            client = new MongoClient();
        }
        [TestMethod]
        public void STORE_NEW_EVENTS_ON_NEW_DOCUMENT()
        {
            MongoEventStore store = new MongoEventStore(client);
            //store.AppendToStream()
        }
        [TestMethod]
        public void STORE_NEW_EVENTS_ON_EXISTSING_DOCUMENT()
        {
             MongoEventStore store = new MongoEventStore(client);
        }

        [TestMethod]
        public void RETREIVE_ALL_EVENTS()
        {
             MongoEventStore store = new MongoEventStore(client);
        }
        [TestMethod]
        public void RETREIVE_EVENT_STREAM_FOR_AN_EXISTING_DOCUMENT()
        {
             MongoEventStore store = new MongoEventStore(client);
        }
    }
}
