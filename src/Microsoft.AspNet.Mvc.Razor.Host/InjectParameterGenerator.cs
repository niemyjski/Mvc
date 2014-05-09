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
using System.Globalization;
using Microsoft.AspNet.Mvc.Razor.Host;
using Microsoft.AspNet.Razor.Generator;
using Microsoft.AspNet.Razor.Parser.SyntaxTree;

namespace Microsoft.AspNet.Mvc.Razor
{
    public class InjectParameterGenerator : SpanCodeGenerator
    {
        public InjectParameterGenerator(string typeName, string propertyName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, "typeName");
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, "propertyName");
            }

            TypeName = typeName;
            PropertyName = propertyName;
        }

        public string TypeName { get; private set; }
        
        public string PropertyName { get; private set; }

        public override void GenerateCode(Span target, CodeGeneratorContext context)
        {
            context.CodeTreeBuilder.AddChunk(new InjectChunk(TypeName, PropertyName), target);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "@inject {0} {1}", TypeName, PropertyName);
        }

        public override bool Equals(object obj)
        {
            var other = obj as InjectParameterGenerator;
            return other != null &&
                   string.Equals(TypeName, other.TypeName, StringComparison.Ordinal) &&
                   string.Equals(PropertyName, other.TypeName, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return TypeName.GetHashCode() + 
                   PropertyName.GetHashCode() * 13;
        }
    }
}