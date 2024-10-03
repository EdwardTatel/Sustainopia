using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash : MonoBehaviour
{
    public GameObject trashPrefab; // Assign your trash prefab in the Inspector
    public int numberOfTrash = 10; // Number of trash objects to spawn

    // Define the range for spawning (editable in the Inspector)
    public float xMin = -5f;
    public float xMax = 5f;
    public float yMin = 0f;
    public float yMax = 5f;
    public float zMin = -5f;
    public float zMax = 5f;

    // If you want to visualize the spawn area
    private void OnDrawGizmosSelected()
    {
        // Draw a wire cube representing the spawn area
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((xMin + xMax) / 2, (yMin + yMax) / 2, (zMin + zMax) / 2);
        Vector3 size = new Vector3(xMax - xMin, yMax - yMin, zMax - zMin);
        Gizmos.DrawWireCube(center, size);
    }

    void Start()
    {
        SpawnTrashObjects();
    }

    void SpawnTrashObjects()
    {
        // Create an array of tags to ensure even distribution
        string[] trashTags = { "Ewaste", "Organic", "Recyclable" };

        // Create a list to track tag assignments and ensure even distribution
        List<string> assignedTags = new List<string>();

        // Loop to spawn the trash objects
        for (int i = 0; i < numberOfTrash; i++)
        {
            // Assign a tag to this trash object, ensuring equal distribution
            string assignedTag = GetTagWithEqualDistribution(trashTags, assignedTags);

            // Generate random position within the defined range
            float xPos = Random.Range(xMin, xMax);
            float yPos = Random.Range(yMin, yMax);
            float zPos = Random.Range(zMin, zMax);

            Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);

            // Instantiate the trash object
            GameObject spawnedTrash = Instantiate(trashPrefab, spawnPosition, Quaternion.Euler(30, 0, 0));

            // Assign the tag to the trash object
            spawnedTrash.tag = assignedTag;

            // Change the sprite color based on the assigned tag
            ChangeSpriteColorBasedOnTag(spawnedTrash, assignedTag);

            // Optionally log the spawned object and its tag
        }
    }

    string GetTagWithEqualDistribution(string[] tags, List<string> assignedTags)
    {
        // Count the number of tags that have already been assigned
        int[] tagCounts = new int[tags.Length];
        for (int i = 0; i < assignedTags.Count; i++)
        {
            for (int j = 0; j < tags.Length; j++)
            {
                if (assignedTags[i] == tags[j])
                {
                    tagCounts[j]++;
                }
            }
        }

        // Find the tag with the lowest count to ensure equal distribution
        int minCount = Mathf.Min(tagCounts);
        List<string> availableTags = new List<string>();

        // Only allow the tags that have been assigned the least number of times
        for (int i = 0; i < tags.Length; i++)
        {
            if (tagCounts[i] == minCount)
            {
                availableTags.Add(tags[i]);
            }
        }

        // Randomly pick one of the available tags
        string selectedTag = availableTags[Random.Range(0, availableTags.Count)];

        // Add the selected tag to the assigned tags list
        assignedTags.Add(selectedTag);

        return selectedTag;
    }

    void ChangeSpriteColorBasedOnTag(GameObject trashObject, string tag)
    {
        // Get the SpriteRenderer component
        SpriteRenderer spriteRenderer = trashObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            return;
        }

        // Set the color based on the tag
        switch (tag)
        {
            case "Ewaste":
                spriteRenderer.color = Color.yellow; // Change to desired color for Ewaste
                break;
            case "Organic":
                spriteRenderer.color = Color.green; // Change to desired color for Organic
                break;
            case "Recyclable":
                spriteRenderer.color = Color.blue; // Change to desired color for Recyclable
                break;
        }
    }

}
