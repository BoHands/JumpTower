using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool collected;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        CheckVisible();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collected = true;
            CheckVisible();
        }
    }

    public void CheckVisible()
    {
        rend = GetComponent<SpriteRenderer>();
        if (collected)
        {
            rend.color = Color.clear;
        }
        else
        {
            rend.color = Color.blue;
        }
    }
}
