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
    private StateManager stateManager;

    // Update is called once per frame
    void Update()
    {
        if (terrainGenerator == null)
        {
            terrainGenerator = FindObjectOfType<TerrainGenerator>();
        }

        if (stateManager == null)
        {
            stateManager = FindObjectOfType<StateManager>();
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

        if (coroutineBusy == false && !stateManager.Busy())
        {
            Timing.RunCoroutine(CheckGround().CancelWith(gameObject));
        }
    }

    private IEnumerator<float> CheckGround()
    {
        coroutineBusy = true;
        yield return Timing.WaitForSeconds(3);
        for (int i = 0; i < groundCheckPositions.Length; i++)
        {
            if (!Physics.Raycast(groundCheckPositions[i], Vector3.down, 5) && !stateManager.Busy())
            {
                if (type == "Tree")
                {
                    terrainGenerator.treeLocations.Remove(location);
                }
                else
                {
                    terrainGenerator.grassLocations.Remove(location);
                }

                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
            }
        }
        coroutineBusy = false;
    }
}
