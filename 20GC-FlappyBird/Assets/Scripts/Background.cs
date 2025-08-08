using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BackgroundSpawn")
        {
            BackgroundManager.Instance.SpawnBackground(BackgroundManager.Instance.GetSpawnLocation());
        }

        if (collision.gameObject.tag == "PoolBackground")
        {
            gameObject.SetActive(false);
        }
    }
}
