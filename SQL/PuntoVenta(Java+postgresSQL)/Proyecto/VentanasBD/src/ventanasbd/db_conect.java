/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package ventanasbd;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import javax.swing.JTable;

/**
 *
 * @author hp
 */
public class db_conect {
    Connection connection;
    Statement stmt;
    ResultSet resultSet;
    private final String nombreConexion = "jdbc:postgresql://localhost/Proyecto_Punto_de_venta?"+"user=postgres&password=rafa02661234";
    
    
    public  void  db_conect ()   {
       try{
          //  JOptionPane.showMessageDialog(null, "DB conectada");
            conexion(); 
       }catch (SQLException ex) {
        //   System.err.println("UPS!");
            System.err.println( ex.getMessage() );
        }
    }
        
    public void conexion() throws SQLException{
       
    Connection c = null;
      try {
         Class.forName("org.postgresql.Driver");
         connection = DriverManager.getConnection(nombreConexion);
      //JOptionPane.showMessageDialog(null, "Conector Ejecutado");
      } catch (ClassNotFoundException e) {
         System.err.println(e.getClass().getName()+": "+e.getMessage());
         System.exit(0);
      } catch (SQLException e) {
          System.err.println(e.getClass().getName()+": "+e.getMessage());
          System.exit(0);
        }
      //System.out.println("Opened database successfully");
      
    }//fin de conexion
    
   public void ejecutarSql(String consulta) throws SQLException{
    try
        {
      stmt = connection.createStatement();
            stmt.executeQuery(consulta);
       stmt.close();
     //  System.out.println("Conecto!");
     }
        catch (SQLException e) {
        System.err.println(e.getClass().getName()+": "+e.getMessage());
     } 
   }
   public String ejecutarRetornando(String consulta) throws SQLException{
       String resultado = "";
       try{       
       ResultSet Rs ;
      stmt = connection.createStatement();
       Rs= stmt.executeQuery(consulta);
      while (Rs.next())
      {
        System.out.println("entro hay resultado");
        resultado=Rs.getString(1);
      }
       }
        catch (SQLException e) {
        System.err.println(e.getClass().getName()+": "+e.getMessage());
     }  
       return resultado;
         
   }
      public String ejecutarRetornandoIndexado(String consulta, Integer Canal) throws SQLException{
       String resultado = "";
       try{       
       ResultSet Rs ;
      stmt = connection.createStatement();
       Rs= stmt.executeQuery(consulta);
      while (Rs.next())
      {
        System.out.println("entro hay resultado");
        resultado=Rs.getString(Canal);
      }
       }
        catch (SQLException e) {
        System.err.println(e.getClass().getName()+": "+e.getMessage());
     }  
       return resultado;
         
   }
      
      public void llenarTabla(String sql, JTable tabla) {
        try {
            //Para manejar los datos en un JTable se utiliza un modelo,
            //que por dejecto es "DefaultTableModel". Sin embargo queremos
            //utilizar uno personalizado para esta aplicación, por eso se creó
            //DBTableModel. Si no está asignado (ni creado) hay que
            //hacerlo, de lo contrario solo hace falta actualizarlo.
            if( (tabla.getModel().getClass().toString()).equalsIgnoreCase("class pruebaconexion.DBTableModel") ) {
                //Se actualiza porque ya existe y lo tiene asignado
                ((DBTableModel) tabla.getModel()).actualizar(sql, nombreConexion);
            } else {
                //Se crea uno nuevo y se asigna
                DBTableModel modelo = new DBTableModel(sql, nombreConexion);
                tabla.setModel(modelo);
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
