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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.Mvc.Razor.Host;
using Microsoft.AspNet.Razor.Generator;
using Microsoft.AspNet.Razor.Generator.Compiler;
using Microsoft.AspNet.Razor.Generator.Compiler.CSharp;

namespace Microsoft.AspNet.Mvc.Razor
{
    public class MvcCSharpCodeVisitor : CodeVisitor<CSharpCodeWriter>
    {
        private readonly List<InjectChunk> _injectChunks = new List<InjectChunk>();

        public MvcCSharpCodeVisitor(CSharpCodeWriter writer, CodeGeneratorContext context)
            : base(writer, context)
        {

        }

        public List<InjectChunk> InjectChunks
        {
            get { return _injectChunks; }
        }

        public override void Accept(Chunk chunk)
        {
            var injectChunk = chunk as InjectChunk;
            if (injectChunk != null)
            {
                Visit(injectChunk);
            }
            base.Accept(chunk);
        }

        protected virtual void Visit(InjectChunk chunk)
        {
            if (_injectChunks.Any(c => String.Equals(c.MemberName, chunk.MemberName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(Resources.FormatMvcRazorCodeParser_InjectParameterAlreadyRegistered(chunk.MemberName));
            }

            Writer.WriteLine(string.Format(CultureInfo.InvariantCulture,
                                           "public {0} {1} {{ get; private set; }}",
                                            chunk.TypeName,
                                            chunk.MemberName));
            _injectChunks.Add(chunk);
        }
    }
}