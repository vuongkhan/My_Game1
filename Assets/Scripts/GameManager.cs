using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [Header("Road Settings")]
    public GameObject[] roadSegments;
    public float gapBetweenSegments = 5f;
    public int initialSegments = 5;
    public int maxSegments = 7;

    [Header("Player Reference")]
    public Transform player;
    private float spawnPosition = 0f;
    private Queue<GameObject> activeSegments = new Queue<GameObject>();

    private void Start()
    {
        Debug.Log("RoadSpawner Started - Spawning Initial Segments");
        for (int i = 0; i < initialSegments; i++)
        {
            SpawnRoadSegment();
        }
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        if (player.position.x + GetMaxVisibleDistance() > spawnPosition)
        {
            Debug.Log($"Player at {player.position.x}, Spawning New Segment at {spawnPosition}");
            SpawnRoadSegment();
        }

        if (activeSegments.Count > maxSegments)
        {
            Debug.Log($"Too many segments ({activeSegments.Count} > {maxSegments}), removing oldest...");
            RemoveOldRoadSegment();
        }
    }

    private void SpawnRoadSegment()
    {
        int randomIndex = Random.Range(0, roadSegments.Length);
        GameObject selectedSegment = roadSegments[randomIndex];

        if (selectedSegment == null)
        {
            Debug.LogError($"Road segment at index {randomIndex} is NULL!");
            return;
        }

        GameObject newRoad = Instantiate(selectedSegment, new Vector3(spawnPosition, 0, 0), Quaternion.identity);

        float segmentWidth = GetSegmentWidth(newRoad);
        if (segmentWidth <= 0)
        {
            Debug.LogError($"Invalid width calculated for {newRoad.name}, using default 20.");
            segmentWidth = 20f;
        }

        activeSegments.Enqueue(newRoad);
        Debug.Log($"Spawned segment '{newRoad.name}' at {spawnPosition}, Width: {segmentWidth}");

        spawnPosition += segmentWidth + gapBetweenSegments;
    }

    private void RemoveOldRoadSegment()
    {
        if (activeSegments.Count > 0)
        {
            GameObject oldRoad = activeSegments.Dequeue();
            Debug.Log($"Removing segment '{oldRoad.name}'");
            Destroy(oldRoad);
        }
    }

    private float GetSegmentWidth(GameObject segment)
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;

        Debug.Log($"Calculating width for segment: {segment.name}");

        foreach (Renderer renderer in segment.GetComponentsInChildren<Renderer>())
        {
            minX = Mathf.Min(minX, renderer.bounds.min.x);
            maxX = Mathf.Max(maxX, renderer.bounds.max.x);
            Debug.Log($"Renderer found: {renderer.gameObject.name}, Bounds: {renderer.bounds.size.x}");
        }

        foreach (Collider2D collider in segment.GetComponentsInChildren<Collider2D>())
        {
            minX = Mathf.Min(minX, collider.bounds.min.x);
            maxX = Mathf.Max(maxX, collider.bounds.max.x);
            Debug.Log($"Collider2D found: {collider.gameObject.name}, Bounds: {collider.bounds.size.x}");
        }

        if (minX == float.MaxValue || maxX == float.MinValue)
        {
            Debug.LogWarning($"Segment '{segment.name}' has no Renderer or Collider2D! Using default width.");
            return 20f;
        }

        float width = maxX - minX;
        Debug.Log($"Calculated width for '{segment.name}': {width}");
        return width;
    }

    private float GetMaxVisibleDistance()
    {
        return 6 * (gapBetweenSegments + 20f);
    }
}
