using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    class GameAction : ISelectable
    {
        GameAction() { }
        public GameAction(UserFrame uf)
        {
            Name = uf.Name;
            SuccessChance = uf.Odds;
            Parameter = uf.Parameter;
            SuccessOutcome = uf.OutcomeA;
            FailureOuctome = uf.OutcomeB;
        }
        public string Name { get; set; }
        public double SuccessChance { get; set; }
        public Object Parameter { get; set; }
        public GameAction SuccessOutcome { get; set; }
        public GameAction FailureOuctome { get; set; }

        public void SelectFrame()
        {
            throw new NotImplementedException();
        }
    }
}
