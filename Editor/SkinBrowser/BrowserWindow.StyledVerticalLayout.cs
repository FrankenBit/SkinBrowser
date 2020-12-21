// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyledVerticalLayout.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEditor;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyledVerticalLayout" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class StyledVerticalLayout : ILayoutDrawable
        {
            [NotNull]
            private readonly ILayoutDrawable _content;

            [NotNull]
            private readonly DeferredStyle _style;

            internal StyledVerticalLayout( [NotNull] DeferredStyle style, [NotNull] ILayoutDrawable content )
            {
                _content = content ?? throw new ArgumentNullException( nameof( content ) );
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
            }

            public void Draw()
            {
                EditorGUILayout.BeginVertical( _style );
                _content.Draw();
                EditorGUILayout.EndVertical();
            }
        }
    }
}