#include <iostream>
#include <stdio.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <stdlib.h>
#include <errno.h>
#include <string.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <netinet/in.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <sys/sendfile.h>
#include <netdb.h>




#define PORT_NUMBER     9876
#define SERVER_ADDRESS  "localhost"
#define FILE_TO_SEND    "512k.test"

using namespace std;

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
  
/*both below are needed when socket is for a server accepting connection by another socket*/
  socklen_t clilen;
  struct sockaddr_in cli_addr;

   
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
      
     int newsockfd;
     clilen = sizeof(cli_addr);
     newsockfd = accept(socketfd, (struct sockaddr *) &cli_addr, &clilen);
           
     if (newsockfd < 0){}  
          //error("ERROR on accept");
            
   //hay q crear el socket y obtener la ref de este
      
     Socket * client= new Socket(newsockfd,'c');
	
     client->clilen=this->clilen;
     client->cli_addr=this->cli_addr;

     return client;//use overload version  
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



int main(int argc, char **argv)
{
       int server_socket;
        int peer_socket;
        socklen_t       sock_len;
        ssize_t len;
        struct sockaddr_in      server_addr;
        struct sockaddr_in      peer_addr;
        int fd;
        int sent_bytes = 0;
        char file_size[256];
        struct stat file_stat;
        off_t offset;
        int remain_data;

        /* Create server socket */
        Socket ss,*sc; //socket server, socket client ptr
	
	server_socket=ss.socketfd;

      
        /* Bind */
        ss.Bind(PORT_NUMBER);//this receives port as int
        ss.Listen(5);
	
	fd = open(FILE_TO_SEND, O_RDONLY);
        if (fd == -1)
        {
                fprintf(stderr, "Error opening file --> %s", strerror(errno));
                exit(EXIT_FAILURE);
        }

        /* Get file stats, check if is not empty */
        if (fstat(fd, &file_stat) < 0)
        {
                fprintf(stderr, "Error fstat --> %s", strerror(errno));
                exit(EXIT_FAILURE);
        }

        fprintf(stdout, "File Size: \n%d bytes\n", file_stat.st_size);


        /* Accepting incoming peer sockets as clients */
        sc = ss.Accept();//this supossedly returns a pointer to the socket client
	peer_socket =sc->socketfd;//basic pointer use


	if (peer_socket == -1)
        {
                fprintf(stderr, "Error on accept --> %s", strerror(errno));

                exit(EXIT_FAILURE);
        }

        fprintf(stdout, "Accept peer --> %s\n", inet_ntoa(peer_addr.sin_addr));

        sprintf(file_size, "%d", file_stat.st_size);

        /* Sending file size */
        len = send(peer_socket, file_size, sizeof(file_size), 0);
        if (len < 0)
        {
              fprintf(stderr, "Error send system call --> %s", strerror(errno));

              exit(EXIT_FAILURE);
        }

        fprintf(stdout, "Server sent %d bytes of total\n", len);

        offset = 0;
        remain_data = file_stat.st_size;
        /* Sending file data */
        while (((sent_bytes = sendfile(peer_socket, fd, &offset, BUFSIZ)) > 0) && 		(remain_data > 0))
        {
                fprintf(stdout, "1. Server sent %d bytes from file, offset is: %d  pending 			bytes = %d\n", sent_bytes, offset, remain_data);
                
		remain_data -= sent_bytes;

        }

        close(peer_socket);
        close(server_socket);

        return 0;
}
