using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //Nested class for ease-of-use
    [System.Serializable]
    public class ObstacleType
    {
        public string name;
        public GameObject prefab; //this needs to be a gameobject with a collider and the obstacle script!
        public float speed;//how fast the obstacle falls
        public float sideSpeed; //how fast the obstacle moves left-right
        public int scoreBonus; //how much score the player gets when succesfully avoiding the obstacle
        public float minSpawnTime; //at which point in the game the obstacle should spawn
    }

    public ObstacleType[] obstacleTypes;
    public GameObject obstacleHolder;

    public float spawningOffset = 0.5f; //makes it so that obstacles don't spawn on land but spawn more centralized
    public float decreaseStep = 0.01f; //each time an object is spawned, the spawntime gets decreased by this variable
    public float minSpawnTime = 1f; //spawning cap so that we don't end up with a shit ton of obstacles near the end of the game
    public float spawnTime = 5f; //base spawn time when the game starts, gets decreased over time by decreaseStep

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(obstacleWave());
    }
    private void spawnObstacle()
    {
        //first we create an array of possible spawnable obstacles at a given spawnTime, we'll use a list instead of
        // an array since it's easier to add items that way
        List<ObstacleType> spawnableObstacles = new List<ObstacleType>();

        foreach (ObstacleType type in obstacleTypes)
        {
            //for example if the type's min spawn time is 4.1, and the current spawner's spawntime is 4
            //then 4.1 >= 4 (true) and the object will be valid
            if (type.minSpawnTime >= spawnTime) spawnableObstacles.Add(type);
        }

        //Randomize index for selection
        int index = Random.Range(0, spawnableObstacles.Count);
        ObstacleType selectedObstacle = spawnableObstacles[index];

        //Builds the instantiated object and sets its type properties, let's keep it clean and assign
        //an empty holder object to keep our inspector readable during runtime (Mikkel wants it clean boys)
        GameObject obstacle = Instantiate(selectedObstacle.prefab, obstacleHolder.transform);
        obstacle.name = selectedObstacle.name;
        obstacle.GetComponent<Obstacle>().speed = selectedObstacle.speed;
        obstacle.GetComponent<Obstacle>().sideSpeed = selectedObstacle.sideSpeed;
        obstacle.GetComponent<Obstacle>().scoreModifier = selectedObstacle.scoreBonus;

        //Sets the spawn position
        obstacle.transform.position = new Vector2(Random.Range(-screenBounds.x + spawningOffset, screenBounds.x - spawningOffset), screenBounds.y);
    }

    IEnumerator obstacleWave()
    {
        yield return new WaitForSeconds(spawnTime);

        spawnObstacle();

        if (spawnTime > minSpawnTime)
        {
            spawnTime -= decreaseStep;
            Debug.Log(spawnTime);
        }

        StartCoroutine(obstacleWave());
    }
}
