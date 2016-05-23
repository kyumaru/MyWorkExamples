/**	TAKE A LOOK AT

http://www.tutorialspoint.com/cplusplus/returning_values_by_reference.htm

http://www.tutorialspoint.com/cplusplus/cpp_overloading.htm

http://os.ecci.ucr.ac.cr/ci1320/proyectos/medicion-tiempo.html

http://www.tutorialspoint.com/cplusplus/cpp_references.htm

**/

#include <iostream>
#include <stdio.h>
#include <sys/socket.h>
#include <unistd.h>
#include <string.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <netdb.h>
#include <stdlib.h>
#include <time.h>  
#include <stdlib.h>
#include <errno.h>
#include <string.h>
#include <arpa/inet.h>

#define PORT_NUMBER     9876 // this port number should be the same in the server side
#define SERVER_ADDRESS  "163.178.104.171"//server arenal
#define FILENAME        "test.pdf"//client will save the file under this name regardless of server side naming for it
 
 
using namespace std;

class Chrono;
class Socket;




class Chrono{

    public:
	
	//declarations
/**
	Chrono();
	Chrono( struct timespec & );
	~Chrono();
	int getTime();	// Reads system time
	int getSecs();	// Returns the seconds part of time in variable
	int getnSecs();	// Returns the nanoseconds part of time in variable
	Chrono & operator =  ( const Chrono & rhs );
	Chrono & operator +  ( const Chrono & rhs );
	Chrono & operator -  ( const Chrono & rhs );
	Chrono & operator += ( const Chrono & rhs );
	Chrono & operator -= ( const Chrono & rhs );
**/

	struct timespec ts;
	
	

	
	
	Chrono() {
		ts.tv_sec = 0;
		ts.tv_nsec = 0;
	}
	
	Chrono( struct timespec & t ) {
		ts.tv_sec = t.tv_sec;
		ts.tv_nsec = t.tv_nsec;
	}
	
	~Chrono() {
	}
	
	int getTime() {
		struct timespec t;
		clock_gettime( CLOCK_MONOTONIC, &t );
		ts.tv_sec = t.tv_sec;
		ts.tv_nsec = t.tv_nsec;
	}
	
	int getSecs() {
		return ts.tv_sec;
	}
	
	int getNSecs() {
	   return ts.tv_nsec;
	}
	
	Chrono & operator = ( const Chrono &rhs ) {
	   if ( this != &rhs ) {
		  ts.tv_sec = rhs.ts.tv_sec;
		  ts.tv_nsec = rhs.ts.tv_nsec;
	   }
		return *this;
	}
	
	Chrono & operator - ( const Chrono &rhs ) {
	   if ( this != &rhs ) {
			  if ( (ts.tv_nsec - rhs.ts.tv_nsec) < 0) {	// nanoseconds difference is negative
				 ts.tv_sec = ts.tv_sec - rhs.ts.tv_sec - 1;	// Substract one from seconds count
				 ts.tv_nsec = 1000000000 + ts.tv_nsec - rhs.ts.tv_nsec;	// Add one second to diff
			  } else {
				 ts.tv_sec -= rhs.ts.tv_sec;
				 ts.tv_nsec -= rhs.ts.tv_sec;
			  }
		   }
		   
		return *this;
	}
	
	
	//a reference is another variable(with another name) that points to the same object
	//Chrono & means this method return a reference
	Chrono & operator -= ( const Chrono &rhs ) {//const Chrono &rhs means pass by reference
		return *this - rhs;//*this means dereference this
	}
	
	Chrono & operator += ( const Chrono &rhs ) {
		return *this + rhs;
	}
	
	Chrono & operator +  ( const Chrono & rhs ){
		/* may be valid but generates a warning from compiler
		Chrono c;//variable container of the rhs+this which is a Chrono object as well
		c.ts.tv_sec = this->ts.tv_sec + rhs.ts.tv_sec;
		c.ts.tv_nsec = this->ts.tv_nsec + rhs.ts.tv_nsec;
		
		return c;
        */		
		
		//using object pointed by this as container
		ts.tv_sec = this->ts.tv_sec + rhs.ts.tv_sec;
		ts.tv_nsec = this->ts.tv_nsec + rhs.ts.tv_nsec;
		/** OR
			this->ts.tv_sec = this->ts.tv_sec + rhs.ts.tv_sec;
			this->ts.tv_nsec = this->ts.tv_nsec + rhs.ts.tv_nsec;
			OR
			ts.tv_sec = ts.tv_sec + rhs.ts.tv_sec;
		    ts.tv_nsec =ts.tv_nsec + rhs.ts.tv_nsec;
		
		**/
		
		return *this;//dereference this and return object
	}
	
	
};

 
class Socket{
  public:
   
  int type;/***SOCK_STREAM ('s') o SOCK_DGRAM ('d'), para las pruebas que vamos a
                   realizar se debe escoger "SOCK_STREAM"***/      
  bool ipv6;
  int domain;//dominio PF_INET y AF_INET = 2
  int protocol;//leave up to OS the protocol choice for the given type
  int socketfd;//the int returned by socket s.c this number is called socket file descriptor
   
  Socket(int t=SOCK_STREAM ,bool ipv=false, int d=AF_INET): type(t),
         ipv6(ipv), domain(d),protocol(0),socketfd(socket(d,t,0)){}
   
  //overload must receive the fd of the socket
  Socket(int fd, char c): socketfd(fd), type(SOCK_STREAM), ipv6(false), domain(AF_INET),
        protocol(0){ }
   
 
   
  ~Socket(){}
   
   
  int Close(){
		return close(socketfd);
  }  
	
