using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Servicios_Reservados_2
{
    public class LoginService
    {
        private AdaptadorBD adaptadorBD = new AdaptadorBD();

        public bool Autenticar(string usuario, string contraseña)
        {
            string hash = EncodePassword(string.Concat(usuario, contraseña));
            //Declaramos la sentencia SQL
            string sql = "SELECT COUNT(*) FROM Usuario WHERE Username = '" + usuario + "' AND Contrasena = '" + hash + "'";
            DataTable dt = adaptadorBD.consultar(sql);
            if (int.Parse(dt.Rows[0][0].ToString()) == 0) { return false; }
            else { return true; }               

        }
        public static string EncodePassword(string originalPassword)
        {
            //Clave que se utilizará para encriptar el usuario y la contraseña
            string clave = "7f9facc418f74439c5e9709832;0ab8a5:OCOdN5Wl,q8SLIQz8i|8agmu¬s13Q7ZXyno/";
            //Se instancia el objeto sha512 para posteriormente usarlo para calcular la matriz de bytes especificada
            SHA512 sha512 = new SHA512CryptoServiceProvider();

            //Se crea un arreglo llamada inputbytes donde se convierte el usuario, la contraseña y la clave a una secuencia de bytes.
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword + clave);
            //Se calcula la matriz de bytes del arreglo anterior y se encripta.
            byte[] hash = sha512.ComputeHash(inputBytes);
            //Convertimos el arreglo de bytes a cadena.
            return Convert.ToBase64String(hash);
        }


        internal DataTable prConsultaUsuario(string usuario, string contraseña)
        {
            string hash = EncodePassword(string.Concat(usuario, contraseña));
            //Declaramos la sentencia SQL
            string sql = "SELECT username FROM Usuario WHERE Username = '" + usuario + "' AND Contrasena = '" + hash + "'";
            return adaptadorBD.consultar(sql); 
        }

        internal DataTable rolesUsuario(string usuario)
        {
            //Declaramos la sentencia SQL
            string sql = "SELECT rol FROM UsuarioRol WHERE usuario = '" + usuario + "'";
            return adaptadorBD.consultar(sql);
        }

        internal DataTable infoUsuario(string usuario)
        {
            //Declaramos la sentencia SQL
            string sql = "SELECT estacion, reestablecer, activo FROM Usuario WHERE Username = '" + usuario + "'";
            return adaptadorBD.consultar(sql);
        }
    }
}
