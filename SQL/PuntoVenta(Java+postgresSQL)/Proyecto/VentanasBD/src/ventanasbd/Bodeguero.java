/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ventanasbd;

import javax.swing.JOptionPane;

/**
 *
 * @author hp
 */
public class Bodeguero extends javax.swing.JFrame {

    /**
     * Creates new form Bodeguero
     */
    public Bodeguero() {
        initComponents();
    }
    
    public Bodeguero(String p){
        
        initComponents();
        
        this.jLabelNomUsuario.setText(p);
        
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jMenuBar1 = new javax.swing.JMenuBar();
        jMenu1 = new javax.swing.JMenu();
        jMenu2 = new javax.swing.JMenu();
        jMenu5 = new javax.swing.JMenu();
        jMenuItem2 = new javax.swing.JMenuItem();
        jPanel1 = new javax.swing.JPanel();
        label1 = new java.awt.Label();
        jLabelNomUsuario = new javax.swing.JLabel();
        jMenuBar2 = new javax.swing.JMenuBar();
        jMenu3 = new javax.swing.JMenu();
        jMenuItemCerrarSesion = new javax.swing.JMenuItem();
        jMenuItemGuardar = new javax.swing.JMenuItem();
        jMenu4VerInventario = new javax.swing.JMenu();
        jMenuItemVerProductos = new javax.swing.JMenuItem();
        jMenuItemAgregarProducto = new javax.swing.JMenuItem();
        jMenuItemDarBaja = new javax.swing.JMenuItem();

        jMenu1.setText("File");
        jMenuBar1.add(jMenu1);

        jMenu2.setText("Edit");
        jMenuBar1.add(jMenu2);

        jMenu5.setText("jMenu5");

        jMenuItem2.setText("jMenuItem2");

        setDefaultCloseOperation(javax.swing.WindowConstants.DO_NOTHING_ON_CLOSE);
        setTitle("Modulo Bodeguero");
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowClosing(java.awt.event.WindowEvent evt) {
                formWindowClosing(evt);
            }
        });

        jPanel1.setBackground(new java.awt.Color(204, 255, 153));

        label1.setText("Bienvenido ");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(label1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(1, 1, 1)
                .addComponent(jLabelNomUsuario, javax.swing.GroupLayout.PREFERRED_SIZE, 84, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(237, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel1Layout.createSequentialGroup()
                .addContainerGap(248, Short.MAX_VALUE)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(label1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jLabelNomUsuario, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );

        jMenu3.setText("Archivo");
        jMenu3.setFont(new java.awt.Font("Lucida Grande", 1, 14)); // NOI18N

        jMenuItemCerrarSesion.setAccelerator(javax.swing.KeyStroke.getKeyStroke(java.awt.event.KeyEvent.VK_F4, java.awt.event.InputEvent.ALT_MASK));
        jMenuItemCerrarSesion.setFont(new java.awt.Font("Lucida Grande", 2, 14)); // NOI18N
        jMenuItemCerrarSesion.setText("Cerrar Sesión");
        jMenuItemCerrarSesion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItemCerrarSesionActionPerformed(evt);
            }
        });
        jMenu3.add(jMenuItemCerrarSesion);

        jMenuItemGuardar.setFont(new java.awt.Font("Lucida Grande", 2, 14)); // NOI18N
        jMenuItemGuardar.setText("Guardar");
        jMenuItemGuardar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItemGuardarActionPerformed(evt);
            }
        });
        jMenu3.add(jMenuItemGuardar);

        jMenuBar2.add(jMenu3);

        jMenu4VerInventario.setText("Inventario");
        jMenu4VerInventario.setFont(new java.awt.Font("Lucida Grande", 1, 14)); // NOI18N

        jMenuItemVerProductos.setFont(new java.awt.Font("Lucida Grande", 2, 14)); // NOI18N
        jMenuItemVerProductos.setText("Ver Productos en Inventario");
        jMenuItemVerProductos.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItemVerProductosActionPerformed(evt);
            }
        });
        jMenu4VerInventario.add(jMenuItemVerProductos);

        jMenuItemAgregarProducto.setFont(new java.awt.Font("Lucida Grande", 2, 14)); // NOI18N
        jMenuItemAgregarProducto.setText("Agregar Producto");
        jMenuItemAgregarProducto.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItemAgregarProductoActionPerformed(evt);
            }
        });
        jMenu4VerInventario.add(jMenuItemAgregarProducto);

        jMenuItemDarBaja.setFont(new java.awt.Font("Lucida Grande", 2, 14)); // NOI18N
        jMenuItemDarBaja.setText("Dar de Baja/Actualizar Info_Producto");
        jMenuItemDarBaja.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItemDarBajaActionPerformed(evt);
            }
        });
        jMenu4VerInventario.add(jMenuItemDarBaja);

        jMenuBar2.add(jMenu4VerInventario);

        setJMenuBar(jMenuBar2);

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

    private void CerrarCesionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_CerrarCesionActionPerformed
        // TODO add your handling code here:
         // TODO add your handling code here:
        
        new VentanaJF().setVisible(true);
        
        this.setVisible(false);
    }//GEN-LAST:event_CerrarCesionActionPerformed

    private void Ver_Produc_InveActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_Ver_Produc_InveActionPerformed
        // TODO add your handling code here:
        new Inventario(1).setVisible(true);
        
    }//GEN-LAST:event_Ver_Produc_InveActionPerformed

    private void Agregar_ProductoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_Agregar_ProductoActionPerformed
        // TODO add your handling code here:
        new Inventario().setVisible(true);
    }//GEN-LAST:event_Agregar_ProductoActionPerformed

    
    private void Dar_De_Baja_EditarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_Dar_De_Baja_EditarActionPerformed
        // TODO add your handling code here:
        new Inventario().setVisible(true);
    }//GEN-LAST:event_Dar_De_Baja_EditarActionPerformed

    private void jMenuItemCerrarSesionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItemCerrarSesionActionPerformed
        // TODO add your handling code here:
        
         new VentanaJF().setVisible(true);
         
         this.setVisible(false);
        
         
    }//GEN-LAST:event_jMenuItemCerrarSesionActionPerformed

    private void textField1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_textField1ActionPerformed
        // TODO add your handling code here:
        
    }//GEN-LAST:event_textField1ActionPerformed

    private void jMenuItemAgregarProductoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItemAgregarProductoActionPerformed
        // TODO add your handling code here:
        new AgregarProducto().setVisible(true);
    }//GEN-LAST:event_jMenuItemAgregarProductoActionPerformed

    private void jMenuItemVerProductosActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItemVerProductosActionPerformed
        // TODO add your handling code here:
        
        new Inventario().setVisible(true);
     
    }//GEN-LAST:event_jMenuItemVerProductosActionPerformed

    private void jMenuItemDarBajaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItemDarBajaActionPerformed
        // TODO add your handling code here:
        new Inventario().setVisible(true);
    }//GEN-LAST:event_jMenuItemDarBajaActionPerformed

    private void jMenuItemGuardarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItemGuardarActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_jMenuItemGuardarActionPerformed

    private void formWindowClosing(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowClosing
        // TODO add your handling code here:
        
        int resp=JOptionPane.showConfirmDialog(null,"¿Está seguro de abandonar esta ventana?");
        if (JOptionPane.OK_OPTION == resp){
            //JOptionPane.showMessageDialog(null,"Selecciona opción Afirmativa");
            new VentanaJF().setVisible(true);
            this.setVisible(false);
        }
        else{
            //JOptionPane.showMessageDialog(null,"No selecciona una opción afirmativa");
        }
        
        
    }//GEN-LAST:event_formWindowClosing

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
            java.util.logging.Logger.getLogger(Bodeguero.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(Bodeguero.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(Bodeguero.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(Bodeguero.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new Bodeguero().setVisible(true);
            }
        });
    }
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabelNomUsuario;
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenu jMenu2;
    private javax.swing.JMenu jMenu3;
    private javax.swing.JMenu jMenu4VerInventario;
    private javax.swing.JMenu jMenu5;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JMenuBar jMenuBar2;
    private javax.swing.JMenuItem jMenuItem2;
    private javax.swing.JMenuItem jMenuItemAgregarProducto;
    private javax.swing.JMenuItem jMenuItemCerrarSesion;
    private javax.swing.JMenuItem jMenuItemDarBaja;
    private javax.swing.JMenuItem jMenuItemGuardar;
    private javax.swing.JMenuItem jMenuItemVerProductos;
    private javax.swing.JPanel jPanel1;
    private java.awt.Label label1;
    // End of variables declaration//GEN-END:variables
}
