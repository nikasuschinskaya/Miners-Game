namespace Miners.Presentation.Models
{
    public sealed class User
    {
        private static readonly User _user = new User();

        public int Id { get; set; }
        public string Name { get; set; }
        public static User Instance => _user;

        static User() { }

        private User() { }
    }
}