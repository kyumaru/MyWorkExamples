/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package ventanasbd;

import java.sql.SQLException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JOptionPane;

/**
 *
 * @author estebannoguerapenaranda
 */
public class ModificarPersonaFisica extends javax.swing.JFrame {

    /**
     * Creates new form ModificarPersonaFisica
     */
    
    private String cedula;
    private int numero;
    private db_conect A;
    
    public ModificarPersonaFisica() {
        initComponents();
    }

    
    public ModificarPersonaFisica(String n,int valor) {
    try{
        initComponents();
        cedula = n;
        A = new db_conect();
        A.conexion();
        numero = valor;
        if(numero == 1){
            titulo.setText("Modificar Cliente");
            sueldo_etiq.setVisible(false);
            sueldo.setVisible(false);
            Administrador.setVisible(false);
            Cajero.setVisible(false);
            Bodeguero.setVisible(false);
            llenar_datos();
        }
        else if(numero == 2){
            titulo.setText("Modificar Empleado");
            llenar_datos();
        }
    }
    catch (SQLException ex) {
            System.err.println( ex.getMessage() );
    }
        
    }
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel9 = new javax.swing.JLabel();
        jLabel15 = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        apellido2 = new javax.swing.JTextField();
        jLabel1 = new javax.swing.JLabel();
        nombre1 = new javax.swing.JTextField();
        jLabel2 = new javax.swing.JLabel();
        apellido1 = new javax.swing.JTextField();
        jLabel3 = new javax.swing.JLabel();
        tel_casa = new javax.swing.JTextField();
        jLabel4 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        jLabel6 = new javax.swing.JLabel();
        Tel_celular = new javax.swing.JTextField();
        email = new javax.swing.JTextField();
        jLabel7 = new javax.swing.JLabel();
        provincia = new javax.swing.JTextField();
        jLabel8 = new javax.swing.JLabel();
        canton = new javax.swing.JTextField();
        jLabel12 = new javax.swing.JLabel();
        jLabel11 = new javax.swing.JLabel();
        distrito = new javax.swing.JTextField();
        jLabel13 = new javax.swing.JLabel();
        senias = new javax.swing.JTextField();
        jLabel14 = new javax.swing.JLabel();
        titulo = new javax.swing.JLabel();
        Modificar = new javax.swing.JButton();
        Cancelar = new javax.swing.JButton();
        sueldo_etiq = new javax.swing.JLabel();
        sueldo = new javax.swing.JTextField();
        Administrador = new javax.swing.JRadioButton();
        Cajero = new javax.swing.JRadioButton();
        Bodeguero = new javax.swing.JRadioButton();
        eliminar = new javax.swing.JButton();
        sueldo_etiq1 = new javax.swing.JLabel();
        idSucursal = new javax.swing.JTextField();

        jLabel9.setFont(new java.awt.Font("Lucida Grande", 1, 14)); // NOI18N
        jLabel9.setText("Datos Personales");

        jLabel15.setText("Señas");

        jPanel1.setBackground(new java.awt.Color(204, 255, 153));

        jLabel1.setText("Nombre");

        jLabel2.setText("Apellido 1");

        jLabel3.setText("Apellido 2");

        jLabel4.setText("Telefonos");

        jLabel5.setText("Casa");

        jLabel6.setText("Celular");

        jLabel7.setText("Email");

        jLabel8.setText("Direccion");

        jLabel12.setText("Cantón");

        jLabel11.setText("Provincia");

        jLabel13.setText("Distrito");

        jLabel14.setText("Señas");

        titulo.setFont(new java.awt.Font("Lucida Grande", 1, 14)); // NOI18N
        titulo.setText("Datos Personales Cliente");

