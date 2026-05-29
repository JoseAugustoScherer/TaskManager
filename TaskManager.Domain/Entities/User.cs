using TaskManager.Domain.Entities.Base;

namespace TaskManager.Domain.Entities;

public class User : SoftDeleteEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    // EF constructor
    private User(){}
    
    // Primary Constructor
    private User(string name, string email, string password) 
        => (Name, Email, Password) = (name, email, password);
    
    // Factory
    public static User Create(string name, string email, string password) 
        => new (name,email, password);
    
    // Class Methods
    public void UpdateName(string newName) => Name = newName;
    public void UpdateEmail(string newEmail) => Email = newEmail;
    public void UpdatePassword(string newPassword) => Password = newPassword;
}