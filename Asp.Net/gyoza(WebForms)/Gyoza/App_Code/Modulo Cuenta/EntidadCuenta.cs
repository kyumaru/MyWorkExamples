using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gyoza.DataSet1TableAdapters;

namespace Gyoza.Modulo_Cuenta
{

    public class EntidadCuenta
    {
        String cedula;
        String nombre;
        String apellidos;
        String correo;
        String telefonoOficina;
        String telefonoCelular;
        String usuario;
        String password;
        String rol;

        public EntidadCuenta(Object[] datos)
        {
            this.cedula = datos[0].ToString();
            this.nombre = datos[1].ToString();
            this.apellidos = datos[2].ToString();
            this.correo = datos[3].ToString();
            this.rol = datos[4].ToString();
            this.usuario = datos[5].ToString();
            this.password = datos[6].ToString();
            this.telefonoOficina = datos[7].ToString();
            this.telefonoCelular = datos[8].ToString();
        }

        public String Cedula
        {
            get { return cedula; }
            set { cedula = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }

        public String Correo
        {
            get { return correo; }
            set { correo = value; }
        }

        public String TelefonoOficina
        {
            get { return telefonoOficina; }
            set { telefonoOficina = value; }
        }

        public String TelefonoCelular
        {
            get { return telefonoCelular; }
            set { telefonoCelular = value; }
        }

        public String Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public String Rol
        {
            get { return rol; }
            set { rol = value; }
        }
    }
}