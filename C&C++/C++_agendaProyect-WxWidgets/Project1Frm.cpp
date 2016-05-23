///-----------------------------------------------------------------
///
/// @file      Project1Frm.cpp
/// @author    kyumaru
/// Created:   11/21/2012 5:53:19 AM
/// @section   DESCRIPTION
///            Project1Frm class implementation
///
///------------------------------------------------------------------

#include "Project1Frm.h"

///////////mi codigo/////////////////start//////////////

#include <iostream>
#include <fstream>
#include <string>

using namespace std;

class Dato{

    public:
    wxString nombre, fecha;
};

class Contact: public Dato{

public:
        wxString email, telef, parentesco;
};


class Remainder: public Dato{

    public:
       wxString asunto, lugar, hinicio, hfin;

};

class Task: public Dato{
    
    public:
       wxString prioridad, estado, descripcion;
};

//contenedor emplantillado
template <class B>
class Bolsa{

 public:

    template <class T=B>
    class Nodo{

    friend class Bolsa<B> ;

    public:

    T data ;
    Nodo<T> * next ;

    Nodo<T>(T d){data=d; next=0; }
    };//nodo end

 public:

 Nodo<B> * inicio ;

 Bolsa<B>(void){inicio=0;}//constructor default
};//bolsa end

///////////////////definicion de bolsas////////////////

Bolsa<Contact> bolsaContacts;
Bolsa<Remainder> bolsaRemainders;
Bolsa<Task> bolsaTasks;

//metodos papi de tomates

//hace toda la implementacion de un contacto en memoria
//carga un contacto en memoria y devuevle una cadena wx que lista para imprimirse
wxString implementContact(wxString n,wxString f,wxString e,wxString t,wxString par){
      //crear contacto
    Contact * unContacto = new Contact;
    //rellenar el contacto
    unContacto->email = e;//parametrizar esto
    unContacto->telef = t;
    unContacto->parentesco = par;
    unContacto->nombre = n;
    unContacto->fecha = f;
    //crear Nodo
 Bolsa<Contact>::Nodo<Contact> * pc=new Bolsa<Contact>::Nodo<Contact>(*unContacto);
 Bolsa<Contact>::Nodo<Contact> * p=0;  
    
    //meter nodo en la bolsa
    if(!bolsaContacts.inicio){ bolsaContacts.inicio=pc; }//mete al nodo al inicio
    else
       {
           p = bolsaContacts.inicio;
            
           while(p->next)//me deja apuntando al ultimo nodo en la bolsa
               p=p->next;
               
           p->next=pc;//mete al nodo
       }     
  //crear cadena que se va imprimir a pantalla
   p = bolsaContacts.inicio;
     
   wxString cadenaContactos=""; 
   int cont=1;
   
   while(p){
       
       wxString mystring = wxString::Format(wxT("%i"),cont); 
       cadenaContactos = cadenaContactos+
       "=========================CONTACTO # "+ mystring + "==========================\n\n"+
       "Nombre> " + p->data.nombre+"\n\n"+
       "Fecha> " + p->data.fecha+"\n\n"+
       "Email> " + p->data.email+"\n\n"+
       "Telefono> " + p->data.telef+"\n\n"+
       "Parentesco> " + p->data.parentesco+"\n\n"+
       "\n";
       p=p->next;
       cont+=1;
   }
	
   return cadenaContactos; 
}


