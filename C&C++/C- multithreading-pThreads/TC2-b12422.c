/**TC2 B12422 Juan Carlos E Soto**
//for MPI check
/** http://mpitutorial.com/tutorials/mpi-reduce-and-allreduce/ **/
//for Pthreads check
/** http://timmurphy.org/2010/05/04/pthreads-in-c-a-minimal-working-example/ **/

/**Remember that with MPI all code between mpi_init and finalize is executed concurrently
on every process, only one process should be root at a given execution point, memory is not
shared amongs processes meaning a variable in p0 is another in p1 even though they are
referred to by the same name**/
/** The MPI foundation is the pair send-recv funtions, they are to be used as a couple in 
different process ranks, meaning if you use one it should be done in one process rank and the 
other on another process rank, this should look like if(someid)-send-else-rcv,
because this funtions are blocking once one is executed it stops execution until it
gets acknowledge from its complement running concurrently in another process rank(there are
some variations of these funtions which allow to continue execution look MPI_Isend)
i.e a sequence in the SAME process rank
send
rcv 
execution gets stuck at the first send, waiting forever, in general the send-rcv pair needs
to be implemented in the form if(someid)-send-else-rcv so that one process sends and other
rcv. Almost any MPI funtion is actually a smart implementation of if(someid)-send-else-rcv 
**/


#include <stdio.h>
#include "mpi.h"
#include <stdlib.h>
#include <math.h>
#include <pthread.h> 
#define MAX 101

/**note que las variables globales son variables(memoria) de proceso, por lo que
son visibles a los hilos q se crean, hilos comparten memoria(con el proceso que los crea)
**/
int* bufferGlobal;
int* bufferLocal;
int* matrix;
int rows,cols,mid;

/**print a vector to file, n length **/
void printVector(int *v, int n ){
	
    FILE * fout;
    fout = fopen("V.txt","w");
	
    int i;
    for(i=0;i<n;i++){
        printf("%d ",v[i]);
		fprintf(fout,"%d\n",v[i]);
	}
	putchar('\n');
    fclose(fout);
}


int pos_neg (){
	
	int result=1;
	if(random()% 2)
		result*=-1;
	
	return result;
}

/*calculates sum of colums of rowindex for matrix
ri row index, c amount of columns in matrix*/
int row_sum(int* matrix, int ri, int c){
	
  int i,sum=0;
  int start=ri*c;
  
  for(i=start; i<(start+c); i++){
		//do stuff to this row
		sum+=matrix[i];
  }
  return sum;
}

/** returns a matrix of random numbers between -+MAX **/
int* make_matrix(int rows, int cols){
  
  srandom(MPI_Wtime()); 	
  int* matrix = (int*)malloc(rows*cols*(sizeof(int)));
  //fill this process matrix with random numbers between -1,1
  int n=rows;
  int m=cols;
  int i,j;
  for(i=0; i<(n*m); i+=m){
	  //do stuff to this row
	for(j=0;j<m;j++){
		matrix[i+j]=(random()% MAX) * pos_neg();
		//printf("%d ",&matrix[i+j]);
		//putchar('\n');
	}
  }
  return matrix;
}
/**TODO this should be done with pthreads**/
/**calculates the local vector of sums **/
int* calc_localsums(int* matrix, int rows, int cols){
  
  int* buffer = (int*)malloc(rows*(sizeof(int)));//container of row sums 
  int i;
  for(i=0; i<rows; i++){
	  buffer[i]=row_sum(matrix,i,cols);
  }
  return buffer;
}



/**for Phtreads, every Pthread needs a wrapping function wich contains the code
to execute in the thread, such funtion, called here entryPoint, takes a void* parameter
and returns a void*, using return NULL **/
void* entryPoint(void* pthreadId){//every Phtread needs to start with an entry point funtion
		
	int left,right;
	//lets cast the type void* to int* and derefference it to get the Pthreadid int
	if(*(int*)pthreadId){//is not pthreadId 0
		
		right=rows;
		for(left=mid; left<rows; ++left)
			bufferLocal[left]=row_sum(matrix,left,cols);		
	}
	else{//is pthreadId 0
		
		right=mid;
		for(left=0; left<mid; ++left)
			bufferLocal[left]=row_sum(matrix,left,cols);
	}
	
	return NULL;
} 


int main(int argc, char** argv) {
  // Initialize the MPI environment
  MPI_Init(NULL, NULL);
  // Find out this process rank in the world
  int my_world_rank;
  MPI_Comm_rank(MPI_COMM_WORLD, &my_world_rank);
  MPI_Status status;
  
  mid=(rows/2);// middle index to spread rows amongst Phtreads

  /**for Pthreads**/
  int id_0 = 0;
  int id_1 = 1;

  int* id0 = &id_0;
  int* id1 = &id_1;

  int num_threads = 2;

  pthread_t thread_0;
  pthread_t thread_1;
  
  /**if there was a concurrent access to a variable by serveral Pthreads some sort
  of lock(semophore) would be needed, sucha as a mutex(mutualExclution)**/
  
  /**end of Phtreads needed info**/
  
  /**root process 0 is the interfaze with user**/
  if(my_world_rank==0){
	  printf(" Digite cuantas filas\n");
	  scanf("%d", &rows);
	  printf(" Digite cuantas columnas\n");
	  scanf("%d", &cols);
  }
 
 /**notice how MPI_Bcast is used for all processes and not just within root
 this is because is a wrapping funtion, implemented with smart coding of MPI_send and 
 MPI_rcv, so is actually a if-send-else-recv function**/
 
  MPI_Bcast(&rows,1,MPI_INT,0,MPI_COMM_WORLD);
  MPI_Bcast(&cols,1,MPI_INT,0,MPI_COMM_WORLD);
   
  //scanf("%d", &rows);
  //scanf("%d", &cols);
  //rows=10000,cols=10;
  
  matrix=make_matrix(rows,cols);//a buffer to allocate linear matrix 
  
  /**ALL PROCESSes**/
  bufferGlobal = (int*)malloc(rows*(sizeof(int)));//container of final results
  
  //calculate array of sums for each row 
  bufferLocal = (int*)malloc(rows*(sizeof(int)));//container of row sums 
  
  /**se hacen los hilos, ojo q es con un llamado a funcion con paso por referencia
	 (void*) es un casteo a tipo void* puntero void **/
  pthread_create (&thread_0, NULL, entryPoint, (void*) id0);
  pthread_create (&thread_1, NULL, entryPoint, (void*) id1);
  
  //synch de los hilos, como barrier en mpi
  pthread_join(thread_0, NULL);
  pthread_join(thread_1, NULL);
   
  //use mpi reduce with array variables storing final result in bufferGlobal and sending to root
  MPI_Reduce(bufferLocal, bufferGlobal, rows, MPI_INT, MPI_SUM, 0, MPI_COMM_WORLD);
  
  if(my_world_rank==0){
	  
	printVector(bufferGlobal,rows);
  }
  else{}
 
  MPI_Finalize();
}