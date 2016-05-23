
%%

%byaccj

%{
  private Parser yyparser;

  public Yylex(java.io.Reader r, Parser yyparser) {
    this(r);
    this.yyparser = yyparser;
  }
%}

NUMESTADO = [0-9]+ //cualquier numero de estado
NL	= \n | \r | \r\n
IDTRANSITION  = [a-z] | "epsilon" //mejor si pudiera un unico simbolo q no sea un numero, ver NUMESTADO



%%


{NL}   { return Parser.NL; }

{IDTRANSITION}   { yyparser.yylval= new ParserVal(yytext()) ; return Parser.IDTRANSITION; }

{NUMESTADO}  { yyparser.yylval= new ParserVal(yytext()); return Parser.NUMESTADO; } // numero de estado

"hay una transicion desde" { return Parser.prHAYUNATRAN ; }

"hacia" { return Parser.prHACIA ; }

"es estado final" { return Parser.prESTADOFINAL ; }

"por" { return Parser.prPOR ; }

"hay un estado" { return Parser.prHAYUNESTADO ; }

"hay un simbolo" { return Parser.prHAYUNSIMBOL ; }

/* whitespace */
[ \t]+ { }

/* error fallback */
[^]    { System.out.println("Error: CARACTER NO IDENTIFICADO '"+yytext()+"'"); }
