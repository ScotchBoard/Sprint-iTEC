using ProgressBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMeter : MonoBehaviour
{
    [SerializeField]
    private GameObject nyanSong;
    [SerializeField]
    private GameObject nyanCat;
    [SerializeField]
    private int destroySkill = 60;
    [SerializeField]
    private int healSkill = 40;
    [SerializeField]
    private int firstThreshold = 5;
    [SerializeField]
    private int secondThreshold = 15;
    [SerializeField]
    private int maxCombo = 100;

    private int currentLevel;
    private ProgressBarBehaviour progressBar;
    private GameObject player;

    private void Awake()
    {
        progressBar = GameObject.Find("MewMeter").GetComponent<ProgressBarBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player");

        ResetNyanCat();
    }

    private void Start()
    {
        StartCoroutine(IncreaseComboPasively());

        nyanCat.gameObject.SetActive(false);
    }

    private void Update()
    {
        /*if(nyanCat.transform.position.x >= player.transform.position.x + 10 || nyanCat.transform.position.x <= player.transform.position.x - 10)
        {
            ResetNyanCat();
            GameManager.INSTANCE.IsGamePaused = false;
        }
        */
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentLevel >= destroySkill)
        {
            DestroySkill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentLevel >= healSkill)
        {
            HealSkill();
        }
    }

    private void DestroySkill()
    {
        currentLevel -= destroySkill;

        NyanCat();

        progressBar.DecrementValue(destroySkill);
    }

    // Best Easter Egg eva'
    private void NyanCat()
    {
        GameManager.INSTANCE.IsGamePaused = true;
        nyanCat.gameObject.SetActive(true);

        nyanSong.GetComponent<AudioSource>().time = 1.5f;
        nyanSong.GetComponent<AudioSource>().Play();

        nyanCat.GetComponent<Rigidbody>().AddForce(player.transform.right * 100);

        StartCoroutine(ResetTheCat());

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyInfo>().Hurt();
        }
    }

    IEnumerator ResetTheCat()
    {
        yield return new WaitForSeconds(6);
        ResetNyanCat();
        GameManager.INSTANCE.IsGamePaused = false;
        nyanSong.GetComponent<AudioSource>().Stop();
        nyanCat.gameObject.SetActive(false);
    }

    private void ResetNyanCat()
    {
        nyanCat.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        nyanCat.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void HealSkill()
    {
        currentLevel -= healSkill;

        GameObject.FindGameObjectWithTag("CatPlanet").GetComponent<PlanetInfo>().ResetHealth();

        progressBar.DecrementValue(healSkill);
    }

    public void IncreaseCombo()
    {
        int gain = 0;

        if(currentLevel <= firstThreshold)
        {
            gain = 1;
        }
        else
        {
            if(currentLevel <= secondThreshold)
            {
                gain = 2;
            }
            else
            {
                gain = 5;
            }
        }
        currentLevel += gain;
        progressBar.IncrementValue(gain);
    }

    public void ResetCombo()
    {
        currentLevel = 0;
        progressBar.Value = currentLevel;
    }

    IEnumerator IncreaseComboPasively()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            progressBar.IncrementValue(1);
            currentLevel++;
        }
    }
}
