using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ClientServerArchitecture
{
    public class Message : IMessage
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("X")]
        public int X { get; set; }
        [JsonProperty("Y")]
        public int Y { get; set; }
        [JsonProperty("MessageText")]
        public string MessageText { get; set; }
        [JsonProperty("IsHorisontal")]
        public bool IsHorisontal { get; set; }

    }
}