wxString implementReminder(wxString n,wxString f,wxString a,wxString l,wxString hi,wxString hf ){
      //crear 
    Remainder * unRemainder = new Remainder;
    //rellenar 
    unRemainder->asunto = a;//parametrizar esto
    unRemainder->lugar = l;
    unRemainder->hinicio = hi;
    unRemainder->hfin = hf;
    unRemainder->nombre = n;
    unRemainder->fecha = f;
    //crear 
 Bolsa<Remainder>::Nodo<Remainder> * pc=new Bolsa<Remainder>::Nodo<Remainder>(*unRemainder);
 Bolsa<Remainder>::Nodo<Remainder> * p=0;  
    
    //meter  en la bolsa
    if(!bolsaRemainders.inicio){ bolsaRemainders.inicio=pc; }//mete al nodo al inicio
    else
       {
           p = bolsaRemainders.inicio;
            
           while(p->next)//me deja apuntando al ultimo nodo en la bolsa
               p=p->next;
               
           p->next=pc;//mete al nodo
       }     
  //crear cadena que se va imprimir a pantalla
   p = bolsaRemainders.inicio;
     
   wxString cadena=""; 
   int cont=1;
   
   while(p){
       
       wxString mystring = wxString::Format(wxT("%i"),cont); 
       cadena = cadena+
       "======================RECORDATORIO # "+ mystring + "==========================\n\n"+
       "Nombre> " + p->data.nombre+"\n\n"+
       "Fecha> " + p->data.fecha+"\n\n"+
       "Hora Inicio> " + p->data.hinicio+"\t\t"+
       "Hora Final> " + p->data.hfin+"\n\n"+
       "Lugar> " + p->data.lugar+"\n\n"+
       "Asunto> " + p->data.asunto+"\n\n"+
       "\n";
       p=p->next;
       cont+=1;
   }
	
   return cadena; 
}

wxString implementTask(wxString n,wxString f,wxString pri,wxString e,wxString d){
      //crear tarea
    Task * unTask = new Task;
    //rellenar tarea
    unTask->descripcion = d;
    unTask->estado = e;
    unTask->prioridad = pri;
    unTask->nombre = n;
    unTask->fecha = f;
    //crear Nodo
 Bolsa<Task>::Nodo<Task> * pc=new Bolsa<Task>::Nodo<Task>(*unTask);
 Bolsa<Task>::Nodo<Task> * p=0;  
    
    //meter nodo en la bolsa
    if(!bolsaTasks.inicio){ bolsaTasks.inicio=pc; }//mete al nodo al inicio
    else
       {
           p = bolsaTasks.inicio;
            
           while(p->next)//me deja apuntando al ultimo nodo en la bolsa
               p=p->next;
               
           p->next=pc;//mete al nodo
       }     
  //crear cadena que se va imprimir a pantalla
   p = bolsaTasks.inicio;
     
   wxString cadena=""; 
   int cont=1;
   
   while(p){
       
       wxString mystring = wxString::Format(wxT("%i"),cont); 
       cadena = cadena+
       "======================TAREA # "+ mystring + "==========================\n\n"+
       "Nombre> " + p->data.nombre+"\n\n"+
       "Fecha> " + p->data.fecha+"\n\n"+
       "Prioridad> " + p->data.prioridad+"\n\n"+
       "Estado> " + p->data.estado+"\n\n"+
       "Descripcion> " + p->data.descripcion+"\n\n"+
       "\n";
       p=p->next;
       cont+=1;
   }
	
   return cadena; 
}


  
void LoadFromFile(string nameFile,wxTextCtrl*& field){

     string fnombre="", ffecha="", femail="", ftelef="", fpar="", fasunto="", flugar="", fhini="", fhfin="", 
     fpriori="", fdescrip="", festado="", line="";	
    
    ifstream myfile;	
    
    if(nameFile=="contactos")
    
       myfile.open ("contactos.txt", ifstream::in);
    
    if(nameFile=="recordatorios")
    
       myfile.open ("recordatorios.txt", ifstream::in);
      
    if(nameFile=="tareas")
    
       myfile.open ("tareas.txt", ifstream::in);   
   
    int checksum=0;//se usa como flag para saber cuando las cadenas estan listas para imprimir
        
          if (myfile.is_open())//si se pudo abrir myfile exitosamente 
          {
             while( myfile.good() )//mientras no se llegue a fin de archivo
             {
                getline (myfile,line);
        
                if(line=="%nombre%") getline (myfile,fnombre);//pase a la siguiente linea
                                        
                if(line=="%fecha%") getline (myfile,ffecha);
                    
                if(line=="%email%"){ getline (myfile,femail); checksum+=2; }
                
                if(line=="%telefono%"){getline (myfile,ftelef); checksum+=3; }

                if(line=="%parentesco%"){ getline(myfile,fpar); checksum+=5;} 

                
                if(checksum==10){//hay un contacto lleno
                   field->SetValue( implementContact(fnombre, ffecha, femail, ftelef, fpar) );
                   checksum=0;                
                }
        
                if(line=="%asunto%"){getline (myfile,fasunto); checksum+=7;}
                    
                if(line=="%lugar%"){getline (myfile,flugar); checksum+=11;}
                    
                if(line=="%hinicio%"){getline (myfile,fhini); checksum+=13;}
                           
                if(line=="%hfin%"){getline (myfile,fhfin); checksum+=17;}
                
               
                if(checksum==48){//hay un reminder lleno
                   field->SetValue( implementReminder(fnombre, ffecha, fasunto, flugar, fhini, fhfin) );
                   checksum=0;                
                }
                
                if(line=="%prioridad%"){ getline (myfile,fpriori); checksum+=19;}
                
                if(line=="%estado%"){getline (myfile,festado); checksum+=23;}
                
                if(line=="%descripcion%"){getline (myfile,fdescrip); checksum+=29;}    
                            
                if(checksum==71){//hay un task lleno
                   field->SetValue(implementTask(fnombre, ffecha, fpriori, festado, fdescrip)); 
                   checksum=0;                
                }
                      
            }//while end
       myfile.close();
    }//if myfile open end
}//end of LoadFromFile

