namespace EcoTrueke.Infrastructure.Security
{
    public static class PasswordGenerator
    {
        public static string RandomPassword()
        {
            const int longitud = 8; // password length
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            var random = new Random();

            return new string(Enumerable.Repeat(caracteres, longitud)
                                         .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
