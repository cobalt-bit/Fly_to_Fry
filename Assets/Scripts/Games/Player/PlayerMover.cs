using Cysharp.Threading.Tasks;
using Rigidbody;
using ScriptableDatas;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        public PlayerMoveData moveData;
        [SerializeField] PlayerBaseYDataScript baseY;
        PlayerInput input;
        public IsLandingManeger landingManeger;
        [SerializeField] bool debug_canMoveX = true;
        new Rigidbody2D rigidbody;
        EventTrigger trigger;
        EventTrigger.Entry[] entryEvents = new EventTrigger.Entry[2];
        AddForce mover;
        JumpAnimator jumper;
        bool isPionterDown = false;
        bool isInputbuttonDown = false;

        public event Action moveEvent, jumpEvent;

        string GUIstr = "";

        private void Awake()
        {
            getEventTrigger();
            setEntryEvents();
            getPlayerInput();
        }

        void getEventTrigger()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Controller");
            foreach (GameObject obj in objs)
            {
                if (obj.TryGetComponent(out trigger)) break;
            }
            return;
        }

        void setEntryEvents()
        {
            for (int i = 0; i < entryEvents.Length; ++i) entryEvents[i] = new();
            entryEvents[0].eventID = EventTriggerType.PointerDown;
            entryEvents[1].eventID = EventTriggerType.PointerUp;
            entryEvents[0].callback.AddListener((_) => MovebyPointer());
            entryEvents[1].callback.AddListener((_) => JumpbyPointer());
        }

        void getPlayerInput()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Controller");
            foreach(GameObject obj in objs)
            {
                if (obj.TryGetComponent(out input)) break;
            }
            return;
        }

        private void OnEnable()
        {
            input.actions["Move"].started += MovebyInputAction;
            input.actions["Move"].canceled += JumpbyInputAction;

            trigger.triggers.Add(entryEvents[0]);
            trigger.triggers.Add(entryEvents[1]);
        }

        private void OnDisable()
        {
            if (input == null) return;
            input.actions["Move"].started -= MovebyInputAction;
            input.actions["Move"].canceled -= JumpbyInputAction;

            foreach(EventTrigger.Entry event_ in entryEvents)
                trigger.triggers.Remove(event_);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null) throw new System.NullReferenceException(gameObject.name + "Rigidbody");
            if (moveData == null) throw new System.NullReferenceException(gameObject.name + "MoveData");
            if (landingManeger == null) throw new System.NullReferenceException(gameObject.name + "IsLandingManeger");

            mover = new AddForce(moveData.move.force, rigidbody);
            jumper = new JumpAnimator(moveData.jump, transform, rigidbody, baseY.surfaceHeight);
        }

        void Move()
        {
            if (!landingManeger.isLanding) return;
            mover.AddForceToMaxSpeed(moveData.startMove.force * Vector2.right, moveData.startMove.maxSpeed);
            if (!isPionterDown && !isInputbuttonDown)
            {
                if (rigidbody.linearVelocityX < moveData.startMove.maxSpeed) return;
                if (jumper.isJumping) return;
                rigidbody.AddForceX(-moveData.move.breakForce);
                return;
            }
            GUIstr = "Move";
            if (!debug_canMoveX) return;
            moveEvent?.Invoke();
            mover.AddForceToMaxSpeed(Vector2.right, moveData.move.maxSpeed);
        }

        void Jump()
        {
            if (!landingManeger.isLanding) return;
            jumpEvent?.Invoke();
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
            isPionterDown = true;
        }

        public void JumpbyPointer()
        {
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

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUILayout.Label("Player Move Event:" + GUIstr);
        }
#endif
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
            try
            {
                await Jump();
                await Sustain();
            } catch (OperationCanceledException) { }

            if (rigidbody != null)
                rigidbody.gravityScale = originalGravity;
            isJumping = false;
        }

        private async UniTask Jump()
        {
            float a = -distance / timeToVertex / timeToVertex;
            for (float timer = 0; timer < timeToVertex; timer += Time.deltaTime)
            {
                float x = timer - timeToVertex;
                float moveDistance = a * x * x + distance;
                if (rigidbody == null) return;
                rigidbody.linearVelocityY = 2 * a * x;
                await UniTask.NextFrame();
            }
        }

        private async UniTask Sustain()
        {
            for (float timer = 0; timer < timeToSus; timer += Time.deltaTime)
            {
                if (rigidbody == null) return;
                rigidbody.linearVelocityY = -grideDownSpeed;
                await UniTask.NextFrame();
            }
        }
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
