parser grammar AdamantParser;

options
{
	language=CSharp;
	tokenVocab=AdamantLexer;
}

compilationUnit
	: usingStatement*
	  // globalAttribute*
	  declaration*
	  EOF
	;

usingStatement
	: 'using' namespaceName ';'
	;

identifier
	: Identifier
	| EscapedIdentifier
	| 'conversion'
	;

namespaceName
	: identifier ('.' identifier)*
	;

declaration
	: 'namespace' namespaceName '{' usingStatement* declaration* '}'  #NamespaceDeclaration
	| attribute* modifier* 'class' name=identifier typeParameterList? baseTypes?
		typeParameterConstraintClause*
		'{' member* '}' #ClassDeclaration
	| attribute* modifier* kind=('var'|'let') name=identifier (':' type)? ('=' expression)? ';' #GlobalDeclaration
	;

attribute
	: EscapedIdentifier ('(' ')')?
	;

baseTypes
	: (':' baseType=typeName? (':' interfaces+=typeName (',' interfaces+=typeName)*)?)
	;

modifier
	: Symbol='public'
	| Symbol='private'
	| Symbol='protected'
	| Symbol='package'
	| Symbol='safe'
	| Symbol='unsafe'
	| Symbol='abstract'
	| Symbol='partial'
	| Symbol='implicit'
	| Symbol='explicit'
	| Symbol='sealed'
	| Symbol='override'
	| Symbol='const'
	;

typeParameterList
	: '<' typeParameter (',' typeParameter)* '>'
	;

typeParameter
	: identifier '...'? (':' typeName)?
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

member
	: attribute* modifier* 'new' identifier? parameterList constructorInitializer? methodBody												#Constructor
	| attribute* modifier* 'delete' parameterList methodBody																				#Destructor
	| attribute* modifier* 'conversion' typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody					#ConversionMethod
	| attribute* modifier* 'operator' overloadableOperator parameterList '=>' type methodBody												#OperatorOverloadMethod
	| attribute* modifier* kind=('var'|'let') mutable='mut'? identifier (':' type)? ('=' expression)? ';'											#Field
	| attribute* modifier* kind=('get'|'set') identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody	#Property
	| attribute* modifier* identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody						#Method
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

constructorInitializer
	: ':' 'base' '(' argumentList ')'
	| ':' 'this' '(' argumentList ')'
	;

argumentList
	:  expression (',' expression)*
	|
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
	| 'for' '(' (variableDeclaration|letDeclaration)? ';' expression? ';' expression? ')' statement		#ForStatement
	| 'foreach' '(' (variableDeclaration|letDeclaration) 'in' expression ')' statement					#ForeachStatement
	| 'delete' expression ';'								#DeleteStatement
	;

variableDeclaration
	: 'var' identifier (':' type)? ('=' expression)?
	;

letDeclaration
	: 'let' identifier (':' type)? '=' expression
	;

expression
	: expression '.' identifier								#MemberExpression
	| expression '->' identifier							#PointerMemberExpression
	| expression '(' argumentList ')'						#CallExpression
	| expression '[' argumentList ']'						#ArrayAccessExpression
	| expression '?'										#NullCheckExpression
	| op=('+'|'-'|'not'|'++'|'--'|'&'|'*') expression		#UnaryExpression
	| expression op=('*'|'/') expression					#MultiplicativeExpression
	| expression op=('+'|'-') expression					#AdditiveExpression
	| expression (ops+='<' ops+='<' | ops+='>' ops+='>') expression #ShiftExpression
	| expression op=('<'|'<='|'>'|'>=') expression			#ComparativeExpression
	| expression op=('=='|'<>') expression					#EqualityExpression
	| expression 'and' expression							#AndExpression
	| expression 'xor' expression							#XorExpression
	| expression 'or' expression							#OrExpression
	| expression '??' expression							#CoalesceExpression
	| <assoc=right> expression '?' expression ':' expression #IfExpression
	| <assoc=right> expression op=('='|'*='|'/='|'+='|'-='|'<<='|'>>='|'and='|'xor='|'or=') expression #AssignmentExpression
	| identifier											#VariableExpression
	| 'new' typeName('.' identifier)? '(' argumentList ')'	#NewExpression
	| 'new' baseTypes? '(' argumentList ')' '{' member* '}'	#NewObjectExpression
	| 'null'												#NullLiteralExpression
	| 'this'												#ThisExpression
	| BooleanLiteral										#BooleanLiteralExpression
	| IntLiteral											#IntLiteralExpression
	| 'uninitialized'										#UninitializedExpression
	| StringLiteral											#StringLiteralExpression
	;