using Rigidboby;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] AddForceToMaxSpeed mover = new();
        [SerializeField] AddForce jumper = new();
        bool isPointerDown = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null) throw new NullReferenceException(gameObject.name);
            mover.Initialize(rigidbody);
            jumper.Initialize(rigidbody);
        }

        void Moving()
        {
            if (!isPointerDown) return;
            Debug.Log("Moving");
            //mover.AddForce(Vector2.right);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Moving();
        }

        public void Move()
        {
            isPointerDown = true;
        }

        public void Jump()
        {
            Debug.Log("Jump");
            isPointerDown = false;
            //jumper.Addforce(Vector2.up);
        }
    }
}

namespace Rigidboby
{
    [Serializable]
    public class AddForceToMaxSpeed
    {
        [SerializeField] float force;
        [SerializeField] float maxSpeed;

        Rigidbody2D rigidbody;

        public void Initialize(Rigidbody2D rigidbody)
        {
            this.rigidbody = rigidbody;
        }

        public void AddForce(Vector2 direction)
        {
            if (rigidbody == null) return;
            if (rigidbody.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed) return;
            rigidbody.AddForce(force * Time.deltaTime * direction);
        }
    }

    [Serializable]
    public class AddForce
    {
        [SerializeField] float force;

        Rigidbody2D rigidbody;

        public void Initialize(Rigidbody2D rigidbody)
        {
            this .rigidbody = rigidbody;
        }

        public void Addforce(Vector2 direction)
        {
            rigidbody.AddForce(force * direction);
        }
    }
}
