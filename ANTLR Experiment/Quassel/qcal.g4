grammar qcal;

options {
    language = CSharp;
}

// comments are deliberately included for code analysis purposes
COMMENT      : '//' ~[\r\n]*;
BLOCKCOMMENT : '/*' .*? '*/';

GPREGREF: ('x'|'X') (DGT10 | ('0'|'1'|'2') DGT10 | '3' ('0'|'1'));
CSREGREF: '%' (LWRCASE | UPRCASE)+;
STRINGLITERAL: '"' (ESCAPEDCHAR | ~('"'|'\\') )* '"';
CHARLITERAL: '\'' (ESCAPEDCHAR | ~('\''|'\\') ) '\'';
NUMLITERAL: DGT10+ | '0' ('x'|'X') DGT16+;
INSTRUCTION: (LWRCASE | UPRCASE)+;
IDENTIFIER: (LWRCASE | UPRCASE) (LWRCASE | UPRCASE | DGT10 | '_')*;

SEMICOLON: ';';
COMMA: ',';

fragment LWRCASE: [a-z];
fragment UPRCASE: [A-Z];
fragment DGT10: [0-9];
fragment DGT16: [0-9a-fA-F];
fragment ESCAPEDCHAR: '\\' ('"' | '\'' | '\\' | 'n' | 't' | 'r' | 'b' | 'f' | 'v');

SPACE : [ \t]+;
NEWLINE : '\r'? '\n';

program: statement_block EOF;
statement_block: statement (statement_seperator statement)* statement_seperator?;
statement_seperator: SPACE? (SEMICOLON NEWLINE? | SEMICOLON? NEWLINE) SPACE?;
statement: INSTRUCTION (SPACE parameter_list)?;
parameter_list: parameter (parameter_separator parameter)*;
parameter_separator: SPACE? COMMA SPACE? | SPACE;
parameter: (regref | STRINGLITERAL | CHARLITERAL | NUMLITERAL | IDENTIFIER);
regref: GPREGREF | CSREGREF;