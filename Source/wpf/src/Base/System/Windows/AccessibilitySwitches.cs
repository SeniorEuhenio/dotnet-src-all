//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
// File: AccessibilitySwitches.cs
//---------------------------------------------------------------------------

using MS.Internal.WindowsBase;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace System.Windows
{
    /// <summary>
    /// Wraps all accessibility logic for switches of the form:
    ///     Switch.UseLegacyAccessibilitySwitch[.N]
    /// This includes default initialization, verification of combinations, and switch values.
    /// </summary>
    internal static class AccessibilitySwitches
    {
        #region Constants

        /// <summary>
        /// This id is used by .NET to report a fatal error.
        /// </summary>
        const int EventId = 1023;

        /// <summary>
        /// This source is used by .NET to report events.
        /// </summary>
        const string EventSource = ".NET Runtime";

        #endregion

        #region Fields

        /// <summary>
        /// Guards against multiple definitions of default switch values.
        /// </summary>
        static int s_DefaultsSet = 0;

        /// <summary>
        /// Guards against multiple verifications of the switch values.
        /// </summary>
        static int s_SwitchesVerified = 0;

        #endregion

        #region Switch Definitions

        #region UseLegacyAccessibilityFeatures

        // Switch to disable new Accessibility features that may affect compat.
        internal const string UseLegacyAccessibilityFeaturesSwitchName = "Switch.UseLegacyAccessibilityFeatures";
        private static int _useLegacyAccessibilityFeatures;
        public static bool UseLegacyAccessibilityFeatures
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(UseLegacyAccessibilityFeaturesSwitchName, ref _useLegacyAccessibilityFeatures);
            }
        }

        #endregion

        #region UseLegacyAccessibilityFeatures2

        // Switch to disable new Accessibility features that may affect compat.
        internal const string UseLegacyAccessibilityFeatures2SwitchName = "Switch.UseLegacyAccessibilityFeatures.2";
        private static int _useLegacyAccessibilityFeatures2;
        public static bool UseLegacyAccessibilityFeatures2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(UseLegacyAccessibilityFeatures2SwitchName, ref _useLegacyAccessibilityFeatures2);
            }
        }

        #endregion

        #endregion

        #region Switch Functions

        /// <summary>
        /// Sets the defaults for all accessibility AppContext switches.
        /// </summary>
        /// <remarks>
        /// Only call this method from within AppContextDefaultValues.PopulateDefaultValuesPartial.
        /// This ensures defaults are set only under the lock from AppContextDefaultValues.
        /// </remarks>
        /// <param name="targetFrameworkVersion"></param>
        internal static void SetSwitchDefaults(int targetFrameworkVersion)
        {
            if(Interlocked.CompareExchange(ref s_DefaultsSet, 1, 0) == 0)
            {
                if (targetFrameworkVersion <= 40700)
                {
                    LocalAppContext.DefineSwitchDefault(UseLegacyAccessibilityFeaturesSwitchName, true);
                }

                if (targetFrameworkVersion <= 40701)
                {
                    LocalAppContext.DefineSwitchDefault(UseLegacyAccessibilityFeatures2SwitchName, true);
                }
            }
        }

        /// <summary>
        /// Verifies that the appropriate switch combinations are set.
        /// Otherwise, this throws an exception to inform the developer to set the appropriate switches.
        /// </summary>
        /// <remarks>
        /// Valid switch combinations are:
        ///     When switch Switch.UseLegacyAccessibilityFeatures.N is set to false:
        ///         Switch.UseLegacyAccessibilityFeatures must be false
        ///         Switch.UseLegacyAccessibilityFeatures.M must be false where M < N
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void VerifySwitches(Dispatcher dispatcher)
        {
            if (Interlocked.CompareExchange(ref s_SwitchesVerified, 1, 0) == 0)
            {
                // If a flag is set to false, we also must ensure the prior accessibility switch is also false.
                // Otherwise we should inform the developer, via an exception, to enable all the flags.
                if (UseLegacyAccessibilityFeatures && !UseLegacyAccessibilityFeatures2)
                {
                    // Dispatch an EventLog and error throw so we get loaded UI, then the crash.
                    // This ensures the WER dialog shows.
                    dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                        new Action(() =>
                        {
                            var exception = new NotSupportedException(SR.Get(SRID.CombinationOfAccessibilitySwitchesNotSupported));

                            if (EventLog.SourceExists(EventSource))
                            {
                                EventLog.WriteEntry(EventSource, 
                                    $"{Process.GetCurrentProcess().ProcessName}{Environment.NewLine}{exception.ToString()}",
                                    EventLogEntryType.Error, EventId);
                            }

                            throw exception;
                        }));
                }
            }
        }

        #endregion
    }
}
