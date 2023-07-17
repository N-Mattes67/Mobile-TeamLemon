using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    //Nested class for ease-of-use
    [System.Serializable]
    public class CoinType
    {
        public string name;
        public GameObject prefab; //this needs to be a gameobject with a collider and the object script!
        public float speed; //how fast the object falls
        public int coinBonus; //how much score the player gets when succesfully avoiding the object
        public float minSpawnTime; //at which point in the game the object should spawn
    }

    public CoinType[] CoinTypes;
    public GameObject CoinHolder;

    public float spawningOffset = 0.5f; //makes it so that objects don't spawn on land but spawn more centralized
    public float decreaseStep = 0.01f; //each time an object is spawned, the spawntime gets decreased by this variable
    public float minSpawnTime = 1f; //spawning cap so that we don't end up with a shit ton of objects near the end of the game
    public float spawnTime = 5f; //base spawn time when the game starts, gets decreased over time by decreaseStep

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(coinWave());
    }
    private void spawnCoin()
    {
        //first we create an array of possible spawnable objects at a given spawnTime, we'll use a list instead of
        // an array since it's easier to add items that way
        List<CoinType> spawnableCoins = new();

        foreach (CoinType type in CoinTypes)
        {
            //for example if the type's min spawn time is 4.1, and the current spawner's spawntime is 4
            //then 4.1 >= 4 (true) and the object will be valid
            if (type.minSpawnTime >= spawnTime) spawnableCoins.Add(type);
        }

        //Randomize index for selection
        int index = Random.Range(0, spawnableCoins.Count);
        CoinType selectedCoin = spawnableCoins[index];

        //Builds the instantiated object and sets its type properties, let's keep it clean and assign
        //an empty holder object to keep our inspector readable during runtime (Mikkel wants it clean boys)
        GameObject coin = Instantiate(selectedCoin.prefab, CoinHolder.transform);
        coin.name = selectedCoin.name;
        coin.GetComponent<Coin>().speed = selectedCoin.speed;
        coin.GetComponent<Coin>().coinsModifier = selectedCoin.coinBonus;

        //Sets the spawn position
        coin.transform.position = new Vector2(Random.Range(-screenBounds.x + spawningOffset, screenBounds.x - spawningOffset), screenBounds.y);
    }

    IEnumerator coinWave()
    {
        yield return new WaitForSeconds(spawnTime);

        spawnCoin();

        if (spawnTime > minSpawnTime)
        {
            spawnTime -= decreaseStep;
            Debug.Log(spawnTime);
        }

        StartCoroutine(coinWave());
    }
}
