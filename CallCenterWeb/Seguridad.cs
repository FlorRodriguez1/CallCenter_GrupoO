using Dominio;

namespace CallCenterWeb
{
    public static class Seguridad
    {
        // Devuelve true solo si hay alguien logueado y es Administrador
        public static bool EsAdministrador(object usuarioSesion)
        {
            Usuario usuario = usuarioSesion as Usuario;
            return usuario != null && usuario.Perfil == Perfil.Administrador;
        }

        public static bool EsSupervisor(object usuarioSesion)
        {
            Usuario usuario = usuarioSesion as Usuario;
            return usuario != null && usuario.Perfil == Perfil.Supervisor;
        }
    }
}