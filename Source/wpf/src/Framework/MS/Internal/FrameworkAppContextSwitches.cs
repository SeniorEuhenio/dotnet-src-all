//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
// File: FrameworkAppContextSwitches.cs
//---------------------------------------------------------------------------

using MS.Internal.PresentationFramework.Interop;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MS.Internal
{
    // There are cases where we have multiple assemblies that are going to import this file and
    // if they are going to also have InternalsVisibleTo between them, there will be a compiler warning
    // that the type is found both in the source and in a referenced assembly. The compiler will prefer
    // the version of the type defined in the source
    //
    // In order to disable the warning for this type we are disabling this warning for this entire file.
    #pragma warning disable 436

    internal static class FrameworkAppContextSwitches
    {
        internal const string DoNotApplyLayoutRoundingToMarginsAndBorderThicknessSwitchName = "Switch.MS.Internal.DoNotApplyLayoutRoundingToMarginsAndBorderThickness";
        private static int _doNotApplyLayoutRoundingToMarginsAndBorderThickness;
        public static bool DoNotApplyLayoutRoundingToMarginsAndBorderThickness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DoNotApplyLayoutRoundingToMarginsAndBorderThicknessSwitchName, ref _doNotApplyLayoutRoundingToMarginsAndBorderThickness);
            }
        }

        internal const string GridStarDefinitionsCanExceedAvailableSpaceSwitchName = "Switch.System.Windows.Controls.Grid.StarDefinitionsCanExceedAvailableSpace";
        private static int _gridStarDefinitionsCanExceedAvailableSpace;
        public static bool GridStarDefinitionsCanExceedAvailableSpace
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(GridStarDefinitionsCanExceedAvailableSpaceSwitchName, ref _gridStarDefinitionsCanExceedAvailableSpace);
            }
        }

        internal const string SelectionPropertiesCanLagBehindSelectionChangedEventSwitchName = "Switch.System.Windows.Controls.TabControl.SelectionPropertiesCanLagBehindSelectionChangedEvent";
        private static int _selectionPropertiesCanLagBehindSelectionChangedEvent;
        public static bool SelectionPropertiesCanLagBehindSelectionChangedEvent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(SelectionPropertiesCanLagBehindSelectionChangedEventSwitchName, ref _selectionPropertiesCanLagBehindSelectionChangedEvent);
            }
        }

        internal const string DoNotUseFollowParentWhenBindingToADODataRelationSwitchName = "Switch.System.Windows.Data.DoNotUseFollowParentWhenBindingToADODataRelation";
        private static int _doNotUseFollowParentWhenBindingToADODataRelation;
        public static bool DoNotUseFollowParentWhenBindingToADODataRelation
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DoNotUseFollowParentWhenBindingToADODataRelationSwitchName, ref _doNotUseFollowParentWhenBindingToADODataRelation);
            }
        }

        #region Accessibility Switches

        #region UseLegacyAccessibilityFeatures

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

        #endregion

        // DDVSO:405199
        // Switch to enable non-adorner based rendering of TextSelection in TextBox and PasswordBox.
        internal const string UseAdornerForTextboxSelectionRenderingSwitchName = "Switch.System.Windows.Controls.Text.UseAdornerForTextboxSelectionRendering";
        private static int _useAdornerForTextboxSelectionRendering;
        public static bool UseAdornerForTextboxSelectionRendering
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(UseAdornerForTextboxSelectionRenderingSwitchName, ref _useAdornerForTextboxSelectionRendering);
            }
        }

        // DDVSO:546550
        // Switch to enable appending the local assembly version to the Uri being set for ResourceDictionary.Source via Baml2006ReaderInternal.
        internal const string AppendLocalAssemblyVersionForSourceUriSwitchName = "Switch.System.Windows.Baml2006.AppendLocalAssemblyVersionForSourceUri";
        private static int _AppendLocalAssemblyVersionForSourceUriSwitchName;
        public static bool AppendLocalAssemblyVersionForSourceUri
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(AppendLocalAssemblyVersionForSourceUriSwitchName, ref _AppendLocalAssemblyVersionForSourceUriSwitchName);
            }
        }
    }

#pragma warning restore 436
}
