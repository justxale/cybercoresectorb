using System;
using UnityEngine;

namespace Objects
{
    public class JumpPad : MonoBehaviour
    {
        public float jumpStrength;
        public float jumpDegree;

        private void Start()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var rb = other.collider.GetComponent<Rigidbody2D>();
            if (other.collider.GetComponent<Rigidbody2D>() != null)
            {
                rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            }
        }
    }
}
