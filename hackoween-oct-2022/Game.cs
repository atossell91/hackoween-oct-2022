using System;
using System.Collections.Generic;
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
            foreach (ISelectable s in AllFrames)
            {
                if (s.GetType() == Type.Action)
                {
                    HandleAction((GameAction)s);
                }
                else
                {
                    Frame f = (Frame)s;
                    UpdateFrameDisplay(f);
                    currentFrame = f;
                    break;
                }
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
