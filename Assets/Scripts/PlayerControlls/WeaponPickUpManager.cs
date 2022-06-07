using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PlayerControlls
{
    public class WeaponPickUpManager : MonoBehaviour
    {
        [SerializeField] private WeaponPickUpManagerData data;
        public WeaponPickUpManagerData Data { get { return data; } }

        [SerializeField] private float spawnTime;
        public float SpawnTime { get { return spawnTime; } set { this.spawnTime = value; } }

        [SerializeField] private List<WeaponPickUp> pickUps = new List<WeaponPickUp>();
        public List<WeaponPickUp> PickUps { get { return pickUps; } }

        Transform SpawnRoot { get; set; }

        private void Awake()
        {
            this.SpawnRoot = new GameObject("Weapon Pick Ups").transform;
        }

        private void Update()
        {
            this.SpawnTime -= Time.deltaTime;

            if (this.SpawnTime <= 0)
            {
                if (this.PickUps.Count >= 3)
                    return;

                WeaponPickUp pickUp = GameObject.Instantiate(this.Data.Weapon);
                pickUp.SetData(this.Data.GetWeaponData());
                pickUp.transform.SetParent(this.SpawnRoot, false);

                Vector3 position = new Vector3();
                position.x = UnityEngine.Random.Range(5, 25) * Mathf.Sign(UnityEngine.Random.value - .5f);
                position.z = UnityEngine.Random.Range(5, 25) * Mathf.Sign(UnityEngine.Random.value - .5f);
                pickUp.transform.position = position;

                this.SpawnTime = this.Data.SpawnTime;
                this.PickUps.Add(pickUp);

                pickUp.OnDespawn += this.PickUp_OnDespawn;
            }
        }

        private void PickUp_OnDespawn(WeaponPickUp weaponPickUp)
        {
            this.PickUps.Remove(weaponPickUp);
        }
    }
}
