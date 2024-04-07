using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] float winCheckRad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, winCheckRad);
    }

    public void CheckWinner()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, winCheckRad);

        foreach (Collider2D item in hits)
        {
            if (item.GetComponent<PlayerMovement>() != null)
            {
                FindObjectOfType<GameManager>().TriggerWin();
            }
        }
    }
}
