// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

using System.Linq;
using Microsoft.AspNet.Razor.Generator;
using Microsoft.AspNet.Razor.Generator.Compiler.CSharp;

namespace Microsoft.AspNet.Mvc.Razor
{
    public class MvcCSharpCodeBuilder : CSharpCodeBuilder
    {
        public MvcCSharpCodeBuilder(CodeGeneratorContext context)
            : base(context)
        {

        }

        protected override void BuildConstructor(CSharpCodeWriter writer)
        {
            writer.WriteLineHiddenDirective();
            var visitor = new MvcCSharpCodeVisitor(writer, Context);
 
             writer.WriteLine();
             visitor.Accept(Context.CodeTreeBuilder.CodeTree.Chunks);
             writer.WriteLine();
 
             writer.WriteLineHiddenDirective();

             var arguments = visitor.InjectChunks.ToDictionary(c => c.TypeName, c => c.MemberName);
             using (writer.BuildConstructor("public", Context.ClassName, arguments))
             {
                 foreach(var inject in visitor.InjectChunks)
                 {
                     writer.WriteStartAssignment("this." + inject.MemberName)
                           .Write(inject.MemberName)
                           .WriteLine(";");
                 }
             }
        }
    }
}