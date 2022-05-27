using UnityEngine;

namespace Motion
{
    public class MoveInfo : MonoBehaviour
    {
        public Move Move { get; private set; }

        Vector3 LastPosition { get; set; }

        private float Distance { get; set; }
        private float Passed { get; set; }

        private void Start()
        {
            this.Move = this.GetComponent<Move>();

            if (this.Move == null)
                this.enabled = false;
        }

        private void Update()
        {
            float add = Time.deltaTime;
            this.Passed += add;

            Vector3 current = this.Move.transform.position;
            Vector3 delta = this.LastPosition - current;

            this.Distance += delta.magnitude;

            this.LastPosition = current;

            if (this.Passed >= 1)
            {
                Debug.Log(this.Distance / this.Passed);

                this.Distance = 0;
                this.Passed = 0;
            }
        }
    }
}