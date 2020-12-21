// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ScrollView.cs" company="FrankenBit">
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
    ///     Partial <see cref="ScrollView" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class ScrollView : ILayoutDrawable
        {
            [NotNull]
            private readonly IScrollableContent _content;

            private Vector2 _scrollPosition;

            internal ScrollView( [NotNull] IScrollableContent content )
            {
                _content = content ?? throw new ArgumentNullException( nameof( content ) );
            }

            public void Draw()
            {
                _scrollPosition = BeginScrollView( _scrollPosition, _content.Height );
                _content.Draw();
                EditorGUILayout.EndScrollView();
            }

            private static Vector2 BeginScrollView( Vector2 scrollPosition, float fullHeight ) =>
                NormalizeScrollPosition(
                    EditorGUILayout.BeginScrollView( ScaleScrollPosition( scrollPosition, fullHeight ) ),
                    fullHeight );

            private static Vector2 NormalizeScrollPosition( Vector2 scrollPosition, float fullHeight ) =>
                new Vector2( scrollPosition.x, scrollPosition.y / fullHeight );

            private static Vector2 ScaleScrollPosition( Vector2 scrollPosition, float fullHeight ) =>
                new Vector2( scrollPosition.x, scrollPosition.y * fullHeight );
        }
    }
}