using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    public class GameActionArgs : EventArgs
    {
        public string actionName;
        public object parameter;
    }
    class GameAction : ISelectable
    {
        public delegate void GameActionEventHandler(Object sender, GameActionArgs e);
        public event GameActionEventHandler GameFuncEvent;
        Random random = new Random();
        GameAction() { }
        public GameAction(UserFrame uf)
        {
            Name = uf.Name;
            SuccessChance = uf.Odds;
            Parameter = uf.Parameter;
            ActionName = uf.ActionName;
            SuccessOutcome = uf.OutcomeA;
            FailureOuctome = uf.OutcomeB;
        }
        public string Name { get; set; }
        public double SuccessChance { get; set; }
        public string ActionName { get; set; }
        public Object Parameter { get; set; }
        public string SuccessOutcome { get; set; }
        public string FailureOuctome { get; set; }

        Type ISelectable.GetType()
        {
            return Type.Action;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
