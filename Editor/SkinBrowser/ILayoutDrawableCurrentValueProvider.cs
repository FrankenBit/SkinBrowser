// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILayoutDrawableCurrentValueProvider.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     An UI control that provides a current value of some kind.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the value provided by the control.
    /// </typeparam>
    internal interface ILayoutDrawableCurrentValueProvider<out T> : ICurrentValueProvider<T>, ILayoutDrawable
    {
    }
}