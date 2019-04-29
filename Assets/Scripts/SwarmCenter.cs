using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmCenter : MonoBehaviour
{
    public static SwarmCenter Instance { get; private set; }

    public LayerMask hitLayers;
    public Transform swarmWrapper;
    public GameObject swarmiePrefab;
    public GameObject lazerPointer;
    public List<GameObject> interestingThings = new List<GameObject>();

    public float panSpeed = 20;
    public float panArea = 0.3f;

    private static readonly Vector3 SPAWN_OFFSET = Vector3.up * 0.6f;
    private const float SPAWN_LOCKOUT_DURATION = 0.05f;
    private float spawnCooldown = 0;
    private bool mouseDown;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < PersistentData.Instance.SwarmSize; i++)
        {
            StartCoroutine("SpawnSwarmie");
        }
    }

    void Update()
    {
        if (spawnCooldown > 0)
            spawnCooldown -= Time.deltaTime;

        if (mouseDown)
            mouseDown = Input.GetMouseButtonDown(0);
        else if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            lazerPointer.SetActive(!lazerPointer.activeSelf);
        }

        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        if (plane.Raycast(ray, out point))
            lazerPointer.transform.position = ray.GetPoint(point);

        HandleEdgePanning();
    }

    private IEnumerator SpawnSwarmie()
    {
        while (spawnCooldown > 0)
            yield return null;

        spawnCooldown = SPAWN_LOCKOUT_DURATION;
        GameObject swarmie = Instantiate(swarmiePrefab, swarmWrapper);
        swarmie.transform.position = transform.position + SPAWN_OFFSET;
        Swarmie swarmieBehaviour = swarmie.GetComponent<Swarmie>();
    }

    private void HandleEdgePanning()
    {
        float horizontalChange = 0;
        float verticalChange = 0;

        float mouseWidthPosition = Input.mousePosition.x / Screen.width;
        if (mouseWidthPosition > 1f - panArea)
            horizontalChange = mouseWidthPosition - (1f - panArea);
        else if (mouseWidthPosition < panArea)
            horizontalChange = mouseWidthPosition - panArea;

        float mouseHeightPosition = Input.mousePosition.y / Screen.height;
        if (mouseHeightPosition > 1f - panArea)
            verticalChange = mouseHeightPosition - (1f - panArea);
        else if (mouseHeightPosition < panArea)
            verticalChange = mouseHeightPosition - panArea;

        if (horizontalChange != 0 || verticalChange != 0)
        {
            Vector3 change = new Vector3(horizontalChange, 0, verticalChange) * Time.deltaTime * panSpeed;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x + change.x, -15, 15),
                0,
                Mathf.Clamp(transform.position.z + change.z, -15, 15)
            );
        }
    }

    void OnDestroy()
    {
        interestingThings = null;
    }
}
