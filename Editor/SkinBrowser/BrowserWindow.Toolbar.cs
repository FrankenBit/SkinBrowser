// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.Toolbar.cs" company="FrankenBit">
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
    ///     Nested <see cref="Toolbar" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     Controls the drawing of a toolbar in an editor window.
        /// </summary>
        private sealed class Toolbar : ILayoutDrawable
        {
            [NotNull]
            private static readonly GUILayoutOption ExpandWidth = GUILayout.ExpandWidth( true );

            [CanBeNull]
            private readonly ILayoutDrawable _leftSide;

            [CanBeNull]
            private readonly ILayoutDrawable _rightSide;

            [NotNull]
            private readonly DeferredStyle _style;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Toolbar" /> class.
            /// </summary>
            /// <param name="style">
            ///     The style to be used to draw the toolbar. Most probably the "Toolbar" style.
            /// </param>
            /// <param name="leftSide">
            ///     The item to be drawn on the left side of the toolbar, if any.
            /// </param>
            /// <param name="rightSide">
            ///     The item to be drawn on the right side of the toolbar, if any.
            /// </param>
            internal Toolbar(
                [NotNull] DeferredStyle style,
                [CanBeNull] ILayoutDrawable leftSide,
                [CanBeNull] ILayoutDrawable rightSide )
            {
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
                _leftSide = leftSide;
                _rightSide = rightSide;
            }

            /// <inheritdoc />
            public void Draw()
            {
                EditorGUILayout.BeginHorizontal( _style, ExpandWidth );
                _leftSide?.Draw();
                GUILayout.FlexibleSpace();
                _rightSide?.Draw();
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}