using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] Levels;
    PlayerMovement player;
    Transform mainCam;
    public int playerLevel, gameComps;
    [SerializeField] Vector3 startPos;
    [SerializeField] Collectable[] collectables;

    GameData saveData;
    float time;

    [SerializeField] TextMeshProUGUI tst;

    public string[] breakUp;

    public GameObject winScreen;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        mainCam = Camera.main.transform;

        Vector2 newPos = new Vector2(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"));

        tst.text = $"{newPos.x} , {newPos.y}";

        if (newPos != Vector2.zero)
        {
            player.transform.position = newPos;
            gameComps = PlayerPrefs.GetInt("comps");
            tst.text = (PlayerPrefs.GetString("colls"));
            breakUp = PlayerPrefs.GetString("colls").Split(',');

            for (int i = 0; i < collectables.Length; i++)
            {
                print(bool.Parse(breakUp[i]));
                if (breakUp.Length > i) collectables[i].collected = bool.Parse(breakUp[i]);
                collectables[i].CheckVisible();
            }
        }


        if (gameComps >= 1)
        {
            foreach (Collectable item in collectables)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Collectable item in collectables)
            {
                item.gameObject.SetActive(false);
            }
        }

        if (!Application.isEditor)
        {
            /*saveData = SaveSystem.LoadData();
            Vector2 newPos = new Vector2(saveData.xPos, saveData.yPos);*/


            /*for (int i = 0; i < saveData.collectables.Length; i++)
            {
                collectables[i].collected = saveData.collectables[i];
                collectables[i].CheckVisible();
            }*/
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        mainCam = Camera.main.transform;
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraLocation(Mathf.FloorToInt((player.transform.position.y + 14.5f) / 30));

        time += Time.deltaTime;
        if (time > 2)
        {
            SaveGame();
            time = 0;
        }
    }

    public void SetCameraLocation(int newLoc)
    {
        Vector3 newPos = Levels[newLoc].transform.position;
        newPos.z = mainCam.position.z;
        mainCam.position = newPos;
        playerLevel = newLoc;
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("xPos", player.transform.position.x);
        PlayerPrefs.SetFloat("yPos", player.transform.position.y);
        string toSave = "";
        foreach (Collectable item in collectables)
        {
            toSave += item.collected.ToString();
            toSave += ",";
        }
        PlayerPrefs.SetString("colls", toSave.TrimEnd(','));
        tst.text = (PlayerPrefs.GetString("colls"));
        PlayerPrefs.SetInt("comps", gameComps);
        /*saveData.xPos = player.transform.position.x;
        saveData.yPos = player.transform.position.y;*/

        /*saveData.collectables = new bool[collectables.Length];
        for (int i = 0; i < collectables.Length; i++)
        {
            saveData.collectables[i] = collectables[i].collected;
        }*/
    }

    public void TriggerWin()
    {
        winScreen.SetActive(true);
    }

    public void ResetGame()
    {
        gameComps++;
        player.transform.position = startPos;

        if (gameComps >= 1)
        {
            foreach (Collectable item in collectables)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Collectable item in collectables)
            {
                item.gameObject.SetActive(false);
            }
        }

        foreach (Collectable item in collectables)
        {
            item.collected = false;
            item.CheckVisible();
        }
        winScreen.SetActive(false);
        player.ResetPlayer();
    }
}
