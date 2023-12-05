using Newtonsoft.Json;
using SQLite;
using System;
using System.Drawing;

namespace Testapp.Models
{
    public class Module
    {
        [PrimaryKey]
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("pic")]
        public string Pic { get; set; }
    }
}