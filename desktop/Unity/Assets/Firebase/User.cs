using System;

[Serializable]
public class User
{
    public string email;
    public int health;

    public User(string email, int health)
    {
        this.email = email;
        this.health = health;
    }
}