/////////////////////mi codigo///////////////////////

//Do not add custom headers between
//Header Include Start and Header Include End
//wxDev-C++ designer will remove them
////Header Include Start
////Header Include End

//----------------------------------------------------------------------------
// Project1Frm
//----------------------------------------------------------------------------
//Add Custom Events only in the appropriate block.
//Code added in other places will be removed by wxDev-C++
////Event Table Start
BEGIN_EVENT_TABLE(Project1Frm,wxFrame)
	////Manual Code Start
	////Manual Code End
	
	EVT_CLOSE(Project1Frm::OnClose)
	EVT_BUTTON(ID_WXBUTTON6,Project1Frm::WxButton6Click)
	EVT_BUTTON(ID_WXBUTTON5,Project1Frm::WxButton5Click)
	EVT_BUTTON(ID_WXBUTTON4,Project1Frm::WxButton4Click)
	EVT_BUTTON(ID_WXBUTTON3,Project1Frm::WxButton3Click)
	EVT_BUTTON(ID_WXBUTTON2,Project1Frm::WxButton2Click)
	EVT_BUTTON(ID_WXBUTTON1,Project1Frm::WxButton1Click)
	
	EVT_UPDATE_UI(ID_WXNOTEBOOKPAGE3,Project1Frm::WxNoteBookPage3UpdateUI)
	
	EVT_UPDATE_UI(ID_WXNOTEBOOKPAGE2,Project1Frm::WxNoteBookPage2UpdateUI)
	
	EVT_UPDATE_UI(ID_WXNOTEBOOKPAGE1,Project1Frm::WxNoteBookPage1UpdateUI)
	
	EVT_NOTEBOOK_PAGE_CHANGED(ID_WXNOTEBOOK1,Project1Frm::WxNotebook1PageChanged)
END_EVENT_TABLE()
////Event Table End

Project1Frm::Project1Frm(wxWindow *parent, wxWindowID id, const wxString &title, const wxPoint &position, const wxSize& size, long style)
: wxFrame(parent, id, title, position, size, style)
{
	CreateGUIControls();
}

Project1Frm::~Project1Frm()
{
}

