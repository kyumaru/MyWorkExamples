package ventanasbd;


import java.awt.Dimension;
import java.awt.Graphics;
import javax.swing.ImageIcon;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author hp
 */



public class Imagen extends javax.swing.JPanel {
 
public Imagen() {
this.setSize(752, 256); //se selecciona el tamaño del panel
//this.setAlignmentX((float) 0.5);
//this.setAlignmentY((float) 0.5);
}
 
//Se crea un método cuyo parámetro debe ser un objeto Graphics
 
@Override
public void paint(Graphics grafico) {
Dimension height = getSize();
 
//Se selecciona la imagen que tenemos en el paquete de la //ruta del programa
 
ImageIcon Img = new ImageIcon(getClass().getResource("/Imagenes/Punto_Venta.png")); 
 
//se dibuja la imagen que tenemos en el paquete Images //dentro de un panel
 
grafico.drawImage(Img.getImage(), 0, 0, height.width, height.height, null);
 
setOpaque(false);
super.paintComponent(grafico);
}
}