        Modificar.setText("Modificar");
        Modificar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                ModificarActionPerformed(evt);
            }
        });

        Cancelar.setText("Cancelar");
        Cancelar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                CancelarActionPerformed(evt);
            }
        });

        sueldo_etiq.setText("Sueldo  ₡");

        Administrador.setText("Administrador");
        Administrador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                AdministradorActionPerformed(evt);
            }
        });

        Cajero.setText("Cajero");
        Cajero.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                CajeroActionPerformed(evt);
            }
        });

        Bodeguero.setText("Bodeguero");
        Bodeguero.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                BodegueroActionPerformed(evt);
            }
        });

        eliminar.setText("Eliminar Persona");
        eliminar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                eliminarActionPerformed(evt);
            }
        });

        sueldo_etiq1.setText("id Sucursal");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(75, 75, 75)
                .addComponent(Modificar)
                .addGap(52, 52, 52)
                .addComponent(eliminar)
                .addGap(53, 53, 53)
                .addComponent(Cancelar)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, jPanel1Layout.createSequentialGroup()
                        .addGap(90, 90, 90)
                        .addComponent(jLabel1)
                        .addGap(18, 18, 18)
                        .addComponent(nombre1))
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, jPanel1Layout.createSequentialGroup()
                        .addGap(53, 53, 53)
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                .addGroup(jPanel1Layout.createSequentialGroup()
                                    .addGap(0, 0, Short.MAX_VALUE)
                                    .addComponent(tel_casa, javax.swing.GroupLayout.PREFERRED_SIZE, 205, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGroup(jPanel1Layout.createSequentialGroup()
                                    .addComponent(jLabel11)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(provincia, javax.swing.GroupLayout.PREFERRED_SIZE, 266, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGroup(jPanel1Layout.createSequentialGroup()
                                    .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                        .addComponent(jLabel4)
                                        .addComponent(jLabel8)
                                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                            .addComponent(jLabel14)
                                            .addComponent(jLabel13))
                                        .addComponent(jLabel7))
                                    .addGap(0, 0, Short.MAX_VALUE)))
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addGap(93, 93, 93)
                                .addComponent(canton, javax.swing.GroupLayout.PREFERRED_SIZE, 268, javax.swing.GroupLayout.PREFERRED_SIZE))))
                    .addGroup(javax.swing.GroupLayout.Alignment.LEADING, jPanel1Layout.createSequentialGroup()
                        .addContainerGap()
                        .addComponent(titulo)
                        .addGap(0, 0, Short.MAX_VALUE))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addComponent(Administrador)
                                .addGap(69, 69, 69)
                                .addComponent(Cajero)
                                .addGap(73, 73, 73)
                                .addComponent(Bodeguero))
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addComponent(jLabel12)
                                .addGap(327, 327, 327))
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel3)
                                    .addComponent(jLabel2, javax.swing.GroupLayout.Alignment.TRAILING))
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                        .addComponent(jLabel5)
                                        .addComponent(email, javax.swing.GroupLayout.DEFAULT_SIZE, 266, Short.MAX_VALUE)
                                        .addComponent(apellido2)
                                        .addComponent(senias)
                                        .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                                            .addComponent(jLabel6)
                                            .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                            .addComponent(Tel_celular, javax.swing.GroupLayout.PREFERRED_SIZE, 204, javax.swing.GroupLayout.PREFERRED_SIZE))
                                        .addComponent(distrito, javax.swing.GroupLayout.Alignment.TRAILING))
                                    .addComponent(apellido1, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 266, javax.swing.GroupLayout.PREFERRED_SIZE))))))
                .addGap(86, 86, 86))
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(128, 128, 128)
                        .addComponent(sueldo_etiq, javax.swing.GroupLayout.PREFERRED_SIZE, 46, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(sueldo_etiq1, javax.swing.GroupLayout.PREFERRED_SIZE, 77, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)))
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(idSucursal, javax.swing.GroupLayout.PREFERRED_SIZE, 135, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(sueldo, javax.swing.GroupLayout.PREFERRED_SIZE, 135, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(158, 158, 158))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(titulo)
                .addGap(15, 15, 15)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(nombre1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel1))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(apellido1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel2))
                .addGap(14, 14, 14)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(apellido2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel3))
                .addGap(18, 18, 18)
                .addComponent(jLabel4)
                .addGap(31, 31, 31)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(tel_casa, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel5))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel6)
                    .addComponent(Tel_celular, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(28, 28, 28)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(email, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel7))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jLabel8)
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(provincia, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel11))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(canton, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel12))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(distrito, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel13))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(senias, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel14))
                .addGap(34, 34, 34)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(Administrador)
                    .addComponent(Cajero)
                    .addComponent(Bodeguero))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(sueldo_etiq)
                    .addComponent(sueldo, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(idSucursal, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(sueldo_etiq1))
                .addGap(30, 30, 30)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(Modificar)
                    .addComponent(eliminar)
                    .addComponent(Cancelar))
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        pack();
        setLocationRelativeTo(null);
    }// </editor-fold>//GEN-END:initComponents

    private void BodegueroActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_BodegueroActionPerformed
        // TODO add your handling code here:

        this.Administrador.setSelected(false);

        this.Cajero.setSelected(false);
    }//GEN-LAST:event_BodegueroActionPerformed

    private void CajeroActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_CajeroActionPerformed
        // TODO add your handling code here:

        this.Administrador.setSelected(false);

        this.Bodeguero.setSelected(false);
    }//GEN-LAST:event_CajeroActionPerformed

    private void AdministradorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_AdministradorActionPerformed
        // TODO add your handling code here:

        this.Cajero.setSelected(false);

        this.Bodeguero.setSelected(false);
    }//GEN-LAST:event_AdministradorActionPerformed

    private void CancelarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_CancelarActionPerformed
        // TODO add your handling code here:
        this.setVisible(false);
    }//GEN-LAST:event_CancelarActionPerformed

    private void ModificarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_ModificarActionPerformed
        // TODO add your handling code here:
        String tEmp = "";
        
        if(Administrador.isSelected()){
            tEmp = "A";
        }
        if(Bodeguero.isSelected()){
            tEmp = "B";
        }
        if(Cajero.isSelected()){
            tEmp = "C";
        }
        
        try{
            if(numero == 1 ){
                A.ejecutarSql("select actualizarclienteF('"+cedula + "','" +nombre1.getText()+"','"+ apellido1.getText()+"','"+ apellido2.getText()+"','"+email.getText()+"')");
                A.ejecutarSql("select actualizarTelefonos('"+cedula+"','"+tel_casa.getText()+"','Casa')");
                A.ejecutarSql("select actualizarTelefonos('"+cedula+"','"+Tel_celular.getText()+"','Celular')");
                A.ejecutarSql("select actualizardirecciones('"+cedula +"','"+ provincia.getText()+"','"+canton.getText()+"','"+distrito.getText()+"','" +  senias.getText()+"')" );
                JOptionPane.showMessageDialog(null, "Cliente Modificado");
                this.setVisible(false);
            }
            
            if(numero == 2 ){
                A.ejecutarSql("select actualizarempleado('"+cedula + "','" +nombre1.getText()+"','"+ apellido1.getText()+"','"+ apellido2.getText()+"','"+email.getText()+"','07-02-1992','"+ sueldo.getText()   +"','"+  tEmp +"','0')");
                A.ejecutarSql("update empleado set idsucursal = '"+ idSucursal.getText() +"' where cedula = '"+ cedula + "'");
                A.ejecutarSql("select actualizarTelefonos('"+cedula+"','"+tel_casa.getText()+"','Casa')");
                A.ejecutarSql("select actualizarTelefonos('"+cedula+"','"+Tel_celular.getText()+"','Celular')");
                A.ejecutarSql("select actualizardirecciones('"+cedula +"','"+ provincia.getText()+"','"+canton.getText()+"','"+distrito.getText()+"','" +  senias.getText()+"')" );
            }
            JOptionPane.showMessageDialog(null, "Empleado Modificado");
            this.setVisible(false);
        }
        catch (SQLException ex) {
            System.err.println( ex.getMessage() );
        }
    }//GEN-LAST:event_ModificarActionPerformed

    private void eliminarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_eliminarActionPerformed
        // TODO add your handling code here:
        int resp=JOptionPane.showConfirmDialog(null,"¿Está seguro de eliminar esta Persona?");
        if (JOptionPane.OK_OPTION == resp){
            try {
                //JOptionPane.showMessageDialog(null,"Selecciona opción Afirmativa");
                A.ejecutarSql("select elimina_x_cedula('"+ cedula + "')");
                JOptionPane.showMessageDialog(null, "Persona Eliminada");
            } catch (SQLException ex) {
                Logger.getLogger(ModificarPersonaFisica.class.getName()).log(Level.SEVERE, null, ex);
            }
            this.setVisible(false);
        }
        else{
            //JOptionPane.showMessageDialog(null,"No selecciona una opción afirmativa");
        }
    }//GEN-LAST:event_eliminarActionPerformed

    private void llenar_datos(){
    try{
        String tipo_empl= "";
        if(numero == 1){
            nombre1.setText(A.ejecutarRetornando("select nombre1 from personafisica where cedula ='" + cedula+"'"));
            apellido1.setText(A.ejecutarRetornando("select apellido1 from personafisica where cedula ='" + cedula+"'"));
            apellido2.setText(A.ejecutarRetornando("select apellido2 from personafisica where cedula ='" + cedula+"'"));
            tel_casa.setText(A.ejecutarRetornando("select NUMEROTEL  from telefonos where cedula = '"+cedula+"' "+ "and tipo = 'CASA'"));
            Tel_celular.setText(A.ejecutarRetornando("select NUMEROTEL  from telefonos where cedula = '"+cedula+"' "+ "and tipo = 'CELULAR'"));
            email.setText(A.ejecutarRetornando("select email from personafisica where cedula ='" + cedula+"'"));
            provincia.setText(A.ejecutarRetornando("select provincia from direcciones where cedula = '"+ cedula +"'"));
            canton.setText(A.ejecutarRetornando("select canton from direcciones where cedula = '"+ cedula +"'"));
            distrito.setText(A.ejecutarRetornando("select distrito from direcciones where cedula = '"+ cedula +"'"));
            senias.setText(A.ejecutarRetornando("select senias from direcciones where cedula = '"+ cedula +"'"));
        }
        else if(numero == 2){
            nombre1.setText(A.ejecutarRetornando("select nombre1 from personafisica where cedula ='" + cedula+"'"));
            apellido1.setText(A.ejecutarRetornando("select apellido1 from personafisica where cedula ='" + cedula+"'"));
            apellido2.setText(A.ejecutarRetornando("select apellido2 from personafisica where cedula ='" + cedula+"'"));
            tel_casa.setText(A.ejecutarRetornando("select NUMEROTEL  from telefonos where cedula = '"+cedula+"' "+ "and tipo = 'CASA'"));
            Tel_celular.setText(A.ejecutarRetornando("select NUMEROTEL  from telefonos where cedula = '"+cedula+"' "+ "and tipo = 'CELULAR'"));
            email.setText(A.ejecutarRetornando("select email from personafisica where cedula ='" + cedula+"'"));
            provincia.setText(A.ejecutarRetornando("select provincia from direcciones where cedula = '"+ cedula +"'"));
            canton.setText(A.ejecutarRetornando("select canton from direcciones where cedula = '"+ cedula +"'"));
            distrito.setText(A.ejecutarRetornando("select distrito from direcciones where cedula = '"+ cedula +"'"));
            senias.setText(A.ejecutarRetornando("select senias from direcciones where cedula = '"+ cedula +"'"));
            tipo_empl = A.ejecutarRetornando("select tipoempleado from empleado where cedula = '"+ cedula +"'"); 
            if(tipo_empl.equalsIgnoreCase("A")){
               this.Administrador.setSelected(true);
            }
            else if(tipo_empl.equalsIgnoreCase("B")){
                this.Bodeguero.setSelected(true);
            }
            else if(tipo_empl.equalsIgnoreCase("C")){
                this.Cajero.setSelected(true);
            }
            sueldo.setText(A.ejecutarRetornando("select sueldo from empleado where cedula = '"+ cedula +"'"));
            idSucursal.setText(A.ejecutarRetornando("select idsucursal from empleado where cedula = '"+ cedula +"'"));
        }
    }
    catch (SQLException ex) {
            System.err.println( ex.getMessage() );
    }
    
    }
    
    
    
    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(ModificarPersonaFisica.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(ModificarPersonaFisica.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(ModificarPersonaFisica.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(ModificarPersonaFisica.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new ModificarPersonaFisica().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JRadioButton Administrador;
    private javax.swing.JRadioButton Bodeguero;
    private javax.swing.JRadioButton Cajero;
    private javax.swing.JButton Cancelar;
    private javax.swing.JButton Modificar;
    private javax.swing.JTextField Tel_celular;
    private javax.swing.JTextField apellido1;
    private javax.swing.JTextField apellido2;
    private javax.swing.JTextField canton;
    private javax.swing.JTextField distrito;
    private javax.swing.JButton eliminar;
    private javax.swing.JTextField email;
    private javax.swing.JTextField idSucursal;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel13;
    private javax.swing.JLabel jLabel14;
    private javax.swing.JLabel jLabel15;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JTextField nombre1;
    private javax.swing.JTextField provincia;
    private javax.swing.JTextField senias;
    private javax.swing.JTextField sueldo;
    private javax.swing.JLabel sueldo_etiq;
    private javax.swing.JLabel sueldo_etiq1;
    private javax.swing.JTextField tel_casa;
    private javax.swing.JLabel titulo;
    // End of variables declaration//GEN-END:variables
}
