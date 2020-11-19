// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.BrowserSettings.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

using UnityEditor;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="BrowserSettings" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private static class BrowserSettings
        {
            private const string ScaleKey = "Scale";

            internal static void SetScale( float scale ) =>
                EditorUserSettings.SetConfigValue( ScaleKey, scale.ToString( NumberFormatInfo.InvariantInfo ) );

            internal static bool TryGetScale( out float scale )
            {
                string scaleString = EditorUserSettings.GetConfigValue( ScaleKey );
                return float.TryParse( scaleString, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out scale );
            }
        }
    }
}