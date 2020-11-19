// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ClickableSpringLabel.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ClickableSpringLabel" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        /// <summary>
        ///     A spring label that displays a text, can be clicked on by the user and expands to all the available width.
        /// </summary>
        private sealed class ClickableSpringLabel : ILayoutDrawable
        {
            [NotNull]
            private static readonly GUILayoutOption ExpandWidth = GUILayout.ExpandWidth( true );

            [NotNull]
            private readonly DeferredStyle _style;

            [NotNull]
            private GUIContent _content = GUIContent.none;

            [NotNull]
            private string _tooltip = string.Empty;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ClickableSpringLabel"/> class.
            /// </summary>
            /// <param name="style">
            ///     The style to be used to draw the label.
            /// </param>
            internal ClickableSpringLabel( [NotNull] DeferredStyle style )
            {
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
            }

            /// <summary>
            ///     Occurs when the user has clicked on the label.
            /// </summary>
            internal event Action Clicked;

            /// <inheritdoc />
            public void Draw()
            {
                if ( GUILayout.Button( _content, _style, ExpandWidth ) ) Clicked?.Invoke();
            }

            /// <summary>
            ///     Change the <paramref name="text" /> to be displayed in the label.
            /// </summary>
            /// <param name="text">
            ///     The text to be displayed in the label.
            /// </param>
            internal void SetText( [CanBeNull] string text ) =>
                _content = text != null ? new GUIContent( text, _tooltip ) : GUIContent.none;

            /// <summary>
            ///     Change the <paramref name="tooltip" /> to be displayed when the user hovers over the label.
            /// </summary>
            /// <param name="tooltip">
            ///     The new tooltip to be displayed, if any.
            /// </param>
            internal void SetTooltip( [CanBeNull] string tooltip )
            {
                _tooltip = tooltip ?? string.Empty;
                _content = _content == GUIContent.none ? GUIContent.none : new GUIContent( _content.text, _tooltip );
            }
        }
    }
}