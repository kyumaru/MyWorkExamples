/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ventanasbd;

import com.google.gdata.client.*;
import com.google.gdata.client.calendar.*;
import com.google.gdata.data.*;
import com.google.gdata.data.acl.*;
import com.google.gdata.data.calendar.*;
import com.google.gdata.data.extensions.*;
import com.google.gdata.util.*;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JOptionPane;
/**
 *
 * @author estebannoguerapenaranda
 */
public class EventosGoogle {
    
            final CalendarService myService;
            
            String cuenta;
            
            String pass;

            public EventosGoogle(){

                myService = new CalendarService("evento calendario");
    
                
                

            }//fin de constructor

public int logger(String cuenta, String pass){
    // retorna un valor entero notificando que sucedio:
    //0--> lo logro
    //1--> hubo un problema con el internet

    int resultado = 0;
    
        try {
            
            this.cuenta = cuenta;
            
            this.pass = pass;
            
            myService.setUserCredentials(cuenta, pass);
        
            
        } catch (AuthenticationException ex) {
            
            //JOptionPane.showMessageDialog(null, "la contrasenia o la cuenta son incorrectas intentelo de nuevo");
            //Logger.getLogger(EventosGoogle.class.getName()).log(Level.SEVERE, null, ex);
           resultado = 1;
        }
        
        return resultado;
    
}//fin de logger


public void agregarEvento(String titulo, String detalle, Date horaInicio, Date horaFinal){
                        
    URL postUrl = null;
                
    try {
     
        postUrl = new URL("https://www.google.com/calendar/feeds/"+cuenta+"/private/full");         
    
    } catch (MalformedURLException ex) {
   
        JOptionPane.showMessageDialog(null,  ex.getMessage());
              
    }

    CalendarEventEntry myEntry = new CalendarEventEntry();

    myEntry.setTitle(new PlainTextConstruct(titulo));

    myEntry.setContent(new PlainTextConstruct(detalle));

    String horaI = "2014-04-17T30:00:00Z";//7->1 am

    String horaF = "2014-04-17T30:15:00Z";
    
       
    DateTime startTime = DateTime.parseDateTime(horaI);

    DateTime endTime = DateTime.parseDateTime(horaF);

    
    When eventTimes = new When();

    eventTimes.setStartTime(startTime);

    eventTimes.setEndTime(endTime);

    myEntry.addTime(eventTimes);

                
    try {
                    
        // Send the request and receive the response:
                    
        CalendarEventEntry insertedEntry = myService.insert(postUrl, myEntry);
                
    } catch (IOException ex) {
                    
        JOptionPane.showMessageDialog(null,  ex.getMessage());
        //Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
                
    } catch (ServiceException ex) {
                    
        JOptionPane.showMessageDialog(null,  ex.getMessage());
        //Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
                
    }
    
}//fin de agregarEvento



//        try {
//            myService.setUserCredentials("noguera.esteban@gmail.com", "1770estebanjosue");
//            
//        } catch (AuthenticationException ex) {
//            Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
//        }

//        /* Create and display the form */
        //java.awt.EventQueue.invokeLater(new Runnable() {
        //    public void run() {
//
                    
//                    //DocsService s = new DocsService("Doc List Demo");
//                // Create the calendar
//
//                    URL postUrl = null;
//                try {
//                    postUrl = new URL("https://www.google.com/calendar/feeds/noguera.esteban@gmail.com/private/full");
//                } catch (MalformedURLException ex) {
//                    Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
//                }
//CalendarEventEntry myEntry = new CalendarEventEntry();
//
//myEntry.setTitle(new PlainTextConstruct("llamar a rafa"));
//myEntry.setContent(new PlainTextConstruct("recordarle la tarea de Bases I"));
//
//
//DateTime startTime = DateTime.parseDateTime("2014-04-17T15:00:00-08:00");
//DateTime endTime = DateTime.parseDateTime("2014-04-17T17:00:00-08:00");
//When eventTimes = new When();
//eventTimes.setStartTime(startTime);
//eventTimes.setEndTime(endTime);
//myEntry.addTime(eventTimes);

//                try {
//                    // Send the request and receive the response:
//                    CalendarEventEntry insertedEntry = myService.insert(postUrl, myEntry);
//                } catch (IOException ex) {
//                    Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
//                } catch (ServiceException ex) {
//                    Logger.getLogger(VentanaJF.class.getName()).log(Level.SEVERE, null, ex);
//                }
}
