using Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerControlls
{
    public class WeaponPickUp : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

        [Header("Settings")]
        [SerializeField] private WeaponData data;
        public WeaponData Data { get { return data; } private set { data = value; } }

        float Time { get; set; }

        private void Update()
        {
            this.Time += UnityEngine.Time.deltaTime;

            Vector3 position = this.SpriteRenderer.transform.localPosition;
            position.y = .4f + Mathf.Sin(this.Time) * .2f;
            this.SpriteRenderer.transform.localPosition = position;


            Vector3 rotation = this.SpriteRenderer.transform.localEulerAngles;
            rotation.y = this.Time * 20f;
            this.SpriteRenderer.transform.localEulerAngles = rotation;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(other.transform.GetComponent<PickUpTrigger>() is PickUpTrigger player))
                return;

            player.Player.PickUp(this.Data);

            GameObject.Destroy(this.gameObject);
        }
    }
}
