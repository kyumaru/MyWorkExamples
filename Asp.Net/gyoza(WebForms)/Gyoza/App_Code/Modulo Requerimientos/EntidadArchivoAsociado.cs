using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//used to encapsulate file data to send a single object through the next layer, implemented ´cause of 4 layer architecture  

namespace Gyoza.Modulo_Requerimientos
{
    public class EntidadArchivoAsociado
    {
        int proyectId;
        String reqName;
        String fileName;
        String path;


        public EntidadArchivoAsociado(Object[] datos)
        {
            this.proyectId = Convert.ToInt32(datos[0]);
            this.reqName = datos[1].ToString();
            this.fileName = datos[2].ToString();
            this.path = datos[3].ToString();
        }

        public int ProyectId
        {
            get { return proyectId; }
            set { proyectId = value; }
        }

        public String ReqName
        {
            get { return reqName; }
            set { reqName = value; }
        }

        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public String Path
        {
            get { return path; }
            set { path = value; }
        }


    }//class end

    
}//namespace end