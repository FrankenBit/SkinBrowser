// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.EditorGUILayoutStyledEnumPopup.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="EditorGUILayoutStyledEnumPopup{T}" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A styled editor GUI layout popup for an enumeration of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the enumeration to display a popup for.
        /// </typeparam>
        private sealed class EditorGUILayoutStyledEnumPopup<T> : EditorGUILayoutEnumPopupBase<T>
            where T : struct, Enum
        {
            private readonly DeferredStyle _style;

            /// <summary>
            ///     Initializes a new instance of the <see cref="EditorGUILayoutStyledEnumPopup{T}" /> class.
            /// </summary>
            /// <param name="style">
            ///     The GUI style to be used to draw the popup control.
            /// </param>
            internal EditorGUILayoutStyledEnumPopup( [NotNull] DeferredStyle style )
            {
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
            }

            protected override int Draw( int value, GUIContent[] names, int[] values ) =>
                EditorGUILayout.IntPopup( value, names, values, _style );
        }
    }
}