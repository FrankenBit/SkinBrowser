// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.ScaleSlider.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="ScaleSlider" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A scale slider used to specify the scale of the style preview tiles.
        /// </summary>
        private sealed class ScaleSlider : ILayoutDrawableCurrentValueProvider<float>
        {
            [NotNull]
            private readonly GUILayoutOption _sliderWidth;

            private readonly float _snapWidth;

            private float _scale = 0.5f;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ScaleSlider"/> class.
            /// </summary>
            /// <param name="sliderWidth">
            ///     The width of the slider control in pixels (e.g. 55 to match the Project window).
            /// </param>
            /// <param name="snapWidth">
            ///     The percentage of the slider width where the slider should snap to the zero value
            ///     (e.g. 0.2f to match the behavior of the slider in the Project window).
            /// </param>
            internal ScaleSlider( float sliderWidth, float snapWidth )
            {
                _sliderWidth = GUILayout.Width( sliderWidth );
                _snapWidth = snapWidth;
            }

            /// <inheritdoc />
            public event Action<float> Changed;

            /// <summary>
            ///     Gets or sets the current scale.
            /// </summary>
            public float Current
            {
                get => GetNormalizedScale( _scale, _snapWidth );
                internal set => _scale = SetNormalizedScale( value, _snapWidth );
            }

            /// <inheritdoc />
            public void Draw() =>
                UpdateScale( GUILayout.HorizontalSlider( _scale, 0, 1, _sliderWidth ) );

            private static float GetNormalizedScale( float scale, float snapWidth ) =>
                scale > snapWidth ? ( scale - snapWidth ) / ( 1 - snapWidth ) : 0;

            private static float SetNormalizedScale( float scale, float snapWidth ) =>
                scale > 0 ? scale / ( 1 - snapWidth ) + snapWidth : 0;

            private void ChangeScale( float scale )
            {
                _scale = scale;
                Changed?.Invoke( Current );
            }

            private void ChangeScaleIfModified( float scale )
            {
                if ( ScaleWasModified( scale ) ) ChangeScale( scale );
            }

            private bool ScaleWasModified( float scale ) =>
                !Mathf.Approximately( scale, _scale );

            private float SnapScale( float scale ) =>
                scale < _snapWidth ? 0 : scale;

            private void UpdateScale( float scale ) =>
                ChangeScaleIfModified( SnapScale( scale ) );
        }
    }
}