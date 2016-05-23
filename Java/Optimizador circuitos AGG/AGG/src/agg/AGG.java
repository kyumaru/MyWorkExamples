/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
//ABOUT static methods http://stackoverflow.com/questions/2671496/java-when-to-use-static-method

package agg;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.List;
import java.util.Random; 
import java.util.*; //stuff for arrraylists
import static java.lang.Math.*;
/**
 *
 * @author kyu
 */
public class AGG {
 
  /*** MODO DE OPERACION ****/
  /*el software trabaja en 3 modos
    "min" encuentra las resistencias para obtner la suma de minimos voltajes entre los nodos
    "max" encuentra las resistencias para obtner la suma de maximos voltajes entre los nodos
    "nearest" encuentra las resistencias para obtner la suma voltajes mas cercana al valor desired
  */
  String mode="nearest";
  float desired=19f;//desired voltage for nearest mode only
  
  /*** PARAMETROS DEL AG ****/
  //globales
  int probaCruze=95;
  int probaMutar=15;
  int cuantos=11;//cuantos bichillos solucion
  ArrayList<Solucion> poblacion=new ArrayList<>();//contenedor de soluciones
  Solucion mejor=new Solucion();//la mejor solucion encontrada  
  

  /*** DESCRIPCION DEL CIRCUITO RESISTIVO ****/
//if static is removed from Rs definition static make_ADN wont work, also is this is used to refer to it 
  static float[] Rs={0.1f,1.0f,1.2f,1.5f,1.8f,3.9f,4.7f,5.1f,5.6f,6.8f,8.2f,11.2f};//posibles resistencias totales en matriz de Gx, ningun valor es suma de otros 2 en la lista
  static float[] Is={1f};//vector de Imalla total proviene de suma de fuentes de corriente presentes
  //describe the nodes and resistances attached to every node
  boolean[][] connections = new boolean[][]{//legth is 2
  /*
  {true,false,true},////which resistances attached to node1
  {false,true,true} ///which resistances attached to this node2....etc
  */
      {false,true,false,true},////which resistances attached to node1
      {false,false,true,true},///which resistances attached to this node2....etc
      {true,true,true,false},
   };
  int N=connections.length;//cantidad de nodos


 void filloffDiagElement(float[][] tuplaGx,float[] Rx){//k is the 
     ArrayList<boolean[]> filas=new ArrayList<>();//contenedor de vectores bool
     for(int i=0;i<connections.length;i++){//recorrer esta coneccion
         filas.add(this.connections[i]);
     }
     
     for(int i=0;i<filas.get(0).length;i++){//para coda comluna
         boolean[] vertical=new boolean[filas.size()];
         for(int j=0;j<filas.size();j++){//para cada vector en la lista
             vertical[j]=filas.get(j)[i];
         }
          int indexr=-1;
          int indexl=-1;
          for(int k=0;k<vertical.length;k++){
              if(vertical[k]){
                  if(indexr==-1)
                      indexr=k;
                  else
                      indexl=k;
              }
          }
          if(indexr!=-1&&indexl!=-1){
              tuplaGx[indexr][indexl]=(float)Math.pow(-Rx[i],-1);
              tuplaGx[indexl][indexr]=(float)Math.pow(-Rx[i],-1);
          }    
     }
  }  
  
  void fillDiagElement(int k,float[][] tuplaGx,float[] Rx){//k is the 
     float sumaGx=0f;
     for(int i=0;i<connections[k%N].length;i++){//recorrer esta coneccion 
         if(connections[k%N][i])
             sumaGx+=Math.pow(Rx[i],-1);
     }
     tuplaGx[k%N][k%N]=sumaGx;
  }
 
  void fillGxRx(Solucion s,float[] Rx){// fills both tuple diagonals  
     
      //fill Rx
      s.Rsx=Rx;//chosen resistances for this solution
      //fill Gx
      for(int i=0;i<N*N;i+=N+1){
          fillDiagElement(i,s.Gsx,Rx);
      } 
      filloffDiagElement(s.Gsx,Rx);     
  }
  
  void CrearGenoma(Solucion s,float[] Rx){// llena las matrices Gx y Rx para esta solucion segun Rx   
     float[][] tuplaGx=new float[N][N];//container for this result
     s.Gsx=new float[N][N];//fillGxRx() needs an initialized [][]matrix
     fillGxRx(s,Rx);
     s.mode=this.mode;
     s.desired=this.desired;
     //apt calc and Vx fill done by calc_apt
  }
  
 //returns a rand resitance Rx
  static Float Make_ADN(){        
      Random rand = new Random();
      rand.setSeed(rand.nextInt(1000000));                
      //return (Float)Math.pow(Rs[rand.nextInt(Rs.length)],-1) ;
      return Rs[rand.nextInt(Rs.length)];
  } 
 
