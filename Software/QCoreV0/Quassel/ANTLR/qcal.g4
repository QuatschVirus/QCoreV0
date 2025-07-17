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
NUMLITERAL: DGT10+ | '0' (('x'|'X') DGT16+ | ('b'|'B') DGT2+ | ('o'|'O') DGT8+);
IDENTIFIER: (LWRCASE | UPRCASE) (LWRCASE | UPRCASE | DGT10 | '_')*;

SEMICOLON: ';';
COMMA: ',';
DOT: '.';
HASH: '#';
COLON: ':';

fragment LWRCASE: [a-z];
fragment UPRCASE: [A-Z];
fragment DGT10: [0-9];
fragment DGT8: [0-7];
fragment DGT2: [01];
fragment DGT16: [0-9a-fA-F];
fragment ESCAPEDCHAR: '\\' ('"' | '\'' | '\\' | 'n' | 't' | 'r' | 'b' | 'f' | 'v');

SPACE : [ \t]+;
NEWLINE : '\r'? '\n';

code: code_line+ EOF;
code_line: SPACE* statement? SPACE* (COMMENT | BLOCKCOMMENT)? NEWLINE;
statement: container_start | container_end | instruction | directive;
instruction: IDENTIFIER (SPACE+ parameter_list)?;
directive: (HASH IDENTIFIER) (SPACE+ parameter_list)?;
container_start: (DOT IDENTIFIER) (SPACE+ parameter_list)?;
container_end: DOT DOT;
parameter_list: parameter ((SPACE* COMMA SPACE* | SPACE) parameter)*;
parameter: parameter_identifier? parameter_value;
parameter_identifier: IDENTIFIER SPACE* COLON SPACE*;
parameter_value: regref | STRINGLITERAL | CHARLITERAL | NUMLITERAL | IDENTIFIER;
regref: GPREGREF | CSREGREF;