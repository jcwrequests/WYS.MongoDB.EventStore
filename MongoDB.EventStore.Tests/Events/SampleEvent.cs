using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.EventStore.Core.Store;
using MongoDB.EventStore;

namespace EventStore.Tests.Events
{
    [Serializable]
    public class SampleID : IIdentity
    {
        public SampleID(Guid value)
        {
            this.value = value;
        }
        public Guid value {get;set;}
    }

    [Serializable]
    public  class SampleEvent : IEvent
    {
        public SampleID id { get; set; }
        public string Message { get; set; }
    }
}
