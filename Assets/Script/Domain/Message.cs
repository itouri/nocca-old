using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Assets.Script.Domain
{
    public class Message
    {
        public string funcName { get; set; }
        public string body { get; set; }
    }
}
