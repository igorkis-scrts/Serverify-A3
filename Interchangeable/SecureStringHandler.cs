using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Interchangeable;

/// <summary>
/// Provides a converter from SecureString to ordinary managed string.
/// </summary>
public static class SecureStringHandler
{
    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <param name="value">Secured string value.</param>
    /// <returns>Managed string.</returns>
    public static string ConvertToString(SecureString value)
    {
        IntPtr valuePtr = IntPtr.Zero;
        try
        {
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
        }
    }
}
