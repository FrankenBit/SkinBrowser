// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.TextContentField.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="TextContentField" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     An input field that wraps entered text in <see cref="GUIContent" />.
        /// </summary>
        private sealed class TextContentField : ILayoutDrawableCurrentValueProvider<GUIContent>
        {
            [NotNull]
            private readonly ILayoutDrawable _field;

            /// <summary>
            ///     Initializes a new instance of the <see cref="TextContentField"/> class.
            /// </summary>
            /// <param name="currentText">
            ///     A component used to enter the text to be wrapped in <see cref="GUIContent" />.
            /// </param>
            internal TextContentField( [NotNull] ILayoutDrawableCurrentValueProvider<string> currentText )
            {
                _field = currentText ?? throw new ArgumentNullException( nameof( currentText ) );
                currentText.Changed += HandleChangedText;
                Current = WrapIntoContent( currentText.Current );
            }

            /// <inheritdoc />
            public event Action<GUIContent> Changed;

            /// <inheritdoc />
            [NotNull]
            public GUIContent Current { get; private set; }

            /// <inheritdoc />
            public void Draw() =>
                _field.Draw();

            [NotNull]
            private static GUIContent WrapIntoContent( [CanBeNull] string text ) =>
                new GUIContent( text );

            private void HandleChangedText( [CanBeNull] string text )
            {
                Current = WrapIntoContent( text );
                Changed?.Invoke( Current );
            }
        }
    }
}