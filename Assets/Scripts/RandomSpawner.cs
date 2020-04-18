using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //Collect Houses
    public GameObject[] gameObj;

    //Object Pooling
    private List<GameObject> _objectPool;
    public int ObjectAmountTotal;
    public int ObjectAmountSingle;

    //Time to spawn
    public float waitForNextMax;
    public float countDown;

    //X Range
    public float xMin;
    public float xMax;

    //Y Range
    public float yMin;
    public float yMax;

    [SerializeField]
    private float _windSpeed = 3.0f;
    public float GetWindSpeed()
    {
        return _windSpeed;
    }

    static private RandomSpawner _singleton = null;
    static public RandomSpawner Get()
    {
        return _singleton;
    }

    private void Awake()
    {
        _singleton = this;
    }

    void Start()
    {
        _objectPool = new List<GameObject>();
        foreach (var obj in gameObj)
        {
            for (int j = 0; j < ObjectAmountSingle; j++)
            {
                GameObject gameObj = Instantiate(obj);
                obj.SetActive(false);
                _objectPool.Add(gameObj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            SpawnObject();
            countDown = waitForNextMax;
        }
    }

    void SpawnObject()
    {
        if (_objectPool != null)
        {
            GameObject gameObj = _objectPool[Random.Range(0, _objectPool.Count)];
            if (!gameObj.activeInHierarchy)
            {
                gameObj.transform.position = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), -5);
                gameObj.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Add GameObject to the RandSpawner Script you DumbAss!!!!");
        }
    }
}
