using System;

namespace AuthenticationLibrary
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
    }

    public class AuthenticationService
    {
        public event Action<string> OnError;
        public event Action<User> OnSuccess;

        private User registeredUser;

        public AuthenticationService()
        {
            registeredUser = new User
            {
                Login = "test",
                Password = "1234",
                Age = 25,
                Weight = 70.5
            };
        }

        public void Authenticate(string login, string password)
        {
            if (login != registeredUser.Login)
            {
                OnError?.Invoke("Invalid login.");
                return;
            }

            if (password != registeredUser.Password)
            {
                OnError?.Invoke("Invalid password.");
                return;
            }

            OnSuccess?.Invoke(registeredUser);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var authService = new AuthenticationService();

            authService.OnError += (message) =>
            {
                Console.WriteLine($"Error: {message}");
            };

            authService.OnSuccess += (user) =>
            {
                Console.WriteLine("Authentication successful!");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"Weight: {user.Weight}");
                Console.WriteLine($"Login: {user.Login}");
            };

            Console.Write("Enter login: ");
            string login = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            authService.Authenticate(login, password);

        }
    }
}
