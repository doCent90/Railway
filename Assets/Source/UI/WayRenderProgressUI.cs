using System;
using UnityEngine;

namespace Source.UI
{
    public class WayRenderProgressUI
    {
        private const float LineWidthComplete = 0.4f;

        private readonly LineRenderer _lineRenderer;
        private readonly Material _builtMaterial;

        public WayRenderProgressUI(LineRenderer lineRenderer, Material builtMaterial)
        {
            _builtMaterial = builtMaterial ? builtMaterial : throw new ArgumentNullException();
            _lineRenderer = lineRenderer ? lineRenderer : throw new ArgumentNullException();
        }

        public void Render(bool isBuilt)
        {
            if (!isBuilt)
                return;

            _lineRenderer.material = _builtMaterial;
            _lineRenderer.widthMultiplier = LineWidthComplete;
        }
    }
}
