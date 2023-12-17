namespace Miners.Presentation.Models
{
    public sealed class User
    {

        private static readonly User _user = new User();


        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }


        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }


        /// <summary>Gets the instance.</summary>
        /// <value>The instance.</value>
        public static User Instance => _user;

        static User() { }

        private User() { }
    }
}