void Project1Frm::CreateGUIControls()
{  
	//Do not add custom code between
	//GUI Items Creation Start and GUI Items Creation End
	//wxDev-C++ designer will remove them.
	//Add the custom code before or after the blocks
	////GUI Items Creation Start

	WxNotebook1 = new wxNotebook(this, ID_WXNOTEBOOK1, wxPoint(209, 3), wxSize(507, 345), wxNB_DEFAULT | wxNB_MULTILINE);

	WxNoteBookPage1 = new wxPanel(WxNotebook1, ID_WXNOTEBOOKPAGE1, wxPoint(4, 25), wxSize(499, 316));
	WxNoteBookPage1->SetBackgroundColour(wxColour(128,128,192));
	WxNotebook1->AddPage(WxNoteBookPage1, wxT("CONTACTOS"));

	WxEdit1 = new wxTextCtrl(WxNoteBookPage1, ID_WXEDIT1, "", wxPoint(-2, 0), wxSize(502, 317), wxTRANSPARENT_WINDOW | wxVSCROLL | wxHSCROLL | wxALWAYS_SHOW_SB | wxTE_READONLY | wxTE_LEFT | wxTE_CAPITALIZE | wxTE_MULTILINE, wxDefaultValidator, wxT("WxEdit1"));
	WxEdit1->SetBackgroundColour(wxColour(128,255,128));

	WxNoteBookPage2 = new wxPanel(WxNotebook1, ID_WXNOTEBOOKPAGE2, wxPoint(4, 25), wxSize(499, 316));
	WxNoteBookPage2->SetForegroundColour(wxColour(255,128,192));
	WxNoteBookPage2->SetBackgroundColour(wxColour(255,128,192));
	WxNotebook1->AddPage(WxNoteBookPage2, wxT("RECORDATORIOS"));

	WxEdit2 = new wxTextCtrl(WxNoteBookPage2, ID_WXEDIT2, "", wxPoint(0, -1), wxSize(499, 318), wxTRANSPARENT_WINDOW | wxVSCROLL | wxHSCROLL | wxALWAYS_SHOW_SB | wxTE_LEFT | wxTE_CAPITALIZE | wxTE_MULTILINE, wxDefaultValidator, wxT("WxEdit2"));
	WxEdit2->SetBackgroundColour(wxColour(255,255,128));

	WxNoteBookPage3 = new wxPanel(WxNotebook1, ID_WXNOTEBOOKPAGE3, wxPoint(4, 25), wxSize(499, 316));
	WxNotebook1->AddPage(WxNoteBookPage3, wxT("TAREAS PENDIENTES"));

	WxEdit3 = new wxTextCtrl(WxNoteBookPage3, ID_WXEDIT3, "", wxPoint(-1, -1), wxSize(500, 317), wxTRANSPARENT_WINDOW | wxVSCROLL | wxHSCROLL | wxALWAYS_SHOW_SB | wxTE_READONLY | wxTE_LEFT | wxTE_CAPITALIZE | wxTE_MULTILINE, wxDefaultValidator, wxT("WxEdit3"));
	WxEdit3->SetBackgroundColour(wxColour(128,128,255));

	WxButton1 = new wxButton(this, ID_WXBUTTON1, wxT("+ CONTACTO"), wxPoint(68, 11), wxSize(119, 60), 0, wxDefaultValidator, wxT("WxButton1"));

	WxPanel1 = new wxPanel(this, ID_WXPANEL1, wxPoint(7, 2), wxSize(273, 348));
	WxPanel1->Show(false);

	WxEdit4 = new wxTextCtrl(WxPanel1, ID_WXEDIT4, wxT("<NOMBRE>"), wxPoint(14, 21), wxSize(224, 19), 0, wxDefaultValidator, wxT("WxEdit4"));

	WxButton2 = new wxButton(WxPanel1, ID_WXBUTTON2, wxT("AGREGAR"), wxPoint(85, 239), wxSize(75, 25), 0, wxDefaultValidator, wxT("WxButton2"));
	WxButton2->SetBackgroundColour(wxColour(128,128,255));

	WxEdit5 = new wxTextCtrl(WxPanel1, ID_WXEDIT5, wxT("<FECHA>"), wxPoint(12, 62), wxSize(224, 19), 0, wxDefaultValidator, wxT("WxEdit5"));

	WxEdit6 = new wxTextCtrl(WxPanel1, ID_WXEDIT6, wxT("<TELEFONO>"), wxPoint(13, 146), wxSize(223, 19), 0, wxDefaultValidator, wxT("WxEdit6"));

	WxEdit7 = new wxTextCtrl(WxPanel1, ID_WXEDIT7, wxT("<PARENTESCO>"), wxPoint(14, 191), wxSize(221, 19), 0, wxDefaultValidator, wxT("WxEdit7"));

	WxEdit8 = new wxTextCtrl(WxPanel1, ID_WXEDIT8, wxT("<EMAIL>"), wxPoint(10, 104), wxSize(226, 19), 0, wxDefaultValidator, wxT("WxEdit8"));

	WxButton3 = new wxButton(this, ID_WXBUTTON3, wxT("+ TAREA "), wxPoint(66, 160), wxSize(120, 60), 0, wxDefaultValidator, wxT("WxButton3"));

	WxButton4 = new wxButton(this, ID_WXBUTTON4, wxT("+ RECORDATORIO"), wxPoint(67, 83), wxSize(122, 60), 0, wxDefaultValidator, wxT("WxButton4"));

	WxPanel2 = new wxPanel(this, ID_WXPANEL2, wxPoint(12, 5), wxSize(275, 340));
	WxPanel2->Show(false);

	WxEdit9 = new wxTextCtrl(WxPanel2, ID_WXEDIT9, wxT("<NOMBRE>"), wxPoint(31, 12), wxSize(211, 24), 0, wxDefaultValidator, wxT("WxEdit9"));

	WxEdit10 = new wxTextCtrl(WxPanel2, ID_WXEDIT10, wxT("<FECHA>"), wxPoint(33, 49), wxSize(208, 24), 0, wxDefaultValidator, wxT("WxEdit10"));

	WxEdit11 = new wxTextCtrl(WxPanel2, ID_WXEDIT11, wxT("<LUGAR>"), wxPoint(32, 87), wxSize(206, 23), 0, wxDefaultValidator, wxT("WxEdit11"));

	WxEdit12 = new wxTextCtrl(WxPanel2, ID_WXEDIT12, wxT("<HORA INICIO>"), wxPoint(39, 124), wxSize(86, 33), 0, wxDefaultValidator, wxT("WxEdit12"));

	WxEdit13 = new wxTextCtrl(WxPanel2, ID_WXEDIT13, wxT("<ASUNTO>"), wxPoint(38, 180), wxSize(202, 100), wxTE_MULTILINE, wxDefaultValidator, wxT("WxEdit13"));

	WxEdit14 = new wxTextCtrl(WxPanel2, ID_WXEDIT14, wxT("<HORA FIN>"), wxPoint(146, 126), wxSize(82, 33), 0, wxDefaultValidator, wxT("WxEdit14"));

	WxButton5 = new wxButton(WxPanel2, ID_WXBUTTON5, wxT("AGREGAR"), wxPoint(89, 297), wxSize(75, 25), 0, wxDefaultValidator, wxT("WxButton5"));
	WxButton5->SetBackgroundColour(wxColour(255,255,0));

	WxPanel3 = new wxPanel(this, ID_WXPANEL3, wxPoint(16, 10), wxSize(278, 346));
	WxPanel3->Show(false);

	WxEdit15 = new wxTextCtrl(WxPanel3, ID_WXEDIT15, wxT("<NOMBRE>"), wxPoint(16, 15), wxSize(240, 20), 0, wxDefaultValidator, wxT("WxEdit15"));

	WxEdit16 = new wxTextCtrl(WxPanel3, ID_WXEDIT16, wxT("<FECHA>"), wxPoint(16, 50), wxSize(240, 24), 0, wxDefaultValidator, wxT("WxEdit16"));

	WxEdit17 = new wxTextCtrl(WxPanel3, ID_WXEDIT17, wxT("<PRIORIDAD>"), wxPoint(15, 90), wxSize(241, 25), 0, wxDefaultValidator, wxT("WxEdit17"));

	WxEdit18 = new wxTextCtrl(WxPanel3, ID_WXEDIT18, wxT("<DESCRIPCION>"), wxPoint(13, 185), wxSize(247, 91), wxTE_MULTILINE, wxDefaultValidator, wxT("WxEdit18"));

	WxButton6 = new wxButton(WxPanel3, ID_WXBUTTON6, wxT("AGREGAR"), wxPoint(103, 302), wxSize(75, 25), 0, wxDefaultValidator, wxT("WxButton6"));

	WxEdit19 = new wxTextCtrl(WxPanel3, ID_WXEDIT19, wxT("<ESTADO>"), wxPoint(17, 130), wxSize(242, 25), 0, wxDefaultValidator, wxT("WxEdit19"));

	SetTitle(wxT("Project1"));
	SetIcon(wxNullIcon);
	SetSize(8,8,812,415);
	Center();	
	////GUI Items Creation End
///codigo mio para implementar bolsas desde archivo///////////////

  LoadFromFile("contactos",WxEdit1);
  LoadFromFile("recordatorios",WxEdit2);
  LoadFromFile("tareas",WxEdit3);

////////////////////////////////////////////////////////////////
}//fin de gui creation items