 int Bind(int Port){
        
    struct sockaddr_in serv_addr;
    
    if (socketfd < 0)  
        //error("ERROR opening socket");
    
     bzero((char *) &serv_addr, sizeof(serv_addr));//fill serv_addr with 0s
 
     serv_addr.sin_family = AF_INET;
     serv_addr.sin_addr.s_addr = INADDR_ANY;
     serv_addr.sin_port = htons(Port);
      
     return bind(socketfd, (struct sockaddr*) &serv_addr, sizeof(serv_addr));    
}
 
int Listen(int Queue){
    
    return listen(socketfd,Queue);
 
}
 
Socket* Accept(){// devuelve una ref bla bla
      
     socklen_t clilen;
     struct sockaddr_in cli_addr;
     int newsockfd;
     clilen = sizeof(cli_addr);
     newsockfd = accept(socketfd, (struct sockaddr *) &cli_addr, &clilen);
           
     if (newsockfd < 0)  
          //error("ERROR on accept");
            
   //hay q crear el socket y obtener la ref de este
      
     return new Socket(newsockfd,'c');//use overload version  
}  
   
   
  /**DE aqui para abajo codigo cliente**/
  /****************************************/
  /*****************************************/
    /****************************************/
  /*****************************************/
 
//method overload 
int Connect(char* Host, char* Port){//para realizar las conexiones 	
	


	 struct addrinfo hints, *res;
	 struct in_addr addr;
 	 int err;

	 memset(&hints, 0, sizeof(hints));
 	 hints.ai_socktype = SOCK_STREAM;
 	 hints.ai_family = AF_INET;

 	if ((err = getaddrinfo(Host, Port, &hints, &res)) != 0) {
   		printf("error %d\n", err);
 	  	return 1;
 	}

 		

	return connect(socketfd, res->ai_addr, res->ai_addrlen);            

	
}  
   
   
  int Connect(char* Host, int Port){//para realizar las conexiones  
    
    struct hostent* server = gethostbyname(Host);//works on ipv6 and ipv4  
    int result=-1;
    
    if (socketfd < 0)  
        cout<<"ERROR socket() sc devolvio -1"<<endl;
     
    if (server != NULL) {//avoid segmentation fault if server is NULL
    
        if(ipv6){//
            struct sockaddr_in6 serv_addr6;
            memset((char*) &serv_addr6,'-',sizeof(serv_addr6));//clean the struct of junk values
            memcpy((char *)&serv_addr6.sin6_addr.s6_addr, (char*)server->h_addr, server->h_length);
            serv_addr6.sin6_family = AF_INET6;
            serv_addr6.sin6_port = htons(Port);
            result=connect(socketfd,(struct sockaddr *) &serv_addr6,sizeof(serv_addr6));            
        }
        else{
            struct sockaddr_in server_address;  
            memset((char*) &server_address,'-',sizeof(server_address));
            memcpy((char *)&server_address.sin_addr.s_addr, (char*)server->h_addr, server->h_length);
            server_address.sin_family = AF_INET;
            server_address.sin_port = htons(Port);
            result=connect(socketfd,(struct sockaddr *) &server_address,sizeof(server_address));            
        }
    }    
    else{
            cout<<" ERROR, con el nombre del host\n";
            this->Shutdown(SHUT_RDWR);
        }
    
    return result;  
  }
   
  int Read(char* text, int len){
       
      return read(socketfd,text,len);
  }
   
  int Write(char* text, int len){
       
      return write(socketfd,text,strlen(text));
  }
   
  int Shutdown( int i ){
      /* i ?? SHUT_RD 0, SHUT_WR 1, SHUT_RDWR 2*/
       
      return shutdown(socketfd,i);//clean all??  
  }
 
};//Socket class end


/*socket client*/
int main( int argc, char * argv[] ){
    
    Chrono ci, cf; 

    int client_socket;
    ssize_t len;
    struct sockaddr_in remote_addr;
    char buffer[BUFSIZ];
    int file_size;
    FILE *received_file;
    int remain_data = 0;
    long double rate;

	
    Socket s;
    s.Bind(PORT_NUMBER);//this receives port as int
	//try to connect to server
    s.Connect(SERVER_ADDRESS, PORT_NUMBER);
    client_socket=s.socketfd;
    
    ci.getTime();	// Start the time measurement    

	/* Receiving file size */
        recv(client_socket, buffer, BUFSIZ, 0);
        file_size = atoi(buffer);
        //fprintf(stdout, "\nFile size : %d\n", file_size);

        received_file = fopen(FILENAME, "w");
        if (received_file == NULL)
        {
                fprintf(stderr, "Failed to open file foo --> %s\n", strerror(errno));

                exit(EXIT_FAILURE);
        }

        remain_data = file_size;

        while (((len = recv(client_socket, buffer, BUFSIZ, 0)) > 0) && (remain_data > 0))
        {
                
		fwrite(buffer, sizeof(char), len, received_file);
                remain_data -= len;
                fprintf(stdout, "Receive %d bytes and we hope :- %d bytes\n", len, remain_data);
	
        	cf.getTime();
	       	cf -= ci;
         	printf( "Time taken to transfer %ld bytes is: %ld sec., %ld ns\n", len, cf.getSecs(), cf.getNSecs() );
         	
        }
        
		fclose(received_file);

		cf.getTime();	// Get the time now
   		cf -= ci;		// Calculate the difference

   		printf( "Time taken to transfer %ld bytes is: %ld sec., %ld ns\n", file_size, cf.getSecs(), cf.getNSecs() );
		

		s.Close();
}

 