using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField]
    private Text storyText;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private GameObject enemy;

    private new Camera camera;

    private void Start()
    {
        camera = Camera.main;

        StartCoroutine(MoveCameraTime());
    }

    IEnumerator MoveCameraTime()
    {
        storyText.text = "Welcome, almighty guardian";
        yield return new WaitForSeconds(3);

        storyText.text = "This is you, the ultimate dab cat";
        camera.transform.LookAt(player.transform);
        yield return new WaitForSeconds(5);

        storyText.text = "This is your planet, Purrrlandia";
        camera.transform.LookAt(planet.transform);
        yield return new WaitForSeconds(5);

        storyText.text = "These are the most vicious enemies ever";
        camera.transform.LookAt(enemy.transform);
        yield return new WaitForSeconds(5);

        storyText.text = "Protect the planet with your life";
        yield return new WaitForSeconds(5);

        storyText.text = "May the God of Meownia laser your path in your journey";
        yield return new WaitForSeconds(6);

        storyText.text = "";

        SceneManager.LoadScene(1);
    }
}
