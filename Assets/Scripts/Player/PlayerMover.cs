using Cysharp.Threading.Tasks;
using Rigidbody;
using ScriptableDatas;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] float baseY;
        [SerializeField] PlayerMoveData moveData;
        [SerializeField] PlayerInput input;
        [SerializeField] IsLandingManeger landingManeger;
        [SerializeField] bool debug_canMoveX = true;
        AddForce mover;
        JumpAnimator jumper;
        GrideAnimation grider;
        //BoolDictionary canMove = new BoolDictionary();
        bool isPionterDown = false;
        bool isInputbuttonDown = false;

        string GUIstr = "";

        private void OnEnable()
        {
            input.actions["Move"].started += MovebyInputAction;
            input.actions["Move"].canceled += JumpbyInputAction;
        }

        private void OnDisable()
        {
            if (input == null) return;
            input.actions["Move"].started -= MovebyInputAction;
            input.actions["Move"].canceled -= JumpbyInputAction;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null) throw new System.NullReferenceException(gameObject.name + "Rigidbody");
            if (moveData == null) throw new System.NullReferenceException(gameObject.name + "MoveData");
            if (landingManeger == null) throw new System.NullReferenceException(gameObject.name + "IsLandingManeger");

            mover = new AddForce(moveData.move.force, rigidbody);
            jumper = new JumpAnimator(moveData.jump, transform, rigidbody, baseY);

            rigidbody.mass = moveData.mass;
        }

        void Move()
        {
            if (!isPionterDown && !isInputbuttonDown) return;
            if (!landingManeger.isLanding) return;
            GUIstr = "Move";
            if (!debug_canMoveX) return;
            mover.AddForceToMaxSpeed(Vector2.right, moveData.move.maxSpeed);
            //mover.AddSpeed(Vector2.right);
        }

        void Jump()
        {
            if (!landingManeger.isLanding) return;
            jumper.CallJump();
            GUIstr = "Jump";
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            GUIstr = "";
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

        private void OnGUI()
        {
            GUILayout.Label("Player Move Event:" + GUIstr);
        }
    }

    [System.Serializable]
    public class JumpAnimator
    {
        private readonly float basePositionY, timeToVertex, height, timeToSus, grideDownSpeed;
        private float distance, fromPosition;
        private readonly Transform targetTransform;
        private readonly Rigidbody2D rigidbody;
        public bool isJumping = false;

        public JumpAnimator(PlayerMoveData.JumpStruct moveData, Transform targetTransform, Rigidbody2D rigidbody, float basePositionY)
        {
            this.timeToVertex = moveData.timeToVertex;
            this.height = moveData.height;
            this.timeToSus = moveData.timeToSus;
            this.grideDownSpeed = moveData.grideDownSpeed;
            if (timeToVertex < 0) Debug.LogError("Please set jumpUpTime to 0 or higher.");
            this.targetTransform = targetTransform;
            this.rigidbody = rigidbody;
            this.basePositionY = basePositionY;
        }

        // ƒ{ƒ^ƒ“‚ª‰Ÿ‚³‚ê‚½Žž‚Éˆê‰ñ‚¸‚ÂŒÄ‚Ño‚·
        public async void CallJump()
        {
            if (isJumping) return;
            isJumping = true;
            fromPosition = targetTransform.position.y;
            distance = basePositionY - targetTransform.position.y + height;
            float originalGravity = rigidbody.gravityScale;
            rigidbody.gravityScale = 0;
            await Jump();
            await Sustain();
            rigidbody.gravityScale = originalGravity;
            isJumping = false;
        }

        private async UniTask Jump()
        {
            rigidbody.linearVelocityY = MathF.Max(0, rigidbody.linearVelocityY);
            float a = -distance / timeToVertex / timeToVertex;
            for (float timer = 0; timer < timeToVertex; timer += Time.deltaTime)
            {
                float x = timer - timeToVertex;
                float moveDistance = a * x * x + distance;
                rigidbody.linearVelocityY = 2 * a * x;
                await UniTask.NextFrame();
            }
        }

        private async UniTask Sustain()
        {
            for (float timer = 0; timer < timeToSus; timer += Time.deltaTime)
            {
                rigidbody.linearVelocityY = -grideDownSpeed;
                await UniTask.NextFrame();
            }
        }
    }

    [System.Serializable]
    public class GrideAnimation
    {
        private readonly float maxAirTime;
        private readonly float gravityScale;
        private readonly float grideSpeed;
    }
}

namespace Rigidbody
{
    [Serializable]
    public class AddForce
    {
        readonly float force;
        readonly Rigidbody2D rigidbody;

        public AddForce(float force, Rigidbody2D rigidbody)
        {
            if (rigidbody == null) throw new System.NullReferenceException();
            this.force = force;
            this.rigidbody = rigidbody;
        }

        public void AddImpulseForce(Vector2 direction)
        {
            rigidbody.AddForce(force * direction, ForceMode2D.Impulse);
        }

        public void AddForceToMaxSpeed(Vector2 direction, float maxSpeed)
        {
            if (rigidbody.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed) return;
            rigidbody.AddForce(force * direction);
        }

        public void AddSpeed(Vector2 direction)
        {
            rigidbody.linearVelocity += force * Time.fixedDeltaTime * direction;
        }
    }
}
