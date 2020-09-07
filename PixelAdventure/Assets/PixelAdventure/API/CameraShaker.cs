﻿using System.Collections;
using UnityEngine;

namespace PixelAdventure.API
{
    public class CameraShaker : MonoBehaviour
    {
        public static CameraShaker Instance {get; private set;}
        private void Awake() => Instance = this;

        private static readonly int k_SHAKE1_HASH = Animator.StringToHash("shake1");
        private Animator m_Animator = null;

        private void Start() 
        {
            m_Animator = GetComponent<Animator>();    
        }

        float m_Duration = 0.05f;
        float m_Magnitude = 0.1f;

        private bool m_Shake = false;

        public void Shake() => m_Animator.SetTrigger(k_SHAKE1_HASH); //StartCoroutine(ShakeCoroutine());

        private void Update() 
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Shake();
            }    
        }

        private IEnumerator ShakeCoroutine()
        {
            if (m_Shake) yield return 0;
            m_Shake = true;

            Vector3 originalPosition = transform.localPosition;

            float endTime = Time.realtimeSinceStartup + m_Duration;

            while (Time.realtimeSinceStartup < endTime)
            {

                transform.localPosition += new Vector3
                (
                    x: (Random.Range(-1f, 1f) + 0.01f) * m_Magnitude,
                    y: (Random.Range(-1f, 1f) + 0.01f) * m_Magnitude,
                    z: originalPosition.z
                );

                yield return null;
            }

            transform.localPosition = originalPosition;
        }
    }
}