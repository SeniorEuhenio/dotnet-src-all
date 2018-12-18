using System;
using System.Collections.Generic;
using System.IO;
using System.Xaml;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Diagnostics;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using MS.Internal;
using System.Globalization;
using XamlReaderHelper = System.Windows.Markup.XamlReaderHelper;

namespace System.Windows.Baml2006
{
    // DDVSO 378607: This class exists to resolve an ambiguity between different versions of the same assembly when loading resource dictionaries.
    // We couldn't modify the original Baml2006Reader to fix this issue because it is public, instead we created this class, and use it when loading dictionaries.
    internal class Baml2006ReaderInternal : Baml2006Reader
    {
        #region Constructors

        internal Baml2006ReaderInternal(Stream stream,
            Baml2006SchemaContext schemaContext,
            Baml2006ReaderSettings settings) : base(stream, schemaContext, settings)
        {
        }
        
        internal Baml2006ReaderInternal(
            Stream stream,
            Baml2006SchemaContext baml2006SchemaContext,
            Baml2006ReaderSettings baml2006ReaderSettings,
            object root)
            : base(stream, baml2006SchemaContext, baml2006ReaderSettings, root)
        {
        }

        #endregion

        // Return the full assembly name, this includes the assembly version
        internal override string GetAssemblyNameForNamespace(Assembly asm)
        {
            return asm.FullName;
        }
    }
}
