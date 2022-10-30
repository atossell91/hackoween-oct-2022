using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    class GameEventArgs : EventArgs
    {
        public Frame Frame;
    }
    class Game
    {
        public delegate void GameEventHandler(Object sender, GameEventArgs e);
        public event GameEventHandler UpdateDisplayEvent;
        public event EventHandler FormClosedRequest;
        public int Health = 100;
        Random random;
        public const int RAND_MIN = 1;
        public const int RAND_MAX = 100;
        public Game()
        {
            random = new Random();
            AllFrames = new List<ISelectable>();
            initActions();
        }

        public void Init()
        {
            LoadFrames();
            ISelectable s = AllFrames[0];
            while (s.GetType() == Type.Action)
            {
                s = findFrame(HandleAction((GameAction)s));
            }
            if (s.GetType() == Type.Frame)
            {
                Frame f = (Frame)s;
                UpdateFrameDisplay(f);
                currentFrame = f;
            }
            //Frame f = (Frame)AllFrames.Find((o) => { return o.GetType() == Type.Frame; });
            //UpdateFrameDisplay(f);
        }

        Dictionary<string, Action<Object>> gameFuncs = new Dictionary<string, Action<Object>>();

        const string FRAME_FILE = "frames.json";
        private List<ISelectable> AllFrames;
        ISelectable currentFrame;

        public void UpdateFrameDisplay(Frame f)
        {
            GameEventArgs e = new GameEventArgs();
            e.Frame = f;
            UpdateDisplayEvent?.Invoke(this, e);
        }
        public void LoadFrames()
        {
            string content = File.ReadAllText(FRAME_FILE);
            List<UserFrame> uFrames = JsonSerializer.Deserialize<List<UserFrame>>(content);
            foreach (UserFrame uf in uFrames)
            {
                if (uf.Type == "Action")
                {
                    GameAction a = new GameAction(uf);
                    AllFrames.Add(a);
                }
                else
                {
                    AllFrames.Add(new Frame(uf));
                }
            }
        }
        void initActions()
        
        {
            gameFuncs.Add("modifyHealth", (o) => { Health += int.Parse(o.ToString()); });
            gameFuncs.Add("none", (o) => {});
            gameFuncs.Add("fbScript", (o) => {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.FileName = "python";
                psi.Arguments = "HACKATHON.py";

                Process p = new Process();
                p.StartInfo = psi;
                p.Start();
            });
            gameFuncs.Add("endGame", (o) => { FormClosedRequest?.Invoke(this, EventArgs.Empty); });
        }

        ISelectable findFrame(string name)
        {
            foreach (ISelectable f in AllFrames)
            {
                if (f.GetName() == name)
                {
                    return f;
                }
            }
            return null;
            //return AllFrames.Find((a) => { return a.GetName() == name; });
        }
        public string HandleAction(GameAction a)
        {
            int rand = random.Next(RAND_MIN, RAND_MAX+1);
            int threshold = (int)(a.SuccessChance * RAND_MAX);

            gameFuncs[a.ActionName].Invoke(a.Parameter);
            
            if (rand <= threshold)
            {
                return a.SuccessOutcome;
            }
            else
            {
                return a.FailureOuctome;
            }
        }
        public void HandleChoice(string choice)
        {
            ISelectable mo = findFrame(choice);
            while (mo.GetType() == Type.Action)
            {
                mo = findFrame(HandleAction((GameAction)mo));
                if (mo == null)
                {
                    throw new ArgumentNullException("HandleChoice: mo cannot be null");
                }
            }

            UpdateFrameDisplay((Frame)mo);
            currentFrame = (Frame)mo;
        }
        public void ChooseA()
        {
            Frame f = (Frame)currentFrame;
            HandleChoice(f.OutcomeA);
        }
        public void ChooseB()
        {
            Frame f = (Frame)currentFrame;
            HandleChoice(f.OutcomeB);
        }
    }
}
