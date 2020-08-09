using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public string ID = "unassigned";
    public Material lineMat;
    public string creationMethod;
    public GameObject inputObject;
    public GameObject outputObject;
    public string inputID;
    public string outputID;
    private LineRenderer connectionLine;
    private float updateTick;
    public int address;
    public int powerAmount;
    public string type;
    public string fuelType;
    public int fuelAmount;
    public int connectionAttempts;
    public bool connectionFailed;
    public bool blocked;
    public bool outOfFuel;
    public bool noReactor;
    private int fuelConsumptionTimer;
    public Texture2D generatorOffTexture;
    public Texture2D generatorOnTexture;
    private GameObject builtObjects;

    void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    void Update()
    {
        updateTick += 1 * Time.deltaTime;
        if (updateTick > 0.5f + (address * 0.001f))
        {
            GetComponent<PhysicsHandler>().UpdatePhysics();
            updateTick = 0;

            if (outputObject == null)
            {
                connectionAttempts += 1;
                if (connectionAttempts >= 120)
                {
                    connectionAttempts = 0;
                    connectionFailed = true;
                }

                if (connectionFailed == false)
                {
                    GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Built");
                    foreach (GameObject obj in allObjects)
                    {
                        if (IsValidObject(obj))
                        {
                            ConnectToOutput(obj);
                        }
                    }
                }
            }

            if (outputObject != null && connectionFailed == false)
            {
                connectionAttempts = 0;
                DistributePower();
            }
            else
            {
                //Debug.Log(ID + " output object is null.");
                connectionLine.enabled = false;
                if (connectionFailed == true)
                {
                    if (creationMethod.Equals("spawned"))
                    {
                        creationMethod = "built";
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        if (outputObject != null && outputObject.GetComponent<PowerReceiver>() != null)
        {
            if (outputObject.GetComponent<PowerReceiver>().power >= powerAmount)
            {
                outputObject.GetComponent<PowerReceiver>().power -= powerAmount;
            }

            if (outputObject.GetComponent<PowerReceiver>().power < 1)
            {
                outputObject.GetComponent<PowerReceiver>().powerON = false;
            }

            if (outputObject.GetComponent<PowerReceiver>().power < outputObject.GetComponent<PowerReceiver>().speed && outputObject.GetComponent<PowerReceiver>().speed > 1)
            {
                outputObject.GetComponent<PowerReceiver>().speed = outputObject.GetComponent<PowerReceiver>().power;
                outputObject.GetComponent<PowerReceiver>().overClocked = true;
            }
        }
    }

    bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    private void ConnectToOutput(GameObject obj)
    {
        float distance = Vector3.Distance(transform.position, obj.transform.position);
        if (distance < 40)
        {
            if (obj.GetComponent<PowerReceiver>() != null && outputObject == null)
            {
                if (obj.GetComponent<PowerReceiver>().powerObject != null)
                {
                    if (obj.GetComponent<PowerReceiver>().powerObject.GetComponent<PowerConduit>() == null)
                    {
                        if (creationMethod.Equals("spawned") && obj.GetComponent<PowerReceiver>().ID.Equals(outputID))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<PowerReceiver>().powerObject = gameObject;
                            outputObject.GetComponent<PowerReceiver>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                            creationMethod = "built";
                        }
                        else if (creationMethod.Equals("built"))
                        {
                            outputObject = obj;
                            outputObject.GetComponent<PowerReceiver>().powerObject = gameObject;
                            outputObject.GetComponent<PowerReceiver>().power += powerAmount;
                            connectionLine.SetPosition(0, transform.position);
                            connectionLine.SetPosition(1, obj.transform.position);
                            connectionLine.enabled = true;
                        }
                    }
                }
                else
                {
                    if (creationMethod.Equals("spawned") && obj.GetComponent<PowerReceiver>().ID.Equals(outputID))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<PowerReceiver>().powerObject = gameObject;
                        outputObject.GetComponent<PowerReceiver>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                        creationMethod = "built";
                    }
                    else if (creationMethod.Equals("built"))
                    {
                        outputObject = obj;
                        outputObject.GetComponent<PowerReceiver>().powerObject = gameObject;
                        outputObject.GetComponent<PowerReceiver>().power += powerAmount;
                        connectionLine.SetPosition(0, transform.position);
                        connectionLine.SetPosition(1, obj.transform.position);
                        connectionLine.enabled = true;
                    }
                }
            }
        }
    }

    private void DistributePower()
    {
        if (outputObject.GetComponent<PowerReceiver>() != null)
        {
            outputObject.GetComponent<PowerReceiver>().powerObject = gameObject;
            outputID = outputObject.GetComponent<PowerReceiver>().ID;

            if (type.Equals("Solar Panel"))
            {
                DistributeAsSolarPanel();
            }
            else if (type.Equals("Generator"))
            {
                DistributeAsGenerator();
            }
            else if (type.Equals("Reactor Turbine"))
            {
                DistributeAsReactorTurbine();
            }
        }
    }

    private void DistributeAsSolarPanel()
    {
        Vector3 sunPosition = new Vector3(7000, 15000, -10000);

        if (Physics.Linecast(sunPosition, transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (blocked == true)
                {
                    outputObject.GetComponent<PowerReceiver>().power += powerAmount;
                }

                outputObject.GetComponent<PowerReceiver>().powerON = true;
                blocked = false;
            }
            else
            {
                if (blocked == false)
                {
                    outputObject.GetComponent<PowerReceiver>().power -= powerAmount;

                    if (outputObject.GetComponent<PowerReceiver>().power < 1)
                    {
                        outputObject.GetComponent<PowerReceiver>().powerON = false;
                    }
                }
                blocked = true;
            }
        }
    }

    private void DistributeAsGenerator()
    {
        if (fuelType.Equals("Coal") && fuelAmount >= 1)
        {
            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().Play();
            }

            if (outOfFuel == true)
            {
                outputObject.GetComponent<PowerReceiver>().power += powerAmount;
            }

            GetComponent<Light>().enabled = true;
            GetComponent<Renderer>().material.mainTexture = generatorOnTexture;
            outputObject.GetComponent<PowerReceiver>().powerON = true;
            outOfFuel = false;
            fuelConsumptionTimer += 1;

            if (fuelConsumptionTimer > 10 - (address * 0.01f))
            {
                fuelAmount -= 1;
                fuelConsumptionTimer = 0;
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();

            if (outOfFuel == false)
            {
                outputObject.GetComponent<PowerReceiver>().power -= powerAmount;

                if (outputObject.GetComponent<PowerReceiver>().power < 1)
                {
                    outputObject.GetComponent<PowerReceiver>().powerON = false;
                }
            }

            GetComponent<Renderer>().material.mainTexture = generatorOffTexture;
            GetComponent<Light>().enabled = false;
            outOfFuel = true;
        }
    }

    private void DistributeAsReactorTurbine()
    {
        bool reactorFound = false;
        Vector3[] allDir = { transform.up, -transform.up, transform.right, -transform.right, transform.forward, -transform.forward };

        foreach (Vector3 dir in allDir)
        {
            if (Physics.Raycast(transform.position, dir, out RaycastHit dirHit, 3))
            {
                if (dirHit.collider.gameObject.GetComponent<NuclearReactor>() != null)
                {
                    reactorFound = dirHit.collider.gameObject.GetComponent<NuclearReactor>().sufficientCooling;
                }
            }
        }
        if (reactorFound == true)
        {
            if (noReactor == true)
            {
                outputObject.GetComponent<PowerReceiver>().power += powerAmount;
            }

            outputObject.GetComponent<PowerReceiver>().powerON = true;
            noReactor = false;

            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (noReactor == false)
            {
                outputObject.GetComponent<PowerReceiver>().power -= powerAmount;

                if (outputObject.GetComponent<PowerReceiver>().power < 1)
                {
                    outputObject.GetComponent<PowerReceiver>().powerON = false;
                }
            }

            noReactor = true;
            GetComponent<AudioSource>().Stop();
        }
    }
}

