using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public class ServerCommunication
    {
        public IEnumerator SaveDuration(Duration duration)
        {
            var header = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" }
            };
            var json = JsonUtility.ToJson(duration);
            byte[] pData = Encoding.ASCII.GetBytes(json.ToCharArray());

            var connection = new WWW(@"http://localhost:58137/api/stats/create", pData, header);
            yield return connection;
            Console.WriteLine(connection.text);
        }
    }
}
