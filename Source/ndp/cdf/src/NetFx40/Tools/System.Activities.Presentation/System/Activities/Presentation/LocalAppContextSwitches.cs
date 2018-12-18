//------------------------------------------------------------------------------
// <copyright file="LocalAppContextSwitches.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Activities.Presentation
{
    using System.Runtime.CompilerServices;

    internal static class LocalAppContextSwitches
    {
        internal const string UseLegacyAccessibilityFeaturesSwitchName = @"Switch.UseLegacyAccessibilityFeatures";
        
        private static int useLegacyAccessibilityFeatures;

        public static bool UseLegacyAccessibilityFeatures
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(LocalAppContextSwitches.UseLegacyAccessibilityFeaturesSwitchName, ref useLegacyAccessibilityFeatures);
            }
        }
    }
}
