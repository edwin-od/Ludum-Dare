using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 scale = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 pushDirection;
    [SerializeField] float pushForce;
    public float projectileDetectionRadius;

    public int dotsNb;
    public GameObject dotPrefab;
    public float dotsSpacing;
    public float trajectoryDetectionRadius;
    public float destroyTime = 4f;

    public GameObject explosionPrefab;

    [HideInInspector] public GameObject dotsParent;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;
    [HideInInspector] public Vector3 pos { get { return transform.position; } }

    [HideInInspector] public Vector2 force { get { return pushDirection.normalized * pushForce; } }

    [HideInInspector] public Trajectory trajectory;

    private bool isPushed;

    private Player player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            if (this.gameObject.transform.GetChild(i).GetComponent<Trajectory>())
            {
                trajectory = this.gameObject.transform.GetChild(i).GetComponent<Trajectory>();
            }
        }

        dotsParent = trajectory.transform.GetChild(0).gameObject;

        isPushed = false;

    }

    private void Start()
    {
        player = this.transform.parent.GetComponent<Player>();
    }

    private void Update()
    {
        if (isPushed)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(pos, projectileDetectionRadius);
            foreach(Collider2D col in cols)
            {
                if (col && col.gameObject.tag != "Projectile1" && col.gameObject.tag != "Projectile2" && col.gameObject.tag != "Projectile3" && col.gameObject.tag != "Player")
                {
                    if ((col.gameObject.tag == "Structure1" && this.gameObject.tag == "Projectile1") ||
                        (col.gameObject.tag == "Structure2" && this.gameObject.tag == "Projectile2") ||
                        (col.gameObject.tag == "Structure3" && this.gameObject.tag == "Projectile3"))
                    {
                        Destroy(col.gameObject);
                    }

                    GameObject exp = Instantiate(explosionPrefab, this.transform);
                    exp.transform.parent = null;

                    player.anim.audioManager.PlayEffect(player.anim.breakAudioSource, player.anim.breakAudioSource.clip);
                    Destroy(this.gameObject);
                }
            }

            if (destroyTime > 0)
                destroyTime -= Time.deltaTime;
            else
                Destroy(this.gameObject);
        }
    }

    public void Push(bool isFlipped)
    {
        if (rb)
        {
            isPushed = true;

            Vector2 dir = new Vector2(isFlipped ? -pushDirection.x : pushDirection.x, pushDirection.y);
            rb.AddForce(dir.normalized * pushForce, ForceMode2D.Impulse);
        }
    }

    public void ActivateRb()
    {
        if (rb)
        {
            rb.isKinematic = false;
        }
    }
    public void DesactivateRb()
    {
        if (rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
        }
    }
}
