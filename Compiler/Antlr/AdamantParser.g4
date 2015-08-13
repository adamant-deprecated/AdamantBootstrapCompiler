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
	| attribute* modifier* 'class' identifier typeParameterList? typeBase? typeParameterConstraintClause* '{' typeMember* '}' #ClassDeclaration
	| attribute* modifier* kind=('var'|'let') identifier (':' type)? ('=' expression)? ';' #GlobalDeclaration
	;

attribute
	: EscapedIdentifier ('(' ')')?
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
	| Symbol='static'
	| Symbol='const'
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
	| conversionMethod
	| method
	| operatorOverload
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
	: attribute* modifier* 'new' identifier? parameterList constructorInitializer? methodBody
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
	: attribute* modifier* 'delete' parameterList methodBody
	;

method
	: attribute* modifier* identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
	;

operatorOverload
	: attribute* modifier* 'operator' overloadableOperator parameterList '=>' type methodBody
	;

conversionMethod
	: attribute* modifier* 'conversion' typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
	;

property
	: attribute* modifier* ('get'|'set') identifier typeArguments? parameterList '=>' type typeParameterConstraintClause* methodBody
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
	: attribute* modifier* 'var' identifier (':' type)? ('=' expression)? ';'
	| attribute* modifier* 'let' identifier (':' type)? '=' expression ';'
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
	| 'null'												#NullLiteralExpression
	| 'this'												#ThisExpression
	| BooleanLiteral										#BooleanLiteralExpression
	| IntLiteral											#IntLiteralExpression
	| 'uninitialized'										#UninitializedExpression
	| StringLiteral											#StringLiteralExpression
	;