  //crea una nueva poblacion inicia la poblacion
  void CrearPoblacion(){    
    for(int j=0;j<this.cuantos;j++){   
      float[] Rx=new float[connections[0].length];//container for chosen resistances for this solution
      for(int i=0;i<Rx.length;i++){//fill Rx with chosen resistances 
          Rx[i]=Make_ADN();
      } 
      Solucion s= new Solucion();//
      this.CrearGenoma(s,Rx);
      //tupla.set_aptitud(calc_apt(tupla)); done by another method
      poblacion.add(s);
    }    
  }
  
  //makes clones of [][]matrix
  public float[][] deepCopyMatrix(float[][] input) {
    if (input == null)
        return null;
    float[][] result = new float[input.length][];
    for (int r = 0; r < input.length; r++) {
        result[r] = input[r].clone();
    }
    return result;
}
 //swaps colum k of the matrix with Is 
    float[][] swapColumn( float[][] Gx, int k){//swaps colum k of the matrix with Is
      float[][] resGx= deepCopyMatrix(Gx);//lets create a new copy so the parameter received Gx is not changed
      float[] Isx=new float[this.N];//full DC vector, one element /node 
      
      for(int i=0;i<N;i++){ //padding Isx with 0s if necessary
          if(i<Is.length)
              Isx[i]=Is[i];
          else
              Isx[i]=0;
      }
      for(int i=0;i<N;i++){//hay q hacer lenth substituciones
         resGx[i][k]=Isx[i];
      }
      return resGx;
  }
 
//gets determinant of a matrix[][]
    float calc_det(float A[][],int N){
        float det=0;
        if(N == 1){
            det = A[0][0];
        }
        else if (N == 2){
            det = A[0][0]*A[1][1] - A[1][0]*A[0][1];
        }
        else{
            det=0;
            for(int j1=0;j1<N;j1++)
            {
                float[][] m = new float[N-1][];
                for(int k=0;k<(N-1);k++){
                    m[k] = new float[N-1];
                }
                for(int i=1;i<N;i++){
                    int j2=0;
                    for(int j=0;j<N;j++){
                        if(j == j1)
                            continue;
                        m[i-1][j2] = A[i][j];
                        j2++;
                    }
                }
                det += Math.pow(-1.0,1.0+j1+1.0)* A[0][j1] * calc_det(m,N-1);
            }
        }
        return det;
    }
   
  //todo el calculo del vector de voltajes se hace aqui
  float calc_apt(Solucion s){
      float[][] Mup = new float[N][N];
      float[][] Mdown = new float[N][N];
      Mdown=s.Gsx;
      float detdown=this.calc_det(Mdown,N);
      float apt=0;
      for(int i=0;i<N;i++){//for every vx to calculate
          Mup=this.swapColumn(s.Gsx, i);
          float vx=this.calc_det(Mup,N)/detdown;
          s.Vsx.add(vx);//save voltages for this solution
          apt+=vx;
      }
      return Math.abs(apt);//discard voltage dirreccion 
  }
  
  void Seleccionar(){
    Collections.sort(poblacion);
  }
 
  void evaluarAptitudes(){//evalua la aptitud de todos en la poblacion
    for(int i=0;i<poblacion.size();i++)   
      poblacion.get(i).set_aptitud(calc_apt(poblacion.get(i)));
  }

//cruza dos soluciones
  //make 1 baby using crossover random copy tech, 
  Solucion makeBaby(Solucion dad, Solucion mom){
      Random rand = new Random();
      rand.setSeed(rand.nextInt(1000000)); 
      Solucion baby=new Solucion();
      ArrayList<Float> babyGenes =new ArrayList<>();
      //lets copy mom and dad genes
      ArrayList<Float> dadGenes =new ArrayList<>();
      ArrayList<Float> momGenes =new ArrayList<>();
      for(int i=0;i<dad.Rsx.length;i++){
          dadGenes.add(dad.Rsx[i]);
          momGenes.add(mom.Rsx[i]);
      }
      //lets copy half dad genes into baby choosen  lucky
      int half=1+dadGenes.size()/2;//random copy half from dad +1
      while(!dadGenes.isEmpty()&&babyGenes.size()!=half){
         int lucky=rand.nextInt(dadGenes.size());
         babyGenes.add(dadGenes.get(lucky));
         dadGenes.remove(lucky);
      }
     //lets copy remaining genes from mom into baby choosen lucky
      while(!momGenes.isEmpty()&&babyGenes.size()!=mom.Rsx.length){
         int lucky=rand.nextInt(momGenes.size());
         babyGenes.add(momGenes.get(lucky));
         momGenes.remove(lucky);
      }
      //copy babyGenes into baby
      float[]genes=new float[babyGenes.size()];
      for(int i=0;i<babyGenes.size();i++){
          genes[i]=babyGenes.get(i);
      }
      baby.Rsx=genes;
      //lets asing its Gx
      this.CrearGenoma(baby, baby.Rsx);

      return baby;
  }

   //cruzar genera 2 soluciones hijo tomando crossover
 //adds both 2 babies to nextGen
 void Cruzar( ArrayList<Solucion> nextGen,ArrayList<Solucion> parents){
      Solucion daddy=parents.get(0);
      Solucion mommy=parents.get(1); 	
      nextGen.add(makeBaby(daddy,mommy));
      nextGen.add(makeBaby(mommy,daddy));
  }
 
 
 
 //genera la nueva poblacion con padres mas aptos+ sus hijos+ relleno, la nueva poblacion debe ser del mismo tamano
ArrayList<Solucion> reproduccion(){
    ArrayList<Solucion> nextGen= new ArrayList<Solucion>((poblacion.subList(0, poblacion.size()/2-1)));//inserta a los mejores de una	 
    ArrayList<Solucion> pool= new ArrayList<Solucion>(nextGen);// los posibles padres actuales 
    ArrayList<Solucion> parents= new ArrayList<Solucion>();//contine padre y madre actuales	
    Random rand = new Random();
    rand.setSeed(rand.nextInt(1000000)); 
  
  int cont=0;
  while(!pool.isEmpty()&&cont<pool.size()) 
  {
        if(this.probaCruze>=rand.nextInt(100)){
            int lucky=rand.nextInt(pool.size());
            Solucion daddy=pool.get(lucky);
            pool.remove(lucky);//chages pool size
            lucky=rand.nextInt(pool.size());
            Solucion mommy=pool.get(lucky);
            pool.remove(lucky);//chages pool size
            parents.add(mommy);
            parents.add(daddy);
            Cruzar(nextGen,parents);
        }
        ++cont;
  }
  cont=nextGen.size();
  while(nextGen.size()!=poblacion.size()){//falta llenar loq sobra
     nextGen.add(poblacion.get(cont));
     ++cont;
  }
  return nextGen;
}
  

  
  
//salvaMejor e impreme tal solucion nueva
void salvarMejor(Solucion s){
    if(mejor.aptitud<s.aptitud){
        mejor=new Solucion();
        mejor.Gsx=this.deepCopyMatrix(this.poblacion.get(0).Gsx);
        mejor.Rsx= (float[])s.Rsx.clone();
        mejor.aptitud=s.get_aptitud();
        mejor.Vsx=new ArrayList<>(s.Vsx);
        System.out.println("\nI have optimized the circuit!!");  
        System.out.println("\nTotal voltage: "+mejor.aptitud/*+"\n"*/);       
        for(int i=0;i<mejor.Vsx.size();i++){
           System.out.print("V"+i+" = "+ mejor.Vsx.get(i)+"\t");
        }
        System.out.println("\n");
        
        for(int i=0;i<this.connections[0].length;i++){
           System.out.print("R"+i+" = "+ mejor.Rsx[i]+"\t");
        }
         System.out.println("\n\n");
         //print conductance matrix
         System.out.println("Conductance Matrix:\n");
         for(int i=0; i<3; i++){
           for(int j=0; j<3; j++){
                System.out.print(String.format("%20s", this.mejor.Gsx[i][j]));
            }
           System.out.println("");
        }
    }
}

//hace una mutacion de genes al azar
  void Mutar(float[] Rx){
      Random rand = new Random();
      rand.setSeed(rand.nextInt(1000000)); 
      for(int i=0;i<rand.nextInt(1);i++)//make 1 swap  
      {           
          int x0 = rand.nextInt(Rx.length);  
          int x1 = rand.nextInt(Rx.length);
          float temp = Rx[x0];
          Rx[x0] = Rx[x1];
          Rx[x1] = temp;        
      }      
  }
  
  void Mutar(){//sobrecarga muta toda la poblacion con cierta proba
    Random rand = new Random();
    rand.setSeed(rand.nextInt(1000000)); 
    for(int i=0;i<poblacion.size();i++){//para toda la poblacion exepto el mejor
        if(this.probaMutar>=rand.nextInt(100)){
            this.Mutar(poblacion.get(i).Rsx);
        }
    }
  }

    //evolves the whole system
//debe haber una poblacion inicial creada
void evolucionar(int N){
   this.CrearPoblacion();
   while(N>0/*&&mejor.aptitud>99999*/){
      evaluarAptitudes();
      Seleccionar();
      salvarMejor(poblacion.get(0));
      poblacion=new ArrayList<Solucion>(this.reproduccion()); 
      this.Mutar();
      --N;
   }
}

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
      AGG myAG=new AGG();
      myAG.evolucionar(0xfffff);
      System.out.println("");         
    }  
}