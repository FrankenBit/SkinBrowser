// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.TextField.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="TextField" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A text input field.
        /// </summary>
        private sealed class TextField : ILayoutDrawableCurrentValueProvider<string>
        {
            [NotNull]
            private readonly DeferredStyle _style;

            /// <summary>
            ///     Initializes a new instance of the <see cref="TextField"/> class.
            /// </summary>
            /// <param name="style">
            ///     The style to be used when drawing the field.
            /// </param>
            internal TextField( [NotNull] DeferredStyle style )
            {
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
            }

            /// <inheritdoc />
            public event Action<string> Changed;

            /// <inheritdoc />
            public string Current { get; internal set; }

            /// <inheritdoc />
            public void Draw() =>
                UpdateTextIfChanged( EditorGUILayout.TextField( Current, _style ) );

            private bool HasTextChanged( [CanBeNull] string text ) =>
                !string.Equals( text, Current, StringComparison.OrdinalIgnoreCase );

            private void UpdateText( [CanBeNull] string text )
            {
                Current = text;
                Changed?.Invoke( text );
            }

            private void UpdateTextIfChanged( [CanBeNull] string text )
            {
                if ( HasTextChanged( text ) ) UpdateText( text );
            }
        }
    }
}