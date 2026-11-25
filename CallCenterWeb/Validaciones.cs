using System.Text.RegularExpressions;

namespace CallCenterWeb
{
    public static class Validaciones
    {
        // DNI: solo números, entre 7 y 8 dígitos
        public static bool DniValido(string dni)
        {
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        // Email: algo@algo.algo, sin espacios
        public static bool EmailValido(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Teléfono: números, espacios y guiones, entre 6 y 20 caracteres (ej: 11-4000-1111)
        public static bool TelefonoValido(string telefono)
        {
            return Regex.IsMatch(telefono, @"^[\d\s-]{6,20}$");
        }
    }
}