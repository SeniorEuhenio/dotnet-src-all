//------------------------------------------------------------------------------
// <copyright file="ControlCreateParseRecorder.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.Web.Compilation {
    using System;
    using System.CodeDom;
    using System.Web.UI;

    class WebObjectActivatorParseRecorder : ParseRecorder {
        public override void ProcessGeneratedCode(ControlBuilder builder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType,
            CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod) {
            // Use similar logic as PageInspector to locate the control create statement
            if (derivedType != null && typeof(Control).IsAssignableFrom(builder.ControlType) && buildMethod != null) {
                var codeAssignStatement = FindControlCreateStatement(builder.ControlType, buildMethod.Statements);
                if (codeAssignStatement != null) {
                    ReplaceControlCreateStatement(builder.ControlType, codeAssignStatement, buildMethod.Statements);
                }
            }

            base.ProcessGeneratedCode(builder, codeCompileUnit, baseType, derivedType, buildMethod, dataBindingMethod);
        }

        private static CodeAssignStatement FindControlCreateStatement(Type controlType, CodeStatementCollection statements) {
            foreach (var statement in statements) {
                var codeAssignStatement = statement as CodeAssignStatement;
                if (codeAssignStatement != null) {
                    var objCreateExpr = codeAssignStatement.Right as CodeObjectCreateExpression;

                    if (objCreateExpr != null && objCreateExpr.CreateType.BaseType == controlType.ToString() && objCreateExpr.Parameters.Count == 0
                        && codeAssignStatement.Left is CodeVariableReferenceExpression) {
                        return codeAssignStatement;
                    }
                }
            }
            return null;
        }

        private static void ReplaceControlCreateStatement(Type ctrlType, CodeAssignStatement objAssignStatement, CodeStatementCollection statements) {
            /* Generate code like below
             
            IServiceProvider __activator = HttpRuntime.WebObjectActivator;

            if (activator != null) {
                _ctrl = (ctrlType)activator.GetService(ctrlType);
            }

            // if default c-tor exists
            else {
                _ctrl = new ....
            }
            // if no default c-tor
            else {
                _ctrl = null
            }

            */
            var webObjectActivatorExpr = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression("System.Web.HttpRuntime"), "WebObjectActivator");
            var activatorRefExpr = new CodeVariableReferenceExpression("__activator");            

            var getServiceExpr = new CodeMethodInvokeExpression(webObjectActivatorExpr, "GetService", new CodeTypeOfExpression(ctrlType));
            var castExpr = new CodeCastExpression(new CodeTypeReference(ctrlType), getServiceExpr);
            var createObjectStatement = new CodeConditionStatement() {
                Condition = new CodeBinaryOperatorExpression(activatorRefExpr,
                                                             CodeBinaryOperatorType.IdentityInequality,
                                                             new CodePrimitiveExpression(null))
            };
            createObjectStatement.TrueStatements.Add(new CodeAssignStatement(objAssignStatement.Left, castExpr));

            // If default c-tor exists
            if (ctrlType.GetConstructor(Type.EmptyTypes) != null) {
                createObjectStatement.FalseStatements.Add(objAssignStatement);
            }
            else {
                createObjectStatement.FalseStatements.Add(new CodeAssignStatement(objAssignStatement.Left, new CodePrimitiveExpression(null)));
            }

            // replace the old assign statement
            var indexOfStatement = statements.IndexOf(objAssignStatement);
            statements.Insert(indexOfStatement, createObjectStatement);
            statements.Insert(indexOfStatement, new CodeAssignStatement(activatorRefExpr, webObjectActivatorExpr));
            statements.Insert(indexOfStatement, new CodeVariableDeclarationStatement(typeof(IServiceProvider), "__activator"));
            statements.Remove(objAssignStatement);
        }
    }
}
