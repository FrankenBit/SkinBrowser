// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleFactory.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleFactory" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A factory that creates new <see cref="GUIStyle" /> instances.
        /// </summary>
        private sealed class StyleFactory : IFactory<GUIStyle>
        {
            [NotNull]
            private readonly string _styleName;

            /// <summary>
            ///     Initializes a new instance of the <see cref="StyleFactory"/> class.
            /// </summary>
            /// <param name="styleName">
            ///     The name of the style to be created by the factory.
            /// </param>
            internal StyleFactory( [NotNull] string styleName )
            {
                _styleName = styleName ?? throw new ArgumentNullException( nameof( styleName ) );
            }

            /// <inheritdoc />
            public GUIStyle Create() =>
                new GUIStyle( _styleName );
        }
    }
}