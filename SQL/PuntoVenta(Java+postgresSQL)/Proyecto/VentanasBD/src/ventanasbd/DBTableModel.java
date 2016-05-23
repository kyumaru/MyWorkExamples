package ventanasbd;

import java.sql.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.table.AbstractTableModel;


/**
 *
 * @author Fernando Valverde A.
 * 
 * Se utilizó como apoyo el ModeloTabla.java de
 * la profesora Alexandra Matrínez
 * 
 */
public class DBTableModel extends AbstractTableModel   {
    private Statement instruccion;
    private ResultSet datos;
    private Connection conexion;
    private ResultSetMetaData metaDatos;
    private int cantidadFilas;
    
    public DBTableModel(String sql, String nombreConexion) {
        try {
            //Se abre la conexion utilizando el driver de postgres
            Class.forName("org.postgresql.Driver");
            conexion = DriverManager.getConnection(nombreConexion);
            instruccion = conexion.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_UPDATABLE);

            datos = instruccion.executeQuery(sql);
            metaDatos = datos.getMetaData();

            datos.last();
            cantidadFilas = datos.getRow();

            fireTableStructureChanged();
        } catch (SQLException e) {
          System.err.println(e.getClass().getName()+": "+e.getMessage());
        } catch (ClassNotFoundException ex) {
            Logger.getLogger(DBTableModel.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    
    public Class getColumnClass(int columna) throws IllegalStateException {
        try {
            String nombreClase = metaDatos.getColumnClassName(columna + 1);
            return Class.forName(nombreClase);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        return Object.class;
    }

    public int getColumnCount() throws IllegalStateException {

        try {
            return metaDatos.getColumnCount();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        return 0;
    }

    public String getColumnName(int columna) throws IllegalStateException {

        try {
            return metaDatos.getColumnName(columna + 1);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }

        return "";
    }

    public int getRowCount() throws IllegalStateException {
        return cantidadFilas;
    }

    public Object getValueAt(int fila, int columna) throws IllegalStateException {

        try {
            datos.absolute(fila + 1);
            return datos.getObject(columna + 1);
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        return "";
    }

    public void actualizar(String sql, String nombreConexion)throws SQLException, IllegalStateException {
        this.desconectar();
        
        try {
            //Se abre la conexion utilizando el driver de postgres
            Class.forName("org.postgresql.Driver");
            conexion = DriverManager.getConnection(nombreConexion);
            instruccion = conexion.createStatement(ResultSet.TYPE_SCROLL_INSENSITIVE,ResultSet.CONCUR_UPDATABLE);

            datos = instruccion.executeQuery(sql);
            metaDatos = datos.getMetaData();

            datos.last();
            cantidadFilas = datos.getRow();

            fireTableStructureChanged();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        
    }
    
    public void desconectar() {       
        try {
            datos.close();
            instruccion.close();
            conexion.close();
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
}
