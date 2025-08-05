using Bitools;
using Datas;
using Rigidbody;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] PlayerMoveData moveData;
        [SerializeField] PlayerInput input;
        AddForceToMaxSpeed mover;
        AddForce jumper;
        //BoolDictionary canMove = new BoolDictionary();
        bool isPionterDown = false;
        bool isInputbuttonDown = false;

        private void OnEnable()
        {
            input.actions["Move"].started += MovebyInputAction;
            input.actions["Move"].canceled += JumpbyInputAction;
        }

        private void OnDisable()
        {
            input.actions["Move"].started -= MovebyInputAction;
            input.actions["Move"].canceled -= JumpbyInputAction;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null) throw new NullReferenceException(gameObject.name);
            if (moveData == null) throw new NullReferenceException(gameObject.name);

            mover = new AddForceToMaxSpeed(moveData.moveForce, moveData.moveMaxSpeed, rigidbody);
            jumper = new AddForce(moveData.jumpForce, rigidbody);
        }

        void Move()
        {
            if (!isPionterDown && !isInputbuttonDown) return;
            Debug.Log("Move");
            //mover.AddForce(Vector2.right);
        }

        void Jump()
        {
            Debug.Log("Jump");
            //jumper.Addforce(Vector2.up);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            Move();
        }

        public void MovebyPointer()
        {
            //canMove.Set("isPointerDown", true);
            isPionterDown = true;
        }

        public void JumpbyPointer()
        {
            //canMove.Set("isPointerDown", false);
            isPionterDown = false;
            Jump();
        }

        private void MovebyInputAction(InputAction.CallbackContext context)
        {
            isInputbuttonDown = true;
        }
        private void JumpbyInputAction(InputAction.CallbackContext context)
        {
            isInputbuttonDown = false;
            Jump();
        }
    }
}

namespace Rigidbody
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
            rigidbody.AddForce(force * direction, ForceMode2D.Impulse);
        }
    }
}
