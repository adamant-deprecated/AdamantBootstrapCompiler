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

namespaceName
	: Identifier ('.' Identifier)*
	;

namespaceMemberDeclaration
	: namespaceDeclaration
	//| typeDeclaration
	;

namespaceDeclaration
	: 'namespace' namespaceName '{' usingStatement* namespaceMemberDeclaration* '}'
	;
