using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

static public class Interact
{
    // TODO アンダーバーつけるのよくなさそう
    [DllImport("__Internal")]
    private static extern string _GetURL();

    [DllImport("__Internal")]
    public static extern void _Alert(string str);

    private static NameValueCollection ParseQueryString(string query)
    {
        var ret = new NameValueCollection();
        query = query.Trim('?');
        foreach (string pair in query.Split('&'))
        {
            string[] kv = pair.Split('=');

            string key = kv.Length == 1
              ? null : Uri.UnescapeDataString(kv[0]).Replace('+', ' ');

            string[] values = Uri.UnescapeDataString(
              kv.Length == 1 ? kv[0] : kv[1]).Replace('+', ' ').Split(',');

            foreach (string value in values)
            {
                ret.Add(key, value);
            }
        }
        return ret;
    }

    static public NameValueCollection GetURLParameter()
    {
        string url = _GetURL();
        Uri uri = new Uri(url);
        return ParseQueryString(uri.Query);
    }

    static public string GetOneValueWithKey(string key)
    {
        var query = GetURLParameter();
        if (query[key] == null)
        {
            _Alert(key + " key must have only one parameter.");
            return "";
        }
        return query[key];
    }
}
