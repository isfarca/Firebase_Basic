public class User 
{
    // Database elements.
    public string Username;
    public string Email;
    
    /// <summary>
    /// Set value for elements.
    /// </summary>
    /// <param name="username">Name of current user.</param>
    /// <param name="email">Current email address.</param>
    public User(string username, string email) 
    {
        // Set current value.
        Username = username;
        Email = email;
    }
}