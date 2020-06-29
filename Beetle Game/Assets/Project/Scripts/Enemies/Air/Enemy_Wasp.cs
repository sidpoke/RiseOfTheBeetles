﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wasp : IEnemy
{
    [Header("Wasp Setup")]
    public float walkSpeed;
    [Range(0, 100)]
    public float boundaryLeft;
    [Range(0, 100)]
    public float boundaryRight;

    [Header("Projectile Setup")]
    public float spawnTime;
    public Transform projectileSpawn;
    public GameObject projectile;

    float startX;
    bool turned = false;
    int direction { get { return (turned ? -1 : 1); } }
    bool gotStomped = false;
    float projectileTimer;


    // Start is called before the first frame update
    protected override void Start()
    {
        gravity = 0;
        startX = transform.position.x;
    }

    // Update is called once per frame
    protected override void Update()
    {        
        Turn();
        Walk();
        if(playerIsInRange(distanceTrigger))
        {
            Shoot();
        }
    }

    void Turn()
    {
        if ((transform.position.x > startX + boundaryRight && !turned || transform.position.x < startX - boundaryLeft && turned))
        {
            turned = !turned;
        }
    }

    void Walk()
    {
        moveCon.Walk(walkSpeed * direction, 0.3f);
    }

    void Shoot()
    {
        projectileTimer += Time.deltaTime;

        if(projectileTimer >= spawnTime)
        { 
            GameObject bullet = Instantiate(projectile, projectileSpawn) as GameObject;
            bullet.transform.SetParent(null);

            projectileTimer = 0;
        }
    }

    public void StompDamage(int damage)
    {
        if (!gotStomped)
        {
            ApplyDamage(damage);
            StartCoroutine(WaitForStompTime(0.2f));
            gotStomped = true;
        }
    }

    private IEnumerator WaitForStompTime(float time)
    {
        yield return new WaitForSeconds(time);
        gotStomped = false;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = new Color(1, 0, 0, 0.3f);
        UnityEditor.Handles.DrawSolidDisc(transform.position + Vector3.right * boundaryRight, transform.forward, 0.2f);
        UnityEditor.Handles.color = new Color(0, 1, 0, 0.3f);
        UnityEditor.Handles.DrawSolidDisc(transform.position - Vector3.right * boundaryLeft, transform.forward, 0.2f);
        UnityEditor.Handles.color = new Color(0, 1, 1, 0.3f);
        UnityEditor.Handles.DrawSolidDisc(projectileSpawn.position, transform.forward, 0.2f);
#endif
    }
}
