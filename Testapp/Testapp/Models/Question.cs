using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Drawing;
using Xamarin.Forms;

namespace Testapp.Models
{
    [Serializable]
    public class Question
    {
        [PrimaryKey]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("module_id")]
        public int Module_id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("position")]
        public string position { get; set; }
        [Ignore]
        public Constraint xConstraint { get; set; }
        [Ignore]
        public Constraint yConstraint { get; set; }
        [Ignore]
        public Constraint widthConstraint { get; set; }
        [Ignore]
        public Constraint heightConstraint { get; set; }
        [Ignore]
        public Constraint rightConstraint { get; set; }
        [Ignore]
        public Constraint bottomConstraint { get; set; }
    }
}