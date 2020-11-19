// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleNameLabel.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleNameLabel" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A label used to display the name of a <see cref="GUIStyle" />.
        /// </summary>
        private sealed class StyleNameLabel : ILayoutDrawable
        {
            [NotNull]
            private readonly ClickableSpringLabel _label;

            private string _currentName;

            /// <summary>
            ///     Initializes a new instance of the <see cref="StyleNameLabel"/> class.
            /// </summary>
            /// <param name="label">
            ///     The spring label used to display the name of the <see cref="GUIStyle" />.
            /// </param>
            internal StyleNameLabel( [NotNull] ClickableSpringLabel label )
            {
                _label = label ?? throw new ArgumentNullException( nameof( label ) );
                _label.SetTooltip( "Click to copy the style name to the clipboard." );
                _label.Clicked += CopyNameToClipboard;
            }

            /// <inheritdoc />
            public void Draw() =>
                _label.Draw();

            /// <summary>
            ///     Change displayed <paramref name="style" />.
            /// </summary>
            /// <param name="style">
            ///     The style to display its name in the label, if any.
            /// </param>
            internal void ChangeStyle( [CanBeNull] GUIStyle style ) =>
                _label.SetText( _currentName = style?.name );

            private void CopyNameToClipboard() =>
                GUIUtility.systemCopyBuffer = $"\"{_currentName}\"";
        }
    }
}