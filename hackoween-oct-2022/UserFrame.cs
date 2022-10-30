using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hackoween_oct_2022
{
    public class UserFrame
    {
        //  Common fields to framea and actions
        [JsonPropertyName("Type")]
        public string Type { get; set; } = "Frame";

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Outcome A")]
        public string OutcomeA { get; set; }

        [JsonPropertyName("Outcome B")]
        public string OutcomeB { get; set; }

        //  Fields that will be used by a frame
        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("Choice A")]
        public string ChoiceA { get; set; }

        [JsonPropertyName("Choice B")]
        public string ChoiceB { get; set; }

        [JsonPropertyName("Image")]
        public string ImagePath { get; set; }

        //  Fields that will be used by an action
        [JsonPropertyName("Action Name")]
        public string ActionName { get; set; } = "none";

        [JsonPropertyName("Parameter")]
        public Object Parameter { get; set; } = null;

        [JsonPropertyName("Odds")]
        public double Odds { get; set; } = 1;
    }
}
