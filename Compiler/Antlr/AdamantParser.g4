parser grammar AdamantParser;

options
{
	language=CSharp;
	tokenVocab=AdamantLexer;
}

compilationUnit
	: usingStatement*
	  // globalAttribute*
	  namespaceMemberDeclaration*
	  EOF
	;

usingStatement
	: 'using' namespaceName ';'
	;

identifier
	: Identifier
	| Operator
	| EscapedIdentifier
	;

namespaceName
	: identifier ('.' identifier)*
	;

namespaceMemberDeclaration
	: namespaceDeclaration
	| typeDeclaration
	| globalDeclaration
	;

namespaceDeclaration
	: 'namespace' namespaceName '{' usingStatement* namespaceMemberDeclaration* '}'
	;

typeDeclaration
	: attribute* accessModifier typeModifier* 'class' identifier typeParameterList? typeBase? typeParameterConstraintClause* '{' typeMember* '}'
	;

globalDeclaration
	: attribute* accessModifier fieldModifier* 'var' identifier (':' type)? ('=' expression)? ';'
	| attribute* accessModifier fieldModifier* 'let' identifier (':' type)? '=' expression ';'
	;

attribute
	: EscapedIdentifier ('(' ')')?
	;

accessModifier
	: 'public'
	| 'private'
	| 'protected'
	| 'package'
	;

typeModifier
	: 'safe'
	| 'unsafe'
	| 'abstract'
	;

typeParameterList
	: '<' typeParameter (',' typeParameter)* '>'
	;

typeParameter
	: identifier '...'? (':' typeName)?
	;

typeBase
	: ':' baseType=typeName? (':' interfaces+=typeName (',' interfaces+=typeName)*)?
	;

typeName
	: identifier typeArguments? ('.' identifier typeArguments?)*
	;

typeArguments
	: '<' type (',' type)* '>'
	;

type
	: typeName
	| type '*'
	| type '[' constExpression (',' constExpression)* ']'
	| 'mut' type
	| 'own' type
	| 'ref' type
	| funcTypeParameterList '=>' type
	;

funcTypeParameterList
	: '(' funcTypeParameter (',' funcTypeParameter)* ')'
	| '(' ')'
	;

funcTypeParameter
	: parameterModifier* type
	;


constExpression
	: IntLiteral
	| StringLiteral
	| identifier
	;

typeParameterConstraintClause
	: 'where' typeParameter ':' typeParameterConstraint (',' typeParameterConstraint)*
	| 'where' typeParameter ('>='|'<='|'<'|'>') IntLiteral
	;

typeParameterConstraint
	: 'new' '(' ')'			#ConstructorConstraint
	| typeName				#TypeConstraint
	| typeParameter			#TypeListParameterConstraint // will only be hit for type lists (i.e. "foo...")
	;

typeMember
	: constructor
	| destructor
	| method
	| operatorOverload
	| conversionMethod
	| field
	| property
	;

parameterList
	: '(' parameter (',' parameter)* ')'
	| '(' ')'
	;

parameter
	: parameterModifier* identifier? ':' type
	| parameterModifier* 'this'
	;

parameterModifier
	: 'mut'
	| 'own'
	| 'params'
	;

constructor
	: attribute* accessModifier methodModifier* 'new' identifier? parameterList constructorInitializer? methodBody
	;

constructorInitializer
	: ':' 'base' '(' argumentList ')'
	| ':' 'this' '(' argumentList ')'
	;

argumentList
	:  expression (',' expression)*
	|
	;

destructor
	: attribute* accessModifier methodModifier* 'delete' parameterList methodBody
	;

method
	: attribute* accessModifier methodModifier* identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
	;

methodModifier
	: 'safe'
	| 'unsafe'
	| 'implicit'
	| 'explicit'
	| 'abstract'
	;

operatorOverload
	: attribute* accessModifier methodModifier* 'operator' overloadableOperator parameterList '=>' type methodBody
	;

conversionMethod
	: attribute* accessModifier methodModifier* 'conversion' typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
	;

property
	: attribute* accessModifier methodModifier* ('get'|'set') identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
	;

methodBody
	: '{' statement* '}'
	| ';'
	;

overloadableOperator
	: '*'
	| '&'
	| 'or'
	| 'and'
	| 'xor'
	| '?'
	| '??'
	| '.'
	| '[' ']'
	;

field
	: attribute* accessModifier fieldModifier* identifier (':' type)? ('=' expression)? ';'
	;

fieldModifier
	: 'unsafe'
	| 'safe'
	| 'static'
	| 'let'
	| 'const'
	;

statement
	: variableDeclaration ';'								#VariableDeclarationStatement
	| letDeclaration ';'	 								#LetDeclarationStatement
	| 'unsafe' '{' statement* '}'							#UnsafeBlockStatement
	| '{' statement* '}'									#Block
	| ';'													#EmptyStatement
	| expression ';'										#ExpressionStatement
	| 'return' expression ';'								#ReturnStatement
	| 'throw' expression ';'								#ThrowStatement
	| 'if' '(' expression ')' statement ('else' statement)?	#IfStatement
	| 'for' '(' (variableDeclaration|letDeclaration)? ';' expression? ';' expression? ')' statement #ForStatement
	| 'foreach' '(' (variableDeclaration|letDeclaration) 'in' expression ')' statement #ForeachStatement
	| 'delete' expression ';'								#DeleteStatement
	;

variableDeclaration
	: 'var' identifier (':' type)? ('=' expression)?
	;

letDeclaration
	: 'let' identifier (':' type)? '=' expression
	;

expression
	: expression '.' identifier
	| expression '->' identifier
	| expression '(' argumentList ')'
	| expression '[' argumentList ']'
	| expression '?'
	| ('+'|'-'|'not'|'++'|'--'|'&'|'*') expression
	| expression ('*'|'/') expression
	| expression ('+'|'-') expression
	| expression ('<' '<' | '>' '>') expression // TODO need to check no spaces between
	| expression ('<'|'<='|'>'|'>=') expression
	| expression ('=='|'<>') expression
	| expression 'and' expression
	| expression 'xor' expression
	| expression 'or' expression
	| expression '??' expression
	| <assoc=right> expression '?' expression ':' expression
	| <assoc=right> expression ('='|'*='|'/='|'+='|'-='|'<<='|'>>='|'and='|'xor='|'or=') expression
	| identifier
	| 'new' typeName('.' identifier)? '(' argumentList ')'
	| 'null'
	| 'this'
	| BooleanLiteral
	| IntLiteral
	| 'uninitialized'
	| StringLiteral
	;