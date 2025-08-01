using Rigidboby;
using System;
using UnityEngine;
using Datas;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] PlayerMoveData moveData;
        AddForceToMaxSpeed mover;
        AddForce jumper;
        bool isPointerDown = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null) throw new NullReferenceException(gameObject.name);
            if (moveData == null) throw new NullReferenceException(gameObject.name);

            mover = new AddForceToMaxSpeed(moveData.moveForce, moveData.moveMaxSpeed, rigidbody);
            jumper = new AddForce(moveData.jumpForce, rigidbody);
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
        readonly float force;
        readonly float maxSpeed;
        readonly Rigidbody2D rigidbody;

        public AddForceToMaxSpeed(float force, float maxSpeed, Rigidbody2D rigidbody)
        {
            this.force = force;
            this.maxSpeed = maxSpeed;
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
        readonly float force;
        readonly Rigidbody2D rigidbody;

        public AddForce(float force, Rigidbody2D rigidbody)
        {
            this.force = force;
            this.rigidbody = rigidbody;
        }

        public void Addforce(Vector2 direction)
        {
            rigidbody.AddForce(force * direction);
        }
    }
}
