using ProtoBuf;
using System;
using System.Collections.Generic;

namespace ProtoBufExample.Models
{
    [ProtoContract]
    public class TestContact
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Address { get; set; }
        [ProtoMember(4)]
        public DateTime Time { get; set; }
        [ProtoMember(5)]
        public List<TestContact2> Datas { get; set; } = new List<TestContact2>();
    }

    [ProtoContract]
    public class TestResponse
    {
        [ProtoMember(1)]
        public IEnumerable<TestContact> DateMatchs { get; set; }
    }

    [ProtoContract]
    public class TestContact2
    {
        [ProtoMember(1)]
        public int ID { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public string Address { get; set; }
        [ProtoMember(4)]
        public DateTime Time { get; set; }
    }
}