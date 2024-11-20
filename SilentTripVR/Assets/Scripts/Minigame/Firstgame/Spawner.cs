using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The prefab to spawn
    public GameObject objectToSpawn;
    public RectTransform screen;
    public MinigameManager minigameManager;
    // Time interval between spawns
    public float spawnInterval = 2.0f;
    public float totalBomb = 10f;
    private float tempInterval;

    private void Start()
    {
        tempInterval = spawnInterval;
        minigameManager = GameObject.Find("Minigame Manager").GetComponent<MinigameManager>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        tempInterval = tempInterval - Time.deltaTime;
        if(totalBomb <= 0)
        {
            Debug.Log("cleared, with " + totalBomb + " left");
            minigameManager.delayTransition.NextScreen();
        }
        if(tempInterval <= 0)
        {
            SpawnObject();
            tempInterval = spawnInterval;
        }
    }

    void SpawnObject()
    {
        Debug.Log(totalBomb);
        GameObject newObject = Instantiate(objectToSpawn, gameObject.transform.position, Quaternion.identity, screen);
        totalBomb--;
    }
}
