using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WRS20_Logic;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing.Drawing2D;
using WRS20_Logic.JsonObjects;
using WRS20_Logic.HTTPCommands;

namespace WRS_WinFormsGui
{
    public partial class WRS_Gui : UserControl
    {
        private int interpInterval = 5;
        private Image bg = Properties.Resources.stars;
        private System.Timers.Timer interpolateTimer;

        private double currViewX;
        private double currViewSizeX;
        private double currViewY;
        private double currViewSizeY;

        public object lockClientArrInterp = new object();
        private List<InterpolatedClient> clientArrInterp = new List<InterpolatedClient>();
        public object lockShotArrInterp = new object();
        private List<InterpolatedShot> shotArrInterp = new List<InterpolatedShot>();

        public JClient focusedClient = null;

        public WRS_SpectatingClient specClient;

        public WRS_Gui()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public void Start()
        {
            specClient = new WRS_SpectatingClient();
            specClient.ClientConnected += specClient_ClientConnected;
            specClient.ClientDisconnected += specClient_ClientDisconnected;

            specClient.ShotSpawned += specClient_ShotSpawned;
            specClient.ShotDespawned += specClient_ShotDespawned;

            specClient.NewHttpData += specClient_NewHttpData;

            interpolateTimer = new System.Timers.Timer();
            interpolateTimer.Interval = interpInterval;
            interpolateTimer.Elapsed += interpolateTimer_Elapsed;
            interpolateTimer.Start();

        }

        void specClient_NewHttpData()
        {
            lock (lockClientArrInterp)
            {

                foreach (var clientInt in clientArrInterp)
                {
                    var newClient = specClient.ClientArr.FirstOrDefault(C => C.Id == clientInt.Id);
                    if (newClient != default(JClient)) clientInt.UpdateData(newClient);
                }
            }

            lock (lockShotArrInterp) 
            {
                foreach (var shotInt in shotArrInterp)
                {
                    var newShot = specClient.ShotArr.FirstOrDefault(S => S.Id == shotInt.Id);
                    if (newShot != default(JShot)) shotInt.UpdateData(newShot);
                }
            }
        }

        private DateTime lastTick = DateTime.Now;
        void interpolateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan tick = DateTime.Now - lastTick;
            lastTick = DateTime.Now;

            lock (lockClientArrInterp) { foreach (var pl in clientArrInterp) pl.Tick(tick); }
            lock (lockShotArrInterp) { foreach (var bu in shotArrInterp) bu.Tick(tick); }

            this.Invalidate();
        }

        void specClient_ClientDisconnected(JClient client)
        {
            lock (lockClientArrInterp) { clientArrInterp.RemoveAll(C => C.Id == client.Id); }
        }

        void specClient_ClientConnected(JClient client)
        {
            lock (lockClientArrInterp) { clientArrInterp.Add(new InterpolatedClient(client.ContentString)); }
        }

        void specClient_ShotDespawned(JShot shot)
        {
            lock (lockShotArrInterp) { shotArrInterp.RemoveAll(S => S.Id == shot.Id); }
        }

        void specClient_ShotSpawned(JShot shot)
        {
            lock (lockShotArrInterp) { shotArrInterp.Add(new InterpolatedShot(shot.ContentString)); }
        }




        private void WRS_Gui_Load(object sender, EventArgs e)
        {
        }


        private void WRS_Gui_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.DrawRectangle(Pens.Black, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

            tileBackground(g);
            if (specClient == null) return;

            if (focusedClient == null)
            {
                currViewX = -specClient.Configuration.GameZone;
                currViewSizeX = -currViewX * 2;
                currViewY = currViewX;
                currViewSizeY = currViewSizeX;
            }
            else
            {
                currViewX = focusedClient.X - ClientSize.Width / 2;
                currViewSizeX = ClientSize.Width;

                currViewY = focusedClient.Y - ClientSize.Height / 2;
                currViewSizeY = ClientSize.Height;
            }

            int minClientSize = 10;

            double relPSize = (specClient.Configuration.ShipRadius * 2) / (currViewSizeX / ClientSize.Width);
            relPSize = relPSize < minClientSize ? minClientSize : relPSize;

            double bulletSize = 5;
            double relBSize = ClientSize.Width / ((currViewX * 2) / (bulletSize * 2));
            relBSize = 5;

            lock (lockClientArrInterp)
            {
                foreach (var client in clientArrInterp)
                {
                    double relPosX = fitValueToCanvasX(client.X);
                    double relPosY = fitValueToCanvasY(client.Y);

                    double px = relPosX - relPSize / 2;
                    double py = relPosY - relPSize / 2;

                    if (relPSize == minClientSize)
                    {
                        g.FillEllipse(Brushes.Gray, (float)px, (float)py, (float)relPSize, (float)relPSize);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.White, (float)px, (float)py, (float)relPSize, (float)relPSize);
                        g.DrawLine(Pens.White, (float)relPosX, (float)relPosY, (float)(relPosX + client.Dx), (float)(relPosY + client.Dy));
                    }


                    g.DrawString(client.Name, SystemFonts.DefaultFont, Brushes.Red, (float)relPosX + 5, (float)relPosY + 5);
                    //g.DrawString("X:" + client.X.ToString() + "; Y:" + client.Y.ToString(), SystemFonts.DefaultFont, Brushes.Red, (float)relPosX, (float)relPosY + 10);
                }
            }

            lock (lockShotArrInterp)
            {
                foreach (var shot in shotArrInterp)
                {
                    double relPosX = fitValueToCanvasX(shot.X);
                    double relPosY = fitValueToCanvasY(shot.Y);

                    double bx = relPosX - relBSize / 2;
                    double by = relPosY - relBSize / 2;

                    g.FillEllipse(Brushes.Yellow, (int)bx, (int)by, (float)relBSize, (float)relBSize);
                }
            }
        }

        private double fitValueToCanvasX(double value)
        {
            return ((value - currViewX) / (currViewSizeX)) * (ClientSize.Width);
        }
        private double fitValueToCanvasY(double value)
        {
            return ((value - currViewY) / (currViewSizeY)) * (ClientSize.Height);
        }

        private void tileBackground(Graphics g)
        {
            if (currViewSizeX == 0 && currViewSizeY == 0) return;
            int startBgX = 0 - ((int)currViewX % bg.Width) - bg.Width;
            int startBgY = 0 - ((int)currViewY % bg.Height) - bg.Height;

            int anzTilesX = ((int)(ClientSize.Width / bg.Width)) + 3;
            int anzTilesY = ((int)(ClientSize.Height / bg.Height)) + 3;

            for (int bgXCount = 0; bgXCount < anzTilesX; bgXCount++)
            {
                for (int bgYCount = 0; bgYCount < anzTilesY; bgYCount++)
                {
                    int x = startBgX + bgXCount * bg.Width;
                    int y = startBgY + bgYCount * bg.Height;
                    g.DrawImage(bg, new Point(x, y));
                }
            }
        }
    }
}
