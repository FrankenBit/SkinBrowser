// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.DeferredStyle.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="DeferredStyle" /> class of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        /// <summary>
        ///     A deferred GUI style that instantiates the GUI style only when it is actually
        ///     requested for the first time, ideally during an OnGUI message.
        /// </summary>
        private sealed class DeferredStyle
        {
            [NotNull]
            private readonly IFactory<GUIStyle> _factory;

            [CanBeNull]
            private GUIStyle _style;

            /// <summary>
            ///     Initializes a new instance of the <see cref="DeferredStyle"/> class.
            /// </summary>
            /// <param name="factory">
            ///     A factory used to create the GUI style when it is requested for the first time.
            /// </param>
            internal DeferredStyle( [NotNull] IFactory<GUIStyle> factory )
            {
                _factory = factory ?? throw new ArgumentNullException( nameof( factory ) );
            }

            /// <summary>
            ///     Implicit conversion operator to <see cref="GUIStyle" />.
            /// </summary>
            /// <param name="style">
            ///     The deferred style instance to be converted into GUI style.
            /// </param>
            /// <returns>
            ///     A <see cref="GUIStyle" /> instance provided by the deferred style.
            /// </returns>
            [NotNull]
            public static implicit operator GUIStyle( [NotNull] DeferredStyle style ) =>
                style.GetOrCreateStyle();

            [NotNull]
            private GUIStyle GetOrCreateStyle() =>
                _style ?? CreateStyle();

            [NotNull]
            private GUIStyle CreateStyle() =>
                _style = _factory.Create();
        }
    }
}