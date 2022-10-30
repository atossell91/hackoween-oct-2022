using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    class Frame : ISelectable
    {
        public Frame() { }

        public Frame(UserFrame uf)
        {
            Name = uf.Name;
            OutcomeA = uf.OutcomeA;
            OutcomeB = uf.OutcomeB;
            Text = uf.Text;
            ChoiceA = uf.ChoiceA;
            ChoiceB = uf.ChoiceB;
            ImagePath = uf.ImagePath;
        }

        public string Name { get; set; }
        public string OutcomeA { get; set; }
        public string OutcomeB { get; set; }
        public string Text { get; set; }
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ImagePath { get; set; }

        public string GetName()
        {
            return Name;
        }

        Type ISelectable.GetType()
        {
            return Type.Frame;
        }
    }
}
