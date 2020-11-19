// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.LayoutSpace.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="LayoutSpace" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     Draws a layout space.
        /// </summary>
        private sealed class LayoutSpace : ILayoutDrawable
        {
            private readonly float _size;

            /// <summary>
            ///     Initializes a new instance of the <see cref="LayoutSpace"/> class.
            /// </summary>
            /// <param name="size">
            ///     The size of the space in pixels.
            /// </param>
            internal LayoutSpace( float size )
            {
                _size = size;
            }

            /// <inheritdoc />
            public void Draw() =>
                GUILayout.Space( _size );
        }
    }
}