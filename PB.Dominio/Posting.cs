using System;

namespace PB.Domain
{
    public class Posting
    {
        public int posting_code { get; set; }
        public string title { get; set; }
        public string body_text { get; set; }
        public DateTime posting_date { get; set; }
        public int subject_code { get; set; }
        public string description_subject { get; set; }
        public int views { get; set; }
    }
}
