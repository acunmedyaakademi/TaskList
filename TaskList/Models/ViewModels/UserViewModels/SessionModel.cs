namespace TaskList.Models.ViewModels.UserViewModels
{
    public class SessionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set;}
        public bool MailConfirmed { get; set; }

    }
}
