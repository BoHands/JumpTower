using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLevels : MonoBehaviour
{
    [SerializeField] int[] onHours;
    [SerializeField] Vector2 wind;
    PlayerMovement player;
    GameManager gameMan;
    bool windOn;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        gameMan = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>();
        CheckTime();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 20)
        {
            time = 0;
            CheckTime();
        }

        if (windOn && gameMan.playerLevel == System.Array.IndexOf(gameMan.Levels, gameObject))
        {
            player.ApplyWind(wind * Time.deltaTime);
        }
    }

    public void CheckTime()
    {
        int hour = System.DateTime.Now.Hour;

        foreach (int item in onHours)
        {
            if (item == hour)
            {
                windOn = true;
                return;
            }
        }

        windOn = false;
    }
}
