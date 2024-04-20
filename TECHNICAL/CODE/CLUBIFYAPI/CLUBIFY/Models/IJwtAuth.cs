namespace Clubify.Models
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);

        string Authentication();
    }
}
