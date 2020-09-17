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

    //! Called by unity engine on start up to initialize variables.
    public void Start()
    {
        connectionLine = gameObject.AddComponent<LineRenderer>();
        connectionLine.startWidth = 0.2f;
        connectionLine.endWidth = 0.2f;
        connectionLine.material = lineMat;
        connectionLine.loop = true;
        connectionLine.enabled = false;
        builtObjects = GameObject.Find("Built_Objects");
    }

    //! Called once per frame by unity engine.
    public void Update()
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

    //! Used to notify power receivers when the power source is destroyed.
    public void OnDestroy()
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
        }
    }

    //! Returns true if the object exists, is active and is not a standard building block.
    private bool IsValidObject(GameObject obj)
    {
        if (obj != null)
        {
            return obj.transform.parent != builtObjects.transform && obj.activeInHierarchy;
        }
        return false;
    }

    //! Connects the power source to a power receiver.
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

    //! Determines the type of power source and calls the appropriate power distribution method.
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

    //! Distributes power to power receivers as a solar panel.
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

    //! Distributes power to power receivers as a generator.
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

    //! Distributes power to power receivers as reactor turbine.
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