// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using UnityEngine;

namespace Source.UI
{
    public interface IScrollRoot
    {
        void OnEndedClick(Vector2 position);
        void OnMoving(Vector2 position);
        void OnStartedClick(Vector2 position);
        void OnZooming(float value);
    }
}