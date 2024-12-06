namespace FinanceManager.Model;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public static class Factories
    {
        public static User Create(string name, string email, string pass)
        {
            return new User()
            {
                Name = name,
                Email = email,
                PasswordHash = pass,
                CreatedAt = DateTime.Now
            };
        }
    }
}
