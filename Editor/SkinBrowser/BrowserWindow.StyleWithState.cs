// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BrowserWindow.StyleWithState.cs" company="FrankenBit">
//   Copyright 2020 by FrankenBit.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

using JetBrains.Annotations;

using UnityEngine;

namespace FrankenBit.SkinBrowser
{
    /// <summary>
    ///     Nested <see cref="StyleWithState" /> structure of the <see cref="BrowserWindow" /> class.
    /// </summary>
    public sealed partial class BrowserWindow
    {
        private struct StyleWithState : IEquatable<StyleWithState>
        {
            internal StyleWithState( [NotNull] GUIStyle style, bool state )
            {
                Style = style;
                State = state;
            }

            [NotNull]
            internal string Name =>
                Style.name;

            internal bool State { get; }

            [NotNull]
            internal GUIStyle Style { get; }

            public bool Equals( StyleWithState other ) =>
                Style.Equals( other.Style );

            public override bool Equals( object obj ) =>
                obj is StyleWithState other && Equals( other );

            public override int GetHashCode() =>
                Style.GetHashCode();
        }
    }
}