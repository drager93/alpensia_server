using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace alpensia_server.Databases
{
    public partial class Board
    {
        [JsonProperty("index")]
        public int BrdIndex { get; set; }

        [JsonProperty("title")]
        public string BrdTitle { get; set; }

        [JsonProperty("startDate")]
        public DateTime BrdStartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime BrdEndDate { get; set; }

        [JsonProperty("mainImg")]
        public string BrdMainImg { get; set; }

        [JsonProperty("detailImg")]
        public string BrdDetailImg { get; set; }

        //[JsonProperty("viewCount")]
        //public int BrdViewCount { get; set; }

        [JsonProperty("ismain")]
        public bool BrdIsMain { get; set; }
    }
}