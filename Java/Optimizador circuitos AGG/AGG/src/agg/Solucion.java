/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package agg;

import java.util.*;

/**
 *
 * @author kyu
 */
//abierta de piernas como public
public class Solucion implements Comparable<Solucion>{//implements, herencia en java
    float []Rsx;//la vetor resistencias solucion
    float[][] Gsx;//la matriz de conductancias para este circuito
    ArrayList<Float> Vsx = new ArrayList<>();//la matriz de voltajes para este circuito
    float aptitud;
    String mode="max";
    float desired=10f;//desired voltage
 
    Solucion(){}//explicit default construtor
    
    public void set_aptitud(float aptitud){this.aptitud=aptitud;}
    public float get_aptitud(){ return this.aptitud;}
    public void set_genes(float[]Rsx){this.Rsx=Rsx;}
    public float[]get_genes(){ return this.Rsx;}
    
    @Override //override de metodo heredado
    public int compareTo(Solucion anotherInstance) {//compare using only MSB numbers are too large and compareTo shouuld return and int    
        /*TODO comparable criteria*/
        //return (int)(-this.get_aptitud()+anotherInstance.get_aptitud());//generates an descending order
        int res=0;
        if(this.mode.equals("max"))
            res=(int)(-this.get_aptitud()+anotherInstance.get_aptitud()); //desc
     
        if(this.mode.equals("min"))
            res=(int)(this.get_aptitud()-anotherInstance.get_aptitud());//asc
        
        if(this.mode.equals("nearest"))
            res=(int)( Math.abs(this.get_aptitud()-desired) - Math.abs(anotherInstance.get_aptitud()-desired) );//asc
        
        return res;
    }
    
}
