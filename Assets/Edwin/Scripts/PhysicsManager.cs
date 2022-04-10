using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{

    #region Singleton class: PhysicsManager

    public static PhysicsManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    Camera cam;

    public Projectile projectile;

    bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
        projectile.DesactivateRb(); ////////////
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            OnDragStart();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            OnDragEnd();
        }

        if(isDragging)
        {
            OnDrag();
        }
    }
    void OnDragStart()
    {
        projectile.DesactivateRb();//////////
        projectile.trajectory.Show();   // a mettre dans projectile
    }
    void OnDrag()
    {
        //projectile.trajectory.UpdateDots();///////////////////////
    }
    void OnDragEnd()
    {
        projectile.ActivateRb();/////////
        //projectile.Push();//////////////////////

        projectile.trajectory.Hide();   // a mettre dans projectile
    }
}
