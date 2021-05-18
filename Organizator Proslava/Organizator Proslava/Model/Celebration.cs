using System;

namespace Organizator_Proslava.Model
{
    public class Celebration
    {
        public string Type { get; set; }
        public Organizer Organizer { get; set; }
        public DateTime DateTime { get; set; }
    }
}