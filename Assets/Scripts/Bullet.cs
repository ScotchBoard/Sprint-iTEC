using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float duration = 5f;

    private void Start()
    {
        StartCoroutine(TimeOut(duration));
    }

    IEnumerator TimeOut(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyInfo>().Hurt();
            Destroy(gameObject);
        }
    }
}
