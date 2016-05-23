

%{
  import java.io.*;
  import java.util.*; //para usar arraylist
%}
      
%token NL          
%token <sval> NUMESTADO 
%token <sval> IDTRANSITION
%token prHAYUNATRAN
%token prHACIA
%token prPOR
%token prESTADOFINAL
%token prHAYUNESTADO
%token prHAYUNSIMBOL

%type <sval> INS

      
%%

I:   /* epsilon */    
     | I L
       ;
      
L:    INS NL  {
				
				//System.out.println(matrix.get(0));					
									
			  } //aqui se toma la accion final
       ;        
      
INS:   
		
		
		| prHAYUNESTADO NUMESTADO  {
				
				
				ArrayList<String> newRow=new ArrayList<String>();
				for (String elem: matrix.get(0)) {
						newRow.add("-");
				}
				newRow.set(0,"q"+$2);	
				matrix.add( newRow );					
		}
	
		
		| prHAYUNSIMBOL IDTRANSITION {
				matrix.get(0).add(++colums,$2);				
		}
		
		//hay una transicion desde 2 hacia 1 por a

		//asumir tabla esta bien creada, transiciones acordes validas
		| prHAYUNATRAN NUMESTADO prHACIA NUMESTADO prPOR IDTRANSITION {		
				matrix.get((Integer.parseInt($2))+1).set(matrix.get(0).indexOf($6),$4);	//matrix.get() da la fila			
		}
		
		| NUMESTADO prESTADOFINAL {
				matrix.get((Integer.parseInt($1))+1).set(colums+1,"acc");				
		}
								
		
	
	   ;//gramar rules delimiter

%%

  //int rows=0;
  int colums=0;

  //tabla con una lista vacia
  public static ArrayList<ArrayList<String>> matrix = new ArrayList<ArrayList<String>>();
	
  private Yylex lexer;

  private int yylex () {
    int yyl_return = -1;
    try {
      yylval = new ParserVal(0);
      yyl_return = lexer.yylex();
    }
    catch (IOException e) {
      System.err.println("IO error :"+e);
    }
    return yyl_return;
  }


  public void yyerror (String error) {
    System.err.println ("Error: " + error);
  }


  public Parser(Reader r) {
    lexer = new Yylex(r, this);
  }


  public static void main(String args[]) throws IOException {
	
	matrix.add(new ArrayList<String>());
	matrix.get(0).add("--");
	matrix.get(0).add("$");
	
    System.out.println("\t EL AUTOMATA DEFINIDO EN LA ENTRADA ES:\n\n");

    Parser yyparser;
    if ( args.length > 0 ) {
	  yyparser = new Parser(new FileReader(args[0]));
    } else {
      //System.out.print("\t EL AUTOMATA DEFINIDO EN LA ENTRADA ES: ");
	  yyparser = new Parser(new InputStreamReader(System.in));
    }

    yyparser.yyparse();
	
	for (ArrayList<String> u: matrix) {
		System.out.println(u+"\n");					
	}
	System.out.println("\n");
  }
