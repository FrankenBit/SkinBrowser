// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILayoutDrawable{T}.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A control that can be drawn in the editor UI using the editor layout and a supplied item.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of item to be drawn in the control.
    /// </typeparam>
    internal interface ILayoutDrawable<in T>
    {
        /// <summary>
        ///     Draw the control in the editor UI using the editor layout and the supplied <paramref name="item" />.
        /// </summary>
        /// <param name="item">
        ///     The item to be drawn in the control.
        /// </param>
        void Draw( [NotNull] T item );
    }
}