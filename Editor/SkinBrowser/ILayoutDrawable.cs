// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILayoutDrawable.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A control that can be drawn in the editor UI using the editor layout.
    /// </summary>
    internal interface ILayoutDrawable
    {
        /// <summary>
        ///     Draw the control in the editor UI using the editor layout.
        /// </summary>
        void Draw();
    }
}