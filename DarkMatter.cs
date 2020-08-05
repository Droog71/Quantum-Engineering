using UnityEngine;
using System.Collections;

public class DarkMatter: MonoBehaviour
{
    float size;
    public GameObject collector;

    void Update()
    {
        if (size < 10)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(10, 10, 10), Time.deltaTime * 0.5f);
            size += 1;
        }
        else if (size >= 10 && size < 20)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(5, 5, 5), Time.deltaTime * 0.5f);
            size += 1;
        }
        else if (size >= 20)
        {
            size = 0;
        }
    }
}