using System;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Assets.Script.Browser
{
    static public class Interact
    {
        // TODO アンダーバーつけるのよくなさそう
        [DllImport("__Internal")]
        private static extern string _GetURL();

        [DllImport("__Internal")]
        private static extern void _Alert(string str);

        static public NameValueCollection GetURLParameter()
        {
            string url = _GetURL();
            Uri uri = new Uri(url);
            return HttpUtility.ParseQueryString(uri.Query);
        }

        static public string GetOneValueWithKey(string key)
        {
            var query = GetURLParameter();
            if (query[key].Length != 1)
            {
                _Alert(key + " key must have only one parameter.");
                return "";
            }
            return query[key];
        }
    }
}
