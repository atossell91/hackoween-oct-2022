using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    class Game
    {
        public Game()
        {
            AllFrames = new List<ISelectable>();
        }

        const string FRAME_FILE = "frames.json";
        private List<ISelectable> AllFrames;

        public void LoadFrames()
        {
            List<UserFrame> uFrames = JsonSerializer.Deserialize<List<UserFrame>>(FRAME_FILE);
            foreach (UserFrame uf in uFrames)
            {
                if (uf.Type == "Action")
                {
                    AllFrames.Add(new GameAction(uf));
                }
                else
                {
                    AllFrames.Add(new Frame(uf));
                }
            }
        }
        public void ChooseA()
        {

        }
        public void ChooseB()
        {

        }
    }
}
