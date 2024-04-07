using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBrick : MonoBehaviour
{
    [SerializeField] Vector3 localPositionA, localPositionB;
    [SerializeField] int[] aHours, bHours;
    float time;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void CheckTime()
    {
        int hour = System.DateTime.Now.Hour;

        foreach (int item in aHours)
        {
            if (item == hour)
            {
                StartCoroutine(MoveToPos(localPositionA));
            }
        }

        foreach (int item in bHours)
        {
            if (item == hour)
            {
                StartCoroutine(MoveToPos(localPositionB));
            }
        }
    }

    public IEnumerator MoveToPos(Vector3 newPos)
    {
        if (transform.position == newPos)
        {
            yield break;
        }
        float a = 0;
        Vector3 startPos = transform.localPosition;
        while (a < 1)
        {
            a += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, newPos, a);
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
