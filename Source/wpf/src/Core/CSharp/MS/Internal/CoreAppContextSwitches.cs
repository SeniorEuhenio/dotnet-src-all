//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
// File: CoreAppContextSwitches.cs
//---------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MS.Internal
{
    // WPF's builds are seeing new warnings as as result of using LocalAppContext in PresentationFramework, PresentationCore and WindowsBase.
    // These binaries have internalsVisibleTo attribute set between them - which results in the warning.
    // We don't have a way of suppressing this warning effectively until the shared copies of LocalAppContext and
    // AppContextDefaultValues have pragmas added to suppress warning 436
#pragma warning disable 436
    internal static class CoreAppContextSwitches
    {
        #region DoNotScaleForDpiChanges

        internal const string DoNotScaleForDpiChangesSwitchName = "Switch.System.Windows.DoNotScaleForDpiChanges";
        private static int _doNotScaleForDpiChanges;
        public static bool DoNotScaleForDpiChanges
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DoNotScaleForDpiChangesSwitchName, ref _doNotScaleForDpiChanges);
            }
        }

        #endregion

        #region DisableStylusAndTouchSupport

        // DDVSO:221075
        // Switch to disable WPF Stylus and Touch Support
        internal const string DisableStylusAndTouchSupportSwitchName = "Switch.System.Windows.Input.Stylus.DisableStylusAndTouchSupport";
        private static int _disableStylusAndTouchSupport;
        public static bool DisableStylusAndTouchSupport
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DisableStylusAndTouchSupportSwitchName, ref _disableStylusAndTouchSupport);
            }
        }

        #endregion

        #region EnablePointerSupport

        // DDVSO:197685
        // Switch to enable WPF support for the WM_POINTER based stylus/touch stack
        internal const string EnablePointerSupportSwitchName = "Switch.System.Windows.Input.Stylus.EnablePointerSupport";
        private static int _enablePointerSupport;
        public static bool EnablePointerSupport
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(EnablePointerSupportSwitchName, ref _enablePointerSupport);
            }
        }

        #endregion

        #region OverrideExceptionWithNullReferenceException

        // DDVSO:223209
        // Switch to enable the correct exception being thrown in ImageSourceConverter.ConvertFrom instead of NullReferenceException
        internal const string OverrideExceptionWithNullReferenceExceptionName = "Switch.System.Windows.Media.ImageSourceConverter.OverrideExceptionWithNullReferenceException";
        private static int _overrideExceptionWithNullReferenceException;
        public static bool OverrideExceptionWithNullReferenceException
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(OverrideExceptionWithNullReferenceExceptionName, ref _overrideExceptionWithNullReferenceException);
            }
        }

        #endregion

        #region DisableDiagnostics

        // DDVSO:406811
        // Switch to disable diagnostic features
        internal const string DisableDiagnosticsSwitchName = "Switch.System.Windows.Diagnostics.DisableDiagnostics";
        private static int _disableDiagnostics;
        public static bool DisableDiagnostics
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DisableDiagnosticsSwitchName, ref _disableDiagnostics);
            }
        }

        #endregion

        #region AllowVisualTreeChangesDuringVisualTreeChanged

        // DDVSO:406811
        // Switch to allow changes during a VisualTreeChanged event
        internal const string AllowChangesDuringVisualTreeChangedSwitchName = "Switch.System.Windows.Diagnostics.AllowChangesDuringVisualTreeChanged";
        private static int _allowChangesDuringVisualTreeChanged;
        public static bool AllowChangesDuringVisualTreeChanged
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(AllowChangesDuringVisualTreeChangedSwitchName, ref _allowChangesDuringVisualTreeChanged);
            }
        }

        #endregion

        #region DisableImplicitTouchKeyboardInvocation

        // DDVSO:362756
        // Switch to disable automatic touch keyboard invocation on focus of a control.
        internal const string DisableImplicitTouchKeyboardInvocationSwitchName = "Switch.System.Windows.Input.Stylus.DisableImplicitTouchKeyboardInvocation";
        private static int _disableImplicitTouchKeyboardInvocation;
        public static bool DisableImplicitTouchKeyboardInvocation
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DisableImplicitTouchKeyboardInvocationSwitchName, ref _disableImplicitTouchKeyboardInvocation);
            }
        }

        #endregion

        #region UseLegacyAccessibilityFeatures

        // DDVSO:444529
        // Switch to disable new Accessibility features that may affect compat.
        public static bool UseLegacyAccessibilityFeatures
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return AccessibilitySwitches.UseLegacyAccessibilityFeatures;
            }
        }

        #endregion

        #region UseLegacyAccessibilityFeatures2

        // Switch to disable new Accessibility features that may affect compat.
        public static bool UseLegacyAccessibilityFeatures2
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return AccessibilitySwitches.UseLegacyAccessibilityFeatures2;
            }
        }
        
        #endregion

        #region ShouldRenderEvenWhenNoDisplayDevicesAreAvailable and ShouldNotRenderInNonInteractiveWindowStation

        #region ShouldRenderEvenWhenNoDisplayDevicesAreAvailable

        internal const string ShouldRenderEvenWhenNoDisplayDevicesAreAvailableSwitchName = "Switch.System.Windows.Media.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable";
        private static int _shouldRenderEvenWhenNoDisplayDevicesAreAvailable;

        /// <summary>
        /// See <see cref="System.Windows.Media.MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable"/> for details
        /// </summary>
        /// <remarks>
        ///     A registry value (HKLM only) can be set to force this value to be true.
        ///         Registry location: 
        ///             HKEY_LOCAL_MACHINE\Software\[Wow6432Node\]Microsoft\.NETFramework\AppContext
        ///         Value Name:
        ///             Switch.System.Windows.Media.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable
        ///         Type:
        ///             REG_SZ
        ///         Values:
        ///             "true"
        ///             "false"
        ///         Default:
        ///             "false"
        ///     ii. An element in the <![CDATA[<AppContextSwitchOverrides>]]> element of the application 
        ///     configuration file (App.Config) can be set as follows:
        ///     <![CDATA[
        ///         <configuration>
        ///             <runtime>
        ///                 <AppContextSwitchOverrides 
        ///                     value="Switch.Name.One=value1;Switch.Name.Two=value2;Switch.System.Windows.Media.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable=true" />
        ///             </runtime>
        ///         </configuration>
        ///     ]]>
        /// </remarks>
        public static bool ShouldRenderEvenWhenNoDisplayDevicesAreAvailable
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(ShouldRenderEvenWhenNoDisplayDevicesAreAvailableSwitchName, ref _shouldRenderEvenWhenNoDisplayDevicesAreAvailable);
            }
        }

        #endregion

        #region ShouldNotRenderInNonInteractiveWindowStation

        internal const string ShouldNotRenderInNonInteractiveWindowStationSwitchName = "Switch.System.Windows.Media.ShouldNotRenderInNonInteractiveWindowStation";
        private static int _shouldNotRenderInNonInteractiveWindowStation;

        /// <summary>
        /// See <see cref="System.Windows.Media.MediaContext.ShouldRenderEvenWhenNoDisplayDevicesAreAvailable"/> for details
        /// </summary>
        /// <remarks>
        ///     A registry value (HKLM only) can be set to force this value to be true.
        ///         Registry location: 
        ///             HKEY_LOCAL_MACHINE\Software\[Wow6432Node\]Microsoft\.NETFramework\AppContext
        ///         Value Name:
        ///             Switch.System.Windows.Media.ShouldNotRenderInNonInteractiveWindowStation
        ///         Type:
        ///             REG_SZ
        ///         Values:
        ///             "true"
        ///             "false"
        ///         Default:
        ///             "false"
        ///     ii. An element in the <![CDATA[<AppContextSwitchOverrides>]]> element of the application 
        ///     configuration file (App.Config) can be set as follows:
        ///     <![CDATA[
        ///         <configuration>
        ///             <runtime>
        ///                 <AppContextSwitchOverrides 
        ///                     value="Switch.Name.One=value1;Switch.Name.Two=value2;Switch.System.Windows.Media.ShouldNotRenderInNonInteractiveWindowStation=true" />
        ///             </runtime>
        ///         </configuration>
        ///     ]]>
        /// </remarks>
        public static bool ShouldNotRenderInNonInteractiveWindowStation
        {
            get
            {
                return LocalAppContext.GetCachedSwitchValue(ShouldNotRenderInNonInteractiveWindowStationSwitchName, ref _shouldNotRenderInNonInteractiveWindowStation);
            }
        }

        #endregion

        #endregion
    }
#pragma warning restore 436
}
