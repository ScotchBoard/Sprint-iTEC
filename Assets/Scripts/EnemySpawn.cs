using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private int objectsNumber = 10;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject catPlanet;
    [SerializeField]
    private float spawnTime = 3f;
    [SerializeField]
    private float distance = 20f;
    [SerializeField]
    private int maxObjects = 20;

    private GameObject[] enemies;
    private int count;

    public int ObjectsNumber { get { return objectsNumber; } private set { objectsNumber = value; } }
    public int MaxObjectsNumber { get { return maxObjects; } private set { maxObjects = value; } }

    private int objectsNumberPrivate;
    
    void Start()
    {
        count = 0;
        ObjectsNumber = objectsNumber;
        objectsNumberPrivate = objectsNumber;

        maxObjects -= 1;
        MaxObjectsNumber = maxObjects;

        CreateEnemies(objectsNumber);
    }

    public void ChangeMaxObjectsNumber(int number)
    {
        maxObjects = number;
    }

    private Vector3 RandomPosition()
    {
        Vector3 center = catPlanet.transform.position;
        SphereCollider planetCollider = catPlanet.GetComponent<SphereCollider>();

        return Random.onUnitSphere * (planetCollider.radius + distance) + center;
    }

    public void CreateEnemies(int objectsNumber)
    {
        ObjectsNumber = objectsNumber;
        enemies = new GameObject[objectsNumber];
        //GameManager.INSTANCE.WaveDone = false;

        for (int i = 0; i < objectsNumber; i++)
        {   
            GameObject enemy = Instantiate(enemyPrefab, RandomPosition(), Quaternion.identity);
            enemies[i] = enemy;
        }
        count = objectsNumber;
        objectsNumberPrivate = 0;
    }

    public void Eliminate(GameObject enemy)
    {
        //Debug.Log("1. " + objectsNumberPrivate + " " + count + " " + maxObjects);
        if (count < maxObjects && !GameManager.INSTANCE.IsGameOver)
        {
            enemy.transform.position = RandomPosition();
            enemy.GetComponent<EnemyInfo>().LookAtPlanet();
            count++;
            objectsNumberPrivate++;
        }
        else
        {
            //GameManager.INSTANCE.WaveDone = true;
            Destroy(enemy.gameObject);
            objectsNumberPrivate--;

        }
        //Debug.Log("2. " + objectsNumberPrivate + " " + count + " " + maxObjects);
        if (objectsNumberPrivate <= 0)
        {
            GameManager.INSTANCE.WaveFinnished();
        }
    }
}
