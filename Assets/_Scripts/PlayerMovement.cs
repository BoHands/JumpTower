using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float powerMult, yBump, magniMax, checkRad;
    [SerializeField] Transform groundCheck;
    [SerializeField] LineRenderer lineDis;
    bool jumpOn;

    SpriteRenderer rend;
    [SerializeField] Sprite[] poses;

    Vector2 pressStart;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        lineDis.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // debug controls
        if (Input.GetMouseButtonDown(0) && Grounded())
        {
            pressStart = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            jumpOn = true;
            lineDis.SetPosition(0, pressStart);
            lineDis.gameObject.SetActive(true);
            rend.sprite = poses[1];
        }

        if (Input.GetMouseButtonUp(0) && Grounded())
        {
            PushPlayer(pressStart - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Application.isEditor)
        {
            lineDis.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }


        // mobile controls
        if (Input.touches.Length > 0 && Grounded())
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                pressStart = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                jumpOn = true;
                lineDis.SetPosition(0, pressStart);
                lineDis.gameObject.SetActive(true);
                rend.sprite = poses[1];
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                PushPlayer(pressStart - (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position));
            }
            lineDis.SetPosition(1, (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position));
        }

        if (Grounded() && rb.velocity == Vector2.zero && !winChecked)
        {
            rend.sprite = poses[0];
            FindObjectOfType<EndGame>().CheckWinner();
            winChecked = true;
        }
    }

    bool winChecked;

    private void FixedUpdate()
    {
        veloHold = rb.velocity;
    }

    public void PushPlayer(Vector2 direction)
    {
        if (!jumpOn)
        {
            return;
        }
        if (direction.y < 0)
        {
            direction *= -1;
        }
        if (direction.magnitude > magniMax)
        {
            direction = direction.normalized * magniMax;
        }
        rb.AddForce(direction * powerMult);
        lineDis.gameObject.SetActive(false);
        jumpOn = false;
        jumpBump = true;
        Invoke("WinOff", 0.1f);
        rend.sprite = poses[2];
    }

    void WinOff()
    {
        winChecked = false;
    }

    bool Grounded()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(groundCheck.position, checkRad);
        foreach (Collider2D item in hits)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }

    Vector2 veloHold;
    bool jumpBump;
    bool recBounce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (recBounce)
        {
            return;
        }
        if (collision.gameObject.layer == 6 || collision.gameObject.layer == 3)
        {
            //Vector2 veloHold = rb.velocity;
            if (jumpBump)
            {
                if(veloHold.y > 0) veloHold.y += veloHold.y * yBump;
                jumpBump = false;
            }
            veloHold.x = -1 * veloHold.x;
            rb.velocity = veloHold;
            recBounce = true;
            Invoke("ResetRec", 0.1f);
        }
    }

    void ResetRec()
    {
        recBounce = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRad);
    }

    public void ApplyWind(Vector2 wind)
    {
        if (!Grounded())
        {
            rb.AddForce(wind);
        }
    }

    public void ResetPlayer()
    {
        lineDis.gameObject.SetActive(false);
    }
}
