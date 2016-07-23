using Assets.Scripts.World;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    public class GameStatistics : MonoBehaviour
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly ServerCommunication _serverCommunication = new ServerCommunication();

        public void StartLevelRecording()
        {
            _stopWatch.Start();
        }

        public void StopLevelRecording()
        {
            if (_stopWatch.IsRunning)
            {
                _stopWatch.Stop();
                var duration = new Duration()
                {
                    Level = GlobalGameObjects.World.Get().GetComponent<LoadLevel>().CurrentLevelName,
                    User = LocalIPAddress(),
                    Time = _stopWatch.ElapsedMilliseconds / 1000
                };

                StartCoroutine(_serverCommunication.SaveDuration(duration));
            }
        }

        private string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
    }
}
