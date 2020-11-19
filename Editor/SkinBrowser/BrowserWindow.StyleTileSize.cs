// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleTileSize.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="BrowserWindow" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     Calculates the current size of a tile item depending on the selected scale.
        /// </summary>
        private sealed class StyleTileSize : ICurrentValueProvider<Vector2>
        {
            private readonly Vector2 _maximalSize;

            private readonly Vector2 _minimalSize;

            /// <summary>
            ///     Initializes a new instance of the <see cref="StyleTileSize"/> class.
            /// </summary>
            /// <param name="currentScale">
            ///     The service providing the current scale of a tile item.
            /// </param>
            /// <param name="minimalSize">
            ///     The minimal size for a tile item.
            /// </param>
            /// <param name="maximalSize">
            ///     The maximal size for a tile item.
            /// </param>
            internal StyleTileSize(
                [NotNull] ICurrentValueProvider<float> currentScale,
                Vector2 minimalSize,
                Vector2 maximalSize )
            {
                _minimalSize = minimalSize;
                _maximalSize = maximalSize;

                currentScale.Changed += UpdateSize;
                UpdateSize( currentScale.Current );
            }

            /// <inheritdoc />
            public event Action<Vector2> Changed;

            /// <inheritdoc />
            public Vector2 Current { get; private set; }

            private void UpdateSize( float scale )
            {
                Current = Vector2.Lerp( _minimalSize, _maximalSize, scale );
                Changed?.Invoke( Current );
            }
        }
    }
}