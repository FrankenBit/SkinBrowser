// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDrawable{T}.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A control that can be drawn in the editor UI using a supplied item and the specified destination rectangle.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of item to be drawn in the control.
    /// </typeparam>
    internal interface IDrawable<in T>
    {
        /// <summary>
        ///     Draw the control in the editor UI using the supplied <paramref name="item" />
        ///     and the specified destination rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     The rectangle where the control shall be drawn.
        /// </param>
        /// <param name="item">
        ///     The item to be drawn in the control.
        /// </param>
        void Draw( Rect rectangle, [NotNull] T item );
    }
}