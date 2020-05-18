﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform LookAt;
    
    public bool LookForwardByInput = true;
    public float LookDistance = 1.0f;
    [Range(1, 10)]
    public float LookSpeed = 0;

    private Vector3 offset;
    private Vector3 lookPosition;

    private void Start()
    {
        offset = transform.localPosition;
    }

    void Update()
    {
        if (GameManager.Instance.GameState == eGameState.running)
        {
            if (LookAt == null)
                return;
  
            if(LookForwardByInput)
            {
                lookPosition = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), -1) * LookDistance;
            }
            else
            {
                lookPosition = Vector3.zero;
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, offset + lookPosition, LookSpeed * Time.deltaTime);
        }
    }

    //public void SetRecoil(Vector3 recoil)
    //{
    //    transform.position += recoil;
    //}
}
