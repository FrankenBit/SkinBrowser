// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ToggleControl.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ToggleControl" /> of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public partial class BrowserWindow
    {
        private sealed class ToggleControl : ICurrentValueProvider<bool>, ILayoutDrawable
        {
            [NotNull]
            private readonly GUIContent _content;

            [NotNull]
            private readonly DeferredStyle _style;

            internal ToggleControl( [NotNull] DeferredStyle style, [NotNull] GUIContent content )
            {
                _style = style ?? throw new ArgumentNullException( nameof( style ) );
                _content = content ?? throw new ArgumentNullException( nameof( content ) );
            }

            public event Action<bool> Changed;

            public bool Current { get; private set; }

            public void Draw()
            {
                bool state = GUILayout.Toggle( Current, _content, _style );
                if ( state != Current ) ChangeState( state );
            }

            private void ChangeState( bool state )
            {
                Current = state;
                Changed?.Invoke( state );
            }
        }
    }
}