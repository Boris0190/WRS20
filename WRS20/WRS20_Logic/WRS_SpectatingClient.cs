using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WRS20_Logic.HTTPCommands;
using WRS20_Logic.JsonObjects;

namespace WRS20_Logic
{
    public class WRS_SpectatingClient
    {
        public delegate void ClientDisconnectedH(JClient client);
        public event ClientDisconnectedH ClientDisconnected;

        public delegate void ClientConnectedH(JClient client);
        public event ClientConnectedH ClientConnected;


        public delegate void ShotSpawnedH(JShot shot);
        public event ShotSpawnedH ShotSpawned;

        public delegate void ShotDespawnedH(JShot shot);
        public event ShotDespawnedH ShotDespawned;

        public delegate void NewHttpDataH();
        public event NewHttpDataH NewHttpData;


        private int refreshHttpInterval = 500;
        public int RefreshHttpInterval
        {
            get { return refreshHttpInterval; }
            set { refreshHttpInterval = value; }
        }

        string uri = "http://server:31337/";
        public string Uri
        {
            get { return uri; }
            set { uri = value; }
        }

        public object lockClientArr = new object();
        private List<JClient> clientArr = new List<JClient>();
        public List<JClient> ClientArr
        {
            get { return clientArr; }
        }

        public object lockShotArr = new object();
        private List<JShot> shotArr = new List<JShot>();
        public List<JShot> ShotArr
        {
            get { return shotArr; }
        }

        private JDump currentDump;
        public JDump CurrentDump
        {
            get { return currentDump; }
        }

        private JConfiguration configuration;
        public JConfiguration Configuration
        {
            get { return configuration; }
        }

        System.Timers.Timer httpTimer;
        public WRS_SpectatingClient()
        {
            CConfiguration config = new CConfiguration();
            config.uri = uri;

            configuration = new JConfiguration(config.SendCommand());
            if (!configuration.IsValid) throw new Exception("Fehler beim Verbinden. Empfangene Configuration nicht gültig");

            httpTimer = new System.Timers.Timer();
            httpTimer.Interval = (double)refreshHttpInterval;
            httpTimer.Elapsed += httpTimer_Elapsed;
            httpTimer.Enabled = true;
        }

        void httpTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CDump newDump = new CDump();
            newDump.uri = uri;
            JDump newJDump = new JDump(newDump.SendCommand());
            if (newJDump.IsValid) currentDump = newJDump;
            parseDumpToArrays();
            if (NewHttpData != null) NewHttpData();
        }

        void parseDumpToArrays()
        {
            //lösche nicht mehr existente Clients aus Array und aktualisiere die Werte der vorhandenen
            #region delete Clients
            for (int i = clientArr.Count - 1; i >= 0; i--)
            {
                bool found = false;
                foreach (var dumpClient in currentDump.ConnectedClients)
                {
                    if (dumpClient.Id == clientArr[i].Id)
                    {
                        found = true;
                        clientArr[i] = dumpClient;
                        break;
                    }
                }

                if (!found)
                {
                    if (ClientDisconnected != null) ClientDisconnected(clientArr[i]);
                    lock (lockClientArr) { clientArr.RemoveAt(i); }
                }
            }
            #endregion

            //lösche nicht mehr existente Shots aus Array und aktualisiere die Werte der vorhandenen
            #region delete Shots
            for (int i = shotArr.Count - 1; i >= 0; i--)
            {
                bool found = false;
                foreach (var dumpShot in currentDump.ExistingShots)
                {
                    if (dumpShot.Id == shotArr[i].Id)
                    {
                        found = true;
                        shotArr[i] = dumpShot;
                        break;
                    }
                }

                if (!found)
                {
                    if (ShotDespawned != null) ShotDespawned(shotArr[i]);
                    lock (lockShotArr) { shotArr.RemoveAt(i); }
                }
            }
            #endregion

            //Clients in Array aufnehmen (und event werfen)
            #region add Clients
            foreach (var dumpClient in currentDump.ConnectedClients)
            {
                bool found = false;
                foreach (var client in clientArr)
                {
                    if (client.Id == dumpClient.Id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    lock (lockClientArr) { clientArr.Add(dumpClient); }
                    if (ClientConnected != null) ClientConnected(dumpClient);
                }
            }
            #endregion

            //Shots in Array aufnehmen
            #region add Shots
            foreach (var dumpShot in currentDump.ExistingShots)
            {
                bool found = false;
                foreach (var shot in shotArr)
                {
                    if (shot.Id == dumpShot.Id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    if (ShotSpawned != null) ShotSpawned(dumpShot);
                    lock (lockShotArr) { shotArr.Add(dumpShot); }
                }
            }
            #endregion

        }
    }
}
