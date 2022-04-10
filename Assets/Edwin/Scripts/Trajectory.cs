using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Projectile projectile;

    Transform[] dotsList;

    float timeStamp;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public void DotsInit()
    {
        if (projectile)
        {
            dotsList = new Transform[projectile.dotsNb];
            for (int i = 0; i < projectile.dotsNb; i++)
            {
                dotsList[i] = Instantiate(projectile.dotPrefab, null).transform;
                dotsList[i].parent = projectile.dotsParent.transform;
                dotsList[i].gameObject.SetActive(false);
            }
        }
        
    }

    public void UpdateDots(bool isFlipped)
    {
        if (projectile)
        {
            Vector2 pos;
            timeStamp = projectile.dotsSpacing;
            bool visible = true;

            float forceX = isFlipped ? -projectile.force.x : projectile.force.x;

            for (int i = 0; i < projectile.dotsNb; i++)
            {
                if (visible)
                {
                    pos.x = projectile.pos.x + (forceX * timeStamp);
                    pos.y = projectile.pos.y + (projectile.force.y * timeStamp) - ((Physics2D.gravity.magnitude * projectile.rb.gravityScale * Mathf.Pow(timeStamp, 2)) / 2f);

                    Collider2D col = Physics2D.OverlapCircle(pos, projectile.trajectoryDetectionRadius);
                    if ((col && col.tag != "Projectile1" && col.tag != "Projectile2" && col.tag != "Projectile3" && col.tag != "Player"))
                        visible = false;
                    else
                        dotsList[i].gameObject.SetActive(true);

                    dotsList[i].position = pos;
                    timeStamp += projectile.dotsSpacing;
                }
                else
                    dotsList[i].gameObject.SetActive(false);
            }
        }
        
    }

    public void Show()
    {
        if (projectile)
            projectile.dotsParent.SetActive(true);
    }
    public void Hide()
    {
        if (projectile)
            projectile.dotsParent.SetActive(false);
    }
}
