//---------------------------------------------------------------------------
//
// File: PresentationNativeLoader.cs
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
//
// Description: Shared code for loading PresentationNative_v0400.dll
//
//---------------------------------------------------------------------------

#if WINDOWS_BASE
namespace MS.Internal.WindowsBase
#elif PRESENTATION_CORE
namespace MS.Internal.PresentationCore
#elif PRESENTATIONFRAMEWORK
namespace MS.Internal.PresentationFramework
#elif REACHFRAMEWORK
namespace MS.Internal.ReachFramework
#elif UIAUTOMATIONCLIENT
namespace MS.Internal.UIAutomationClient
#elif UIAUTOMATIONCLIENTSIDEPROVIDERS
namespace MS.Internal.UIAutomationClientSideProviders
#elif UIAUTOMATIONTYPES
namespace MS.Internal.UIAutomationTypes
#elif WINDOWSFORMSINTEGRATION
namespace MS.Internal.WinFormsIntegration
#elif DRT
namespace MS.Internal.Drt
#else
namespace Microsoft.Internal.Interop
#endif
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Security;
    using System.Security.Permissions;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using MS.Win32;

    [SuppressUnmanagedCodeSecurity, SecurityCritical(SecurityCriticalScope.Everything)]
    internal static class PresentationNativeLoader
    {
        private const string PresentationNativeDll = "PresentationNative_v0400.dll";

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        private const string COMPLUS_Version = @"COMPLUS_Version";
        private const string COMPLUS_InstallRoot = @"COMPLUS_InstallRoot";
        private const string EnvironmentVariables = COMPLUS_Version + ";" + COMPLUS_InstallRoot;
        private const string FRAMEWORK_RegKey  = @"Software\Microsoft\Net Framework Setup\NDP\v4\Client\";
        private const string FRAMEWORK_RegKey_FullPath  = @"HKEY_LOCAL_MACHINE\" + FRAMEWORK_RegKey;
        private const string FRAMEWORK_InstallPath_RegValue = "InstallPath";
        private const string DOTNET_RegKey = @"Software\Microsoft\.NETFramework";
        private const string DOTNET_Install_RegValue = @"InstallRoot";
        private const string WPF_SUBDIR = @"WPF";

        // P/Invoke calls into PresentationNative will fail if it hasn't been loaded, call this method before such P/Invokes
        internal static void EnsureLoaded()
        {
            string installPath = GetWPFInstallPath();
            string fullName = Path.Combine(installPath, PresentationNativeDll);
            LoadLibrary(fullName);
        }

        // We support a "private CLR" which allows someone to use a different framework
        // location than what is specified in the registry.  The CLR support for this
        // involves two environment variable: COMPLUS_InstallRoot and COMPLUS_Version.
        [EnvironmentPermission(SecurityAction.Assert, Read = EnvironmentVariables)]
        private static string GetWPFInstallPath()
        {
            string path = null;

            string version = Environment.GetEnvironmentVariable(COMPLUS_Version);
            if (!String.IsNullOrEmpty(version))
            {
                path = Environment.GetEnvironmentVariable(COMPLUS_InstallRoot);
                if (String.IsNullOrEmpty(path))
                {
                    // The COMPLUS_Version environment variable was set, but the
                    // COMPLUS_InstallRoot environment variable was not.  We fall back
                    // to getting the framework install root from the registry, but
                    // still use the private CLR version.
                    path = ReadLocalMachineString(DOTNET_RegKey, DOTNET_Install_RegValue);
                }

                if (!String.IsNullOrEmpty(path))
                {
                    path = Path.Combine(path, version);
                }
            }

            if (String.IsNullOrEmpty(path))
            {
                // The COMPLUS_Version environment variable was not set.  We do not support
                // extracting the appropriate version ourselves, since this could come from
                // various places (app config, etc), so we default to 4.0.  The entire path
                // is stored in the registry, under the v4 key.
                path = ReadLocalMachineString(FRAMEWORK_RegKey, FRAMEWORK_InstallPath_RegValue);
            }

            // WPF chose to make a subdirectory for its own DLLs under the framework directory.
            path = Path.Combine(path, WPF_SUBDIR);

            return path;
        }

        private static string ReadLocalMachineString(string key, string valueName)
        {
            string keyPath = "HKEY_LOCAL_MACHINE\\" + key;
            new RegistryPermission(RegistryPermissionAccess.Read, keyPath).Assert();
            return Microsoft.Win32.Registry.GetValue(keyPath, valueName, null) as string;
        }

    }
}
