// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFactory.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using JetBrains.Annotations;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     A factory creating instances of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of instances to be created by the factory.
    /// </typeparam>
    internal interface IFactory<out T>
    {
        /// <summary>
        ///     Create a new instance of <typeparamref name="T" />.
        /// </summary>
        /// <returns>
        ///     A new instance of <typeparamref name="T" />.
        /// </returns>
        [NotNull]
        T Create();
    }
}