void Project1Frm::OnClose(wxCloseEvent& event)
{
	Destroy();
}

void Project1Frm::WxButton1Click(wxCommandEvent& event)//boton +contacto
{
    WxPanel1->Show(true);
    WxPanel1->Raise();   
}

void Project1Frm::WxButton4Click(wxCommandEvent& event)//boton +recordatorio
{
    WxPanel2->Show(true);
    WxPanel2->Raise();
}


void Project1Frm::WxButton3Click(wxCommandEvent& event)//boton +tarea
{
    WxPanel3->Show(true);
    WxPanel3->Raise();
}

void Project1Frm::WxButton2Click(wxCommandEvent& event)//agregar contacto
{
//implementar el contacto
wxString mystring = implementContact(WxEdit4->GetValue(),WxEdit5->GetValue(),WxEdit8->GetValue(),WxEdit6->GetValue(),WxEdit7->GetValue());
     
   //actualizar archivo
   
   ofstream myfile ("contactos.txt", fstream::out | fstream::app);
   if (myfile.is_open())
   {
     myfile << "%nombre%\n";   
     myfile << WxEdit4->GetValue()+"\n";
     myfile << "%fecha%\n"; 
     myfile << WxEdit5->GetValue()+"\n";
     myfile << "%email%\n"; 
     myfile << WxEdit8->GetValue()+"\n";
     myfile << "%telefono%\n"; 
     myfile << WxEdit6->GetValue()+"\n";
     myfile << "%parentesco%\n"; 
     myfile << WxEdit7->GetValue()+"\n";
     
     myfile.close();
   }     
   //restablecer balores de los campos de insercion
   
   WxEdit4->SetValue("<NOMBRE>");
   WxEdit5->SetValue("<FECHA>");
   WxEdit6->SetValue("<TELEFONO>");
   WxEdit7->SetValue("<PARENTESCO>");
   WxEdit8->SetValue("<EMAIL>");
   
   //imprimir a pantalla
   WxEdit1->SetValue(mystring); 
    
   WxPanel1->Show(false);
}

void Project1Frm::WxButton5Click(wxCommandEvent& event)//agregar recordatorio
{
  wxString mystring = implementReminder(WxEdit9->GetValue(),WxEdit10->GetValue(), WxEdit13->GetValue(), WxEdit11->GetValue(), WxEdit12->GetValue(), WxEdit14->GetValue());  
       
    //actualizar archivo   
   ofstream myfile ("recordatorios.txt", fstream::out | fstream::app);
   if (myfile.is_open())
   {
     myfile << "%nombre%\n";   
     myfile << WxEdit9->GetValue()+"\n";
     myfile << "%fecha%\n"; 
     myfile << WxEdit10->GetValue()+"\n";
     myfile << "%asunto%\n"; 
     myfile << WxEdit13->GetValue()+"\n";
     myfile << "%lugar%\n"; 
     myfile << WxEdit11->GetValue()+"\n";
     myfile << "%hinicio%\n"; 
     myfile << WxEdit12->GetValue()+"\n";
     myfile << "%hfin%\n"; 
     myfile << WxEdit14->GetValue()+"\n";
     
     myfile.close();
   }   
   //restablecer balores de los campos de insercion
   
   WxEdit9->SetValue("<NOMBRE>");
   WxEdit10->SetValue("<FECHA>");
   WxEdit11->SetValue("<LUGAR>");
   WxEdit12->SetValue("<HORA INICIO>");
   WxEdit13->SetValue("<ASUNTO>");
   WxEdit14->SetValue("<FIN>");
   
   WxEdit2->SetValue(mystring); 
    
   WxPanel2->Show(false);
}


