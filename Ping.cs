using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ping : MonoBehaviour
{
    float timer;
    public string type;
    public GameObject billboard;
    public GameObject attachedLight;
    public Material ironMat;
    public Material aluminumMat;
    public Material tinMat;
    public Material copperMat;
    public Material darkMatterMat;
    public Material iceMat;
    public Material coalMat;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("QE_World_Atmo"))
        {
            attachedLight.GetComponent<Light>().color = Color.white;
        }
        if (type.Equals("iron"))
        {
            billboard.GetComponent<MeshRenderer>().material = ironMat;
        }
        if (type.Equals("tin"))
        {
            billboard.GetComponent<MeshRenderer>().material = tinMat;
        }
        if (type.Equals("copper"))
        {
            billboard.GetComponent<MeshRenderer>().material = copperMat;
        }
        if (type.Equals("aluminum"))
        {
            billboard.GetComponent<MeshRenderer>().material = aluminumMat;
        }
        if (type.Equals("darkMatter"))
        {
            billboard.GetComponent<MeshRenderer>().material = darkMatterMat;
        }
        if(type.Equals("ice"))
        {
            billboard.GetComponent<MeshRenderer>().material = iceMat;
        }
        if(type.Equals("coal"))
        {
            billboard.GetComponent<MeshRenderer>().material = coalMat;
        }
    }

    void Update()
    {
        transform.LookAt(GameObject.Find("Player").transform);
        timer += 1 * Time.deltaTime;
        if (timer > 1)
        {
            Destroy(gameObject);
        }
    }
}
