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
	| attribute* modifier* kind=('var'|'let') name=identifier (':' ownershipType)? ('=' expression)? ';' #GlobalDeclaration
	| attribute* modifier* name=identifier typeArguments? parameterList '=>' returnType=ownershipType typeParameterConstraintClause* methodBody	 #FunctionDeclaration
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
	| Symbol='async'
	;

typeParameterList
	: '<' typeParameter (',' typeParameter)* '>'
	;

typeParameter
	: identifier '...'? (':' typeName)?
	;

typeName
	: outerType=typeName '.' identifier typeArguments?
	| identifier typeArguments?
	;

typeArguments
	: '<' ownershipType (',' ownershipType)* '>'
	;

ownershipType // these are types with ownership modifiers
	: ref='ref'? 'mut' plainType	#MutableType
	| ref='ref'? 'own' plainType	#OwnedType
	| ref='ref'? 'immut' plainType	#ImmutableType
	| ref='ref'? plainType			#ImplicitType
	;

plainType
	: typeName													#NamedType
	| plainType '*'												#PointerType
	| plainType '[' constExpression (',' constExpression)* ']'	#ArrayType
	| plainType '[' ','* ']'									#ArraySliceType
	| funcTypeParameterList '=>' ownershipType					#FunctionType
	;

funcTypeParameterList
	: '(' funcTypeParameter (',' funcTypeParameter)* ')'
	| '(' ')'
	;

funcTypeParameter
	: parameterModifier* ownershipType
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
	: attribute* modifier* 'new' identifier? parameterList constructorInitializer? methodBody													#Constructor
	| attribute* modifier* 'delete' parameterList methodBody																					#Destructor
	| attribute* modifier* 'conversion' typeArguments? parameterList '=>' ownershipType typeParameterConstraintClause* methodBody					#ConversionMethod
	| attribute* modifier* 'operator' overloadableOperator parameterList '=>' ownershipType methodBody												#OperatorOverloadMethod
	| attribute* modifier* kind=('var'|'let') identifier (':' ownershipType)? ('=' expression)? ';'													#Field
	| attribute* modifier* kind=('get'|'set') identifier typeArguments? parameterList '=>' ownershipType typeParameterConstraintClause* methodBody	#Property
	| attribute* modifier* identifier typeArguments? parameterList '=>' returnType=ownershipType typeParameterConstraintClause* methodBody						#Method
	;

parameterList
	: '(' parameters+=parameter (',' parameters+=parameter)* ')'
	| '(' ')'
	;

parameter
	: parameterModifier* identifier? ':' ownershipType
	| parameterModifier* 'this' (':' 'mut')?
	;

parameterModifier
	: 'params'
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
	| 'unsafe' '{' statement* '}'							#UnsafeBlockStatement
	| '{' statement* '}'									#Block
	| ';'													#EmptyStatement
	| expression ';'										#ExpressionStatement
	| 'return' expression ';'								#ReturnStatement
	| 'throw' expression ';'								#ThrowStatement
	| 'if' '(' expression ')' statement ('else' statement)?	#IfStatement
	| 'for' '(' variableDeclaration? ';' expression? ';' expression? ')' statement		#ForStatement
	| 'foreach' '(' variableDeclaration 'in' expression ')' statement					#ForeachStatement
	| 'delete' expression ';'								#DeleteStatement
	;

variableDeclaration
	: kind=('var'|'let') identifier (':' ownershipType)? ('=' expression)?
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