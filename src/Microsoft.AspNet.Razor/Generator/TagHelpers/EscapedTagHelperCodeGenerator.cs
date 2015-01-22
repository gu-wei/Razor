// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.AspNet.Razor.Parser.SyntaxTree;

namespace Microsoft.AspNet.Razor.Generator
{
    /// <summary>
    /// A <see cref="BlockCodeGenerator"/> that is responsible for generating valid 
    /// <see cref="Compiler.LiteralChunk"/>s from escaped tag helpers.
    /// </summary>
    public class EscapedTagHelperCodeGenerator : SpanCodeGenerator
    {
        /// <summary>
        /// Creates a <see cref="Compiler.LiteralChunk"/> for the given <paramref name="target"/>.
        /// </summary>
        /// <param name="target">
        /// The <see cref="Span"/> responsible for this <see cref="EscapedTagHelperCodeGenerator"/>.
        /// </param>
        /// <param name="context">A <see cref="CodeGeneratorContext"/> instance that contains information about
        /// the current code generation process.</param>
        /// <remarks>Removes the first tag helper escape character '!' from the generated 
        /// <see cref="Compiler.LiteralChunk"/>'s <see cref="Compiler.LiteralChunk.Text"/>.</remarks>
        public override void GenerateCode(Span target, CodeGeneratorContext context)
        {
            var literalText = target.Content;
            var bangIndex = literalText.IndexOf('!');

            // The requirement for this code generator is for output to be escaped by '!'; therefore there should 
            // always be a '!' in the literalText.
            Debug.Assert(bangIndex != -1);

            literalText = literalText.Remove(bangIndex, 1);

            context.CodeTreeBuilder.AddLiteralChunk(literalText, target);
        }
    }
}