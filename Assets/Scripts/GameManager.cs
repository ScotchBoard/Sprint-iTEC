using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int enemyNumberIncrease = 5;
    [SerializeField]
    private int enemyMaxNumberIncrease = 2;
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private float waveTextShowTime = 2f;
    [SerializeField]
    private int waveNumber = 3;

    public static GameManager INSTANCE = null;

    public bool IsGameOver { get; set; }
    //public bool WaveDone { get; set; }

    private int waveCount = 1;

    void Awake()
    {
        //Check if instance already exists
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            if (INSTANCE != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
            }
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        IsGameOver = false;

        StartCoroutine(TextTimeAppear(waveTextShowTime));
        waveCount++;
    }

    public void GameOver()
    {
        waveText.text = "Game Over";
        IsGameOver = true;
    }

    public void WaveFinnished()
    {
        if(waveCount <= waveNumber && !IsGameOver)
        {
            StartCoroutine(TextTimeAppear(waveTextShowTime));
            waveCount++;

            NextWave();
        }
    }

    private IEnumerator TextTimeAppear(float showTime)
    {
        waveText.text = "Wave " + waveCount;
        yield return new WaitForSeconds(showTime);
        waveText.text = "";
    }

    private void NextWave()
    {
        GetComponentInParent<EnemySpawn>().CreateEnemies(GetComponentInParent<EnemySpawn>().ObjectsNumber + enemyNumberIncrease);
        GetComponentInParent<EnemySpawn>().ChangeMaxObjectsNumber(GetComponentInParent<EnemySpawn>().MaxObjectsNumber + enemyMaxNumberIncrease);
    }
}
