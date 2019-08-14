namespace CarRentalDDD.Infra.Emails
{
    public class Email
    {
        public string From { get; }
        public string To { get; }
        public string Subject { get; }
        public string Content { get; }

        public Email(string from, string to, string subject, string content)
        {
            this.From = from;
            this.To = to;
            this.Subject = subject;
            this.Content = content;
        }
    }
}
