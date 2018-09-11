using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using UnityEngine;

public class RequestModel
{
    public Dictionary<string, string> data { get; set; }
    public string method { get; set; }
    public string signature { get; set; }
    public string timesamp { get; set; }
    public RequestModel()
    {
        data = new Dictionary<string, string>();
    }

    public void AddData(string key, string value)
    {
        if (!data.ContainsKey(key))
        {
            data.Add(key, value);
        }
        else
        {
            data[key] = value;
        }
    }

    public string ObjectToJson()
    {
        return JsonMapper.ToJson(this);
    }

    public byte[] GetBytes()
    {
        return Encoding.UTF8.GetBytes(JsonMapper.ToJson(this));
    }
}
