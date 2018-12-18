/***************************************************************************\
*
* File: ResourceKey.cs
*
* Base class for various types of key objects to use in resource dictionaries.
*
* Copyright (C) 2005 by Microsoft Corporation.  All rights reserved.
*
\***************************************************************************/

using System;
using System.Reflection;
using System.Windows.Markup;

namespace System.Windows
{
    /// <summary>
    ///     Abstract base class for various resource keys.
    ///     Provides a common base for simple key detection in resource components.
    /// </summary>
    [MarkupExtensionReturnType(typeof(ResourceKey))]
    public abstract class ResourceKey : MarkupExtension
    {
        /// <summary>
        ///     Used to determine where to look for the resource dictionary that holds this resource.
        /// </summary>
        public abstract Assembly Assembly
        {
            get;
        }

        /// <summary>
        ///  Return this object.  ResourceKeys are typically used as a key in a dictionary.
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
