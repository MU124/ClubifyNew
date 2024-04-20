namespace Clubify.Models
{
    public class Settings
    {
        public string ClubName { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string AboutUs { get; set; }
        public string SmtpServerAddress { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public bool? UseSslTls { get; set; }
        public string DefaultEmailSubjectPrefix { get; set; }
        public string DefaultEmailBody { get; set; }
        public string EmailFooter { get; set; }
        public string DefaultCcEmail { get; set; }
        public string DefaultBccEmail { get; set; }
        public int EmailSendingLimitPerHour { get; set; }
    }
}
