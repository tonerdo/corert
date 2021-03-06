// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Globalization
    {
        [DllImport(Libraries.GlobalizationNative, EntryPoint = "GlobalizationNative_LoadICU")]
        internal static extern int LoadICU();

        [DllImport(Libraries.GlobalizationNative, EntryPoint = "GlobalizationNative_GetICUVersion")]
        internal static extern int GetICUVersion();
    }
}
