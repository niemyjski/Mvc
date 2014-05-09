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
using Microsoft.AspNet.Mvc.Razor.Host;
using Microsoft.AspNet.Razor.Generator.Compiler;

namespace Microsoft.AspNet.Mvc.Razor
{
    public class InjectChunk : Chunk
    {
        /// <summary>
        /// Represents the chunk for an @inject statement.
        /// </summary>
        /// <param name="typeName">The type of object that would be injected</param>
        /// <param name="propertyName">The member name the field is exposed to the page as.</param>
        public InjectChunk(string typeName,
                           string propertyName)
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
            MemberName = propertyName;
        }

        public string TypeName { get; private set; }

        public string MemberName { get; private set; }
    }
}