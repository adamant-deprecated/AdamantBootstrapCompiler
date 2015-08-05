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
	: Whitespace* '#' InputChar* { Preprocess(); } -> skip
	;

// Here we use a mode to handle preprocessor sections that are skipped
mode PREPROCESSOR_SKIP;

PreprocessorSkippedSection
	: InputChar+? Whitespace* '#' 
	;

PreprocessorLineInSkipped
	// the type here prevents it from creatig another token type
	: Whitespace* '#' InputChar* { Preprocess(); } -> type(PreprocessorLine), skip 
	;

// And switch back to default mode so rest of file is correct
mode DEFAULT_MODE;

//*************
// Keywords
//*************
Using : 'using';
Namespace : 'namespace';
Class : 'class';
New : 'new';
Operator : 'operator';
This : 'this';
Return : 'return';
Uninitialized : 'uninitialized';
// Conversion
Implicit : 'implicit';
Explicit : 'explicit';
Conversion : 'conversion';
// Access modifiers
Public : 'public';
Private : 'private';
Protected : 'protected;
// Safety
Safe : 'safe';
Unsafe : 'unsafe
// Ownership
Own : 'own';
Mutable : 'mut';


//*************
// Literals
//*************
BooleanLiteral
	: 'true'
	| 'false'
	;

NullLiteral : 'null';

// must be defined after all keywords so it will not match a keyword
Identifier
	: IdentifierStartChar IdentifierPartChar*
	;

EscapedIdentifier
	: '@' Identifier
	// | '@' Keyword // TODO is this needed?
	;

// Operators
Semicolon : ';';
Colon : ':';
Dot : '.';
LeftBrace : '{';
RightBrace : '}';
LeftAngle: '<';
RightAngle: '>';
LeftBracket : '[';
RightBracket : ']';
Asterisk : '*';
AtSign : '@';
AddressOf : '&';
Returns : '->';
Coalesce : '??'; // TODO this may need to be handled in the grammar level
IsNull : '?';
Equal : '==';
NotEqual : '!=';

Unknown : .; // An error catch rule for everything else