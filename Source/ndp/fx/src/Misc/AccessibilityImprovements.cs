//------------------------------------------------------------------------------
// <copyright file="AccessibilityImprovements.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System
{
    using System;
#if DRAWINGDESIGN_NAMESPACE
    using System.Drawing.Design;
#elif Microsoft_NAMESPACE
    using System.Windows.Forms;
#endif
    using System.Runtime.CompilerServices;
    
    /// <summary>
    /// This class maps accessibility quirks set in the app.config file or based on the target framework moniker 
    /// to accessibility levels that are referenced in the source code.
    /// </summary>
    internal static class AccessibilityImprovements
    {
        private static bool levelsValidated;
        private static int  useLegacyAccessibilityFeatures;
        private static int  useLegacyAccessibilityFeatures2;

        // We assume that accessibility quirks have the same names in different assemblies.
        internal const string UseLegacyAccessibilityFeaturesSwitchName = @"Switch.UseLegacyAccessibilityFeatures";
        internal const string UseLegacyAccessibilityFeatures2SwitchName = @"Switch.UseLegacyAccessibilityFeatures.2";

        /// <summary>
        /// False - opt-out of the accessibility improvements introduced in 4.7.1.
        /// This is the case of application that target .NET frameworks below 4.7.1 or 
        /// applications that target 4.7.1 and above and have the following  
        /// <AppContextSwitchOverrides value="Switch.UseLegacyAccessibilityFeatures=true" />
        /// in the <runtime> section in the app.config file.
        /// 
        /// True - opt-in into the accessibility improvements introduced in 4.7.1.
        /// </summary>
        internal static bool Level1 
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (useLegacyAccessibilityFeatures < 0) return true;
                if (useLegacyAccessibilityFeatures > 0) return false;
                ValidateLevels();

                return (useLegacyAccessibilityFeatures < 0);
            }
        }

        /// <summary>
        /// Opt in or out of the accessibility improvements introduced in .NET Framework versions 4.7.2 and 4.7.1.
        /// </summary>
        internal static bool Level2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (useLegacyAccessibilityFeatures2 < 0) return true;
                if (useLegacyAccessibilityFeatures2 > 0) return false;
                ValidateLevels();

                return (useLegacyAccessibilityFeatures2 < 0);
            }
        }

        internal static void ValidateLevels()
        {
            if (levelsValidated)
            {
                return;
            }

            bool level1 = !LocalAppContext.GetCachedSwitchValue(UseLegacyAccessibilityFeaturesSwitchName, ref useLegacyAccessibilityFeatures);
            bool level2 = !LocalAppContext.GetCachedSwitchValue(UseLegacyAccessibilityFeatures2SwitchName, ref useLegacyAccessibilityFeatures2);

            // 4.7.2 accessibility improvements are building upon the infrastructure introduced in 4.7.1,
            // thus the application has to opt-in into 4.7.1 level in order to get the 4.7.2 level of support. 
            if (!level1 && level2)
            {
                throw new NotSupportedException(SR.GetString(SR.CombinationOfAccessibilitySwitchesNotSupported));
            }

            // If this code is executed concurrently, in the worst case we'll throw the same exception on each thread.
            levelsValidated = true;
        }
    }
}
