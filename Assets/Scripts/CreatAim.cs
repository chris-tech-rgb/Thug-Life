using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatAim : MonoBehaviour
{
    public float Timer;
    public GameObject RedPoint;
    private Vector2 position;
    public GameObject player;

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = Random.Range(0.01f, 2.8f);
            position = new Vector2(Random.Range(player.gameObject.transform.position.x-9f, player.gameObject.transform.position.x + 9f), Random.Range(player.gameObject.transform.position.y - 4.5f, player.gameObject.transform.position.y + 4.5f));
            Instantiate(RedPoint, position, Quaternion.identity);
        }
    }

}