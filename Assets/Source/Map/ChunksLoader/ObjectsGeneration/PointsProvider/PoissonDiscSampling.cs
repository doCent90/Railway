using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider
{
    public static class PoissonDiscSampling
    {
        public static List<Vector2> GeneratePoints(float maxRadius, float minRadius, Vector2 sampleRegionSize,
            float falloff,
            float center, int numSamplesBeforeRejection = 30)
        {
            float cellSize = maxRadius / Mathf.Sqrt(2);
            float radius = minRadius;

            List<Vector2>[,] grid = new List<Vector2>[Mathf.CeilToInt(sampleRegionSize.x / cellSize),
                Mathf.CeilToInt(sampleRegionSize.y / cellSize)];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new List<Vector2>();
                }
            }

            List<Vector2> points = new List<Vector2>();
            List<Vector2> spawnPoints = new List<Vector2>();

            spawnPoints.Add(sampleRegionSize / 2);
            while (spawnPoints.Count > 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Count);
                Vector2 spawnCentre = spawnPoints[spawnIndex];
                bool candidateAccepted = false;

                for (int i = 0; i < numSamplesBeforeRejection; i++)
                {
                    float angle = Random.value * Mathf.PI * 2;
                    Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                    float newRadius = Mathf.Clamp(Mathf.Abs(spawnCentre.y + center) * falloff, minRadius, maxRadius);
                    Vector2 candidate = spawnCentre + dir * Random.Range(newRadius, 2 * newRadius);

                    if (IsValid(candidate, sampleRegionSize, cellSize, newRadius, points, grid))
                    {
                        points.Add(candidate);
                        spawnPoints.Add(candidate);
                        grid[(int) (candidate.x / cellSize), (int) (candidate.y / cellSize)].AddRange(points);
                        candidateAccepted = true;
                        break;
                    }
                }

                if (!candidateAccepted)
                {
                    spawnPoints.RemoveAt(spawnIndex);
                }
            }

            return points;
        }

        static bool IsValid(Vector2 candidate, Vector2 sampleRegionSize, float cellSize, float radius, List<Vector2> points,
            List<Vector2>[,] grid)
        {
            if (candidate.x >= 0 && candidate.x < sampleRegionSize.x && candidate.y >= 0 &&
                candidate.y < sampleRegionSize.y)
            {
                int cellX = (int) (candidate.x / cellSize);
                int cellY = (int) (candidate.y / cellSize);
                int searchStartX = Mathf.Max(0, cellX - 2);
                int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
                int searchStartY = Mathf.Max(0, cellY - 2);
                int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

                for (int x = searchStartX; x <= searchEndX; x++)
                {
                    for (int y = searchStartY; y <= searchEndY; y++)
                    {
                        foreach (Vector2 point in grid[x, y])
                        {
                            float sqrDst = (candidate - point).sqrMagnitude;
                            if (sqrDst < radius * radius)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }

            return false;
        }
    }
}