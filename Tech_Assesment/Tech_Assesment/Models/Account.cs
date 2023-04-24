namespace Tech_Assesment.Models
{
    public class Account
    {
        public Question[] Property1 { get; set; }
    }

    public class Question
    {
        public string title { get; set; }
        public string description { get; set; }
        public Correct_Answers[] correct_answers { get; set; }
    }

    public class Correct_Answers
    {
        public string type { get; set; }
        public Entry[] entries { get; set; }
    }

    public class Entry
    {
        public string when { get; set; }
        public string type { get; set; }
        public int Dr { get; set; }
        public int Cr { get; set; }
    }

}
