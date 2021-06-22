using UnityEngine;
using System.Collections;

public class ProceduralFoliage : MonoBehaviour
{
    private Coroutine checkGroundCoroutine;
    private bool coroutineBusy;
    private Vector3[] groundCheckPositions;

    // Update is called once per frame
    void Update()
    {
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
            checkGroundCoroutine = StartCoroutine(CheckGround());
        }
    }

    private IEnumerator CheckGround()
    {
        coroutineBusy = true;
        foreach (Vector3 pos in groundCheckPositions)
        {
            if (!Physics.Raycast(pos, Vector3.down, 5))
            {
                Destroy(gameObject);
            }
        }
        yield return new WaitForSeconds(3);
        coroutineBusy = false;
    }
}
