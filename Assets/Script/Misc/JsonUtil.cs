using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;

public static class JsonUtil
{
    public static string Serialize(object graph)
    {
        return  Newtonsoft.Json.JsonConvert.SerializeObject(graph);
    }

    public static T Deserialize<T>(string message)
    {
        var deserialized = JsonConvert.DeserializeObject<T>(message);
        return deserialized;
    }
}

