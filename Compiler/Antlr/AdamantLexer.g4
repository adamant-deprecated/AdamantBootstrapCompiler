lexer grammar AdamantLexer;

import AdamantCommon; // defines Whitespace, Newline and InputChar

channels { DocComments }

options
{
	language=CSharp;
}

//*************
// Comments
//*************
SingleLineDocComment
	: '///' InputChar* -> channel(DocComments)
	;

BlockDocComment
	: '/**' .*? '*/' -> channel(DocComments)
	;

SingleLineComment
	: '//' InputChar* -> skip
	;

BlockComment
	: '/*' .*? '*/' -> skip
	;

//*************
// Preprocessor
//*************
PreprocessorLine
	: Whitespace? '#' InputChar* { Preprocess(); } -> skip
	;

// Here we use a mode to handle preprocessor sections that are skipped, this mode will be entered by the preprocessor code
mode PREPROCESSOR_SKIP;

PreprocessorLineInSkipped
	// the type here prevents it from creatig another token type
	: Whitespace? '#' InputChar* { Preprocess(); } -> type(PreprocessorLine), skip
	;

PreprocessorSkippedSection
	// anything except newline or #
	// newline is excluded becuase otherwise a multiline match could swollow the leading whitespace we need to check
	// that preprocessor directives are the first thing on the line
	: ~[\u000D\u000A\u0085\u2028\u2029#]+ -> skip
	;

PreprocessorSkippedNewline
	// the type here prevents it from creatig another token type
	: Newline -> type(Newline), skip
	;

// And switch back to default mode so rest of file is correct
mode DEFAULT_MODE;

//*************
// Keywords
//*************
Using : 'using';
Namespace : 'namespace';
Class : 'class';
Enum : 'enum';
New : 'new';
Delete : 'delete';
Operator : 'operator';
This : 'this';
Return : 'return';
Uninitialized : 'uninitialized';
Where : 'where';
Var : 'var';
Let : 'let';
Get : 'get';
Set : 'set';
Do : 'do';
While : 'while';
If : 'if';
Else : 'else';
Abstract : 'abstract';
Params : 'params';
For : 'for';
Foreach : 'foreach';
In : 'in';
Ref : 'ref';
Base : 'base';
Const : 'const';
Sealed : 'sealed';
Override : 'override';
Partial : 'partial';
Yield : 'yeild';
Switch : 'switch';
Break : 'break';
Continue : 'continue';

//Exceptions
Try : 'try';
Catch : 'catch';
Finally : 'finally';
Throw : 'throw';

// Conversion
Implicit : 'implicit';
Explicit : 'explicit';
Conversion : 'conversion';

// Access modifiers
Public : 'public';
Private : 'private';
Protected : 'protected';
Package : 'package';

// Safety
Safe : 'safe';
Unsafe : 'unsafe';

// Ownership
Own : 'own';
Mutable : 'mut';
Immutable : 'immut';

// Async
Async: 'async';
Await: 'await';

//*************
// Literals
//*************
BooleanLiteral
	: 'true'
	| 'false'
	;

IntLiteral
	: [1-9] ('_'|DecimalDigit)*
	| '0'
	;

NullLiteral : 'null';

StringLiteral
	: '"' (InputChar|'\"'|'\\')*? '"'
	;

//*************
// Operators
//*************
Semicolon : ';';
Colon : ':';
Dot : '.';
Comma : ',';
Arrow : '->';
Lambda : '=>';
LeftBrace : '{';
RightBrace : '}';
LeftAngle: '<';
RightAngle: '>';
LeftBracket : '[';
RightBracket : ']';
LeftParen : '(';
RightParen : ')';
Asterisk : '*';
AtSign : '@';
AddressOf : '&';
Coalesce : '??'; // TODO this may need to be handled in the grammar level
IsNull : '?';
Equal : '==';
NotEqual : '<>';
LessThanOrEqual : '<=';
GreaterThanOrEqual : '>=';
TypeList : '...';
Plus : '+';
Minus : '-';
Divide : '/';
And : 'and';
Xor : 'xor';
Or : 'or';
Not : 'not';
Increment : '++';
Decrement : '--';

Assign : '=';
AddAssign : '+=';
SubtractAssign : '-=';
MultiplyAssign : '*=';
DivideAssign : '/=';
LeftShiftAssign : '<<=';
RightShiftAssign : '>>=';
AndAssign : 'and=';
XorAssign : 'xor=';
OrAssign : 'or=';

// must be defined after all keywords so it will not match a keyword
Identifier
	: IdentifierStartChar IdentifierPartChar*
	;

EscapedIdentifier
	: '@' IdentifierStartChar IdentifierPartChar*
	;

//*************
// Error Rules
//*************
BadNotEqual : '!=' { NotifyErrorListeners("Invalid operator, use '<>' for not equal instead of '!='"); } -> type(NotEqual);
Unknown : .; // An error catch rule for everything else