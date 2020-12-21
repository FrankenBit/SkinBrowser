// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScrollableContent.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Content that can be drawn and provides its height to support improved scrolling.
    /// </summary>
    internal interface IScrollableContent : ILayoutDrawable, IHeightProvider
    {
    }
}