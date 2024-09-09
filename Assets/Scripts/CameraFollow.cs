using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private Transform player;
   private Vector3 offset;

   private void Start()
   {
      offset = transform.position - player.position;
   }

   private void Update()
   {
      transform.position = player.position + offset;
   }
}