void Project1Frm::WxButton6Click(wxCommandEvent& event)//agregar tarea
{   
   wxString mystring = implementTask(WxEdit15->GetValue(), WxEdit16->GetValue(), WxEdit17->GetValue(), WxEdit19->GetValue(), WxEdit18->GetValue());
    
   //actualizar archivo   
   ofstream myfile ("tareas.txt", fstream::out | fstream::app);
   if (myfile.is_open())
   {
     myfile << "%nombre%\n";   
     myfile << WxEdit15->GetValue()+"\n";
     myfile << "%fecha%\n"; 
     myfile << WxEdit16->GetValue()+"\n";
     myfile << "%prioridad%\n"; 
     myfile << WxEdit17->GetValue()+"\n";
     myfile << "%estado%\n"; 
     myfile << WxEdit19->GetValue()+"\n";
     myfile << "%descripcion%\n"; 
     myfile << WxEdit18->GetValue()+"\n";
     
     myfile.close();
   }  
   //restablecer balores de los campos de insercion
   
   WxEdit15->SetValue("<NOMBRE>");
   WxEdit16->SetValue("<FECHA>");
   WxEdit17->SetValue("<PRIORIDAD>");
   WxEdit18->SetValue("<DESCRIPCION>");
   WxEdit19->SetValue("<ESTADO>");
   
   WxEdit3->SetValue(mystring); 
        
   WxPanel3->Show(false);
}

////////////////////////////////codigo basura generado por basura wxwidgets
/*
 * WxNotebook1PageChanged
 */
void Project1Frm::WxNotebook1PageChanged(wxNotebookEvent& event)
{
	// insert your code here
}
/*
 * WxNoteBookPage1UpdateUI
 */
void Project1Frm::WxNoteBookPage1UpdateUI(wxUpdateUIEvent& event)
{
// insert your code here	
}
/*
 * WxNoteBookPage2UpdateUI
 */
void Project1Frm::WxNoteBookPage2UpdateUI(wxUpdateUIEvent& event)
{
	// insert your code here
}
/*
 * WxNoteBookPage3UpdateUI
 */
void Project1Frm::WxNoteBookPage3UpdateUI(wxUpdateUIEvent& event)
{
	// insert your code here
}
/*
 * WxEdit2Updated
 */
void Project1Frm::WxEdit2Updated(wxCommandEvent& event)
{
	// insert your code here
}
