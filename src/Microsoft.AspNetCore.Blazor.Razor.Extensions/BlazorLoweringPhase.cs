﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System;

namespace Microsoft.AspNetCore.Blazor.Razor
{
    /// <summary>
    /// A <see cref="RazorEngine"/> phase that builds the C# document corresponding to
    /// a <see cref="RazorCodeDocument"/> for a Blazor component.
    /// </summary>
    internal class BlazorLoweringPhase : IRazorCSharpLoweringPhase
    {
        public RazorEngine Engine { get; set; }

        public void Execute(RazorCodeDocument codeDocument)
        {
            var documentNode = codeDocument.GetDocumentIntermediateNode();
            var writer = BlazorComponentDocumentWriter.CreateWriter(((BlazorCodeTarget)documentNode.Target), documentNode.Options);
            var csharpDoc = writer.WriteDocument(codeDocument, documentNode);
            codeDocument.SetCSharpDocument(csharpDoc);
        }

        /// <summary>
        /// Creates <see cref="DocumentWriter"/> instances that are configured to use
        /// <see cref="BlazorCodeTarget"/>.
        /// </summary>
        private class BlazorComponentDocumentWriter : DocumentWriter
        {
            public static DocumentWriter CreateWriter(BlazorCodeTarget codeTarget, RazorCodeGenerationOptions options)
                => Instance.Create(codeTarget, options);

            private static BlazorComponentDocumentWriter Instance
                = new BlazorComponentDocumentWriter();

            public override RazorCSharpDocument WriteDocument(
                RazorCodeDocument codeDocument, DocumentIntermediateNode documentNode)
                => throw new NotImplementedException();
        }
    }
}