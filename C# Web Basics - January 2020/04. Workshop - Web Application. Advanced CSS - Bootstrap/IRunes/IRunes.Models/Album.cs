﻿namespace IRunes.Models
{
    using System.Collections.Generic;

    public class Album
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Cover { get; set; }

        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
