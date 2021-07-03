using UnityEngine;
using System.Collections.Generic;
using MEC;

public class ProceduralFoliage : MonoBehaviour
{
    private bool coroutineBusy;
    private Vector3[] groundCheckPositions;
    public Vector3 location;
    public string type;
    private TerrainGenerator terrainGenerator;

    // Update is called once per frame
    void Update()
    {
        if (terrainGenerator == null)
        {
            terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        if (groundCheckPositions == null)
        {
            groundCheckPositions = new Vector3[]
            {
                transform.position,
                transform.position + Vector3.right * 5,
                transform.position + Vector3.right * -5,
                transform.position + Vector3.forward * 5,
                transform.position + Vector3.forward * -5
            };
        }

        if (coroutineBusy == false)
        {
            Timing.RunCoroutine(CheckGround());
        }
    }

    private IEnumerator<float> CheckGround()
    {
        coroutineBusy = true;
        yield return Timing.WaitForSeconds(3);
        foreach (Vector3 pos in groundCheckPositions)
        {
            if (!Physics.Raycast(pos, Vector3.down, 5))
            {
                if (type == "Tree")
                {
                    terrainGenerator.treeLocations.Remove(location);
                }
                else
                {
                    terrainGenerator.grassLocations.Remove(location);
                }
                Destroy(gameObject);
            }
        }
        coroutineBusy = false;
    }
}
