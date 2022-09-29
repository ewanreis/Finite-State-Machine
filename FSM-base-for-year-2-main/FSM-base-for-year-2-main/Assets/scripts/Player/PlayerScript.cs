using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator anim;
        Collision col;
        public SpriteHelper sh;
        public LayerMask platformLayerMask;
        public Collider2D platformCollider;
        
        [HideInInspector]
        public bool onPlatform;
        [HideInInspector]
        public bool jumpFlag, jumpButtonPressed, jumpButtonReleased;
        [HideInInspector]

        public bool shootButtonPressed, shootButtonReleased;
        [HideInInspector]
        public bool crouchButtonPressed, crouchButtonReleased;
        [HideInInspector]
        public bool upButtonPressed, downButtonPressed;
        [HideInInspector]
        public bool moveButtonPressed, moveButtonReleased;

        public float fall = 0.2f;
        public float jumpGravity = 0.6f;
        public float initialJumpVel = 10f;
        public float xv, yv;

        Dir lastDir;
        Dir currentDir;

        public GameObject lanceWeapon;


        public float runSpeed = 6;

        

        LevelManager lm;


        // variables holding the different player states
        public StandingState standingState;
        public CrouchingState crouchingState;
        public JumpingState jumpingState;
        public MovingState movingState;
        public CrouchShootState crouchShootState;
        public FallingState fallingState;

        public StateMachine sm;


        private void Awake()
        {
            sh = gameObject.AddComponent<SpriteHelper>();
        }




        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            col = gameObject.AddComponent<Collision>();
            lm = LevelManager.lm;
            sm = gameObject.AddComponent<StateMachine>();

            rb.gravityScale = 10f;
            rb.mass = 100f;

            // add new states here
            standingState = new StandingState(this, sm);
            crouchingState = new CrouchingState(this, sm);
            jumpingState  = new JumpingState(this, sm);
            movingState = new MovingState(this, sm);
            crouchShootState = new CrouchShootState(this, sm);
            fallingState = new FallingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(standingState);

            
            sh.SetSpriteXDirection(Dir.Right);

            

        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();

            //output debug info to the canvas
            string s;
            s = string.Format("onplat={0} jumpFlag={1}\nlast state={2}\ncurrent state={3}", onPlatform, jumpFlag, sm.LastState, sm.CurrentState);
            UIscript.ui.DrawText(s);

            s = string.Format("current dir2={0} lastdir={1} yv={2}", currentDir, lastDir, yv);
            UIscript.ui.DrawText(s);

            s = string.Format("shoot button={0} ", shootButtonPressed);
            UIscript.ui.DrawText(s);

            // Press R to reset the player's position
            DebugPlayer();

        }



        void FixedUpdate()
        {
            
            // this is called for all states
            
            col.CheckTileCollisionPlatform(platformLayerMask, 0.38f, 0.4f, 0.11f);

            onPlatform = col.PlatformHit();
            //onPlatform = (this.GetComponent<Collider2D>().IsTouching(platformCollider)) ? true : false;
            
            col.ShowDebugCollisionPoints();

            sm.CurrentState.PhysicsUpdate();
            rb.velocity = new Vector2(xv, yv);
        }


        public void CheckForLand()
        {
            Vector2 pos = rb.position;

            // check for landing on a platform

            if ((yv <= 1) && (onPlatform == true))
            {
                yv = 0;
                rb.velocity = new Vector2(0, 0);

                jumpFlag = false;

                // round to 0.5
                pos.y = Mathf.Round(pos.y);

                //pos.y = (Mathf.Round(pos.y * 2)) / 2;

                print("Landed! y was=" + transform.position.y + "  and is now " + pos.y);
                sm.ChangeState(standingState);

                rb.transform.position = pos;
            }
            
        }

        public IEnumerator JumpUpCoroutine()
        {
            yield return new WaitForSeconds(0.2f);
            yv = 0;
            CheckForLand();
            sm.ChangeState(fallingState);
            yield return null;
        }



        public void CheckForStand()
        {
            if (onPlatform == true)
            {

                if (Input.GetKey("left") == false) // key held down
                {
                    if (Input.GetKey("right") == false) // key held down
                    {
                        sm.ChangeState(standingState);
                    }
                }

            }

            // check for changing direction
            if (currentDir != lastDir)
            {
                // player has changed direction
                sm.ChangeState(standingState);
            }
        }



        public void ReadInputKeys()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                crouchButtonPressed = true;
                crouchButtonReleased = false;
            }
            else
            {
                crouchButtonPressed = false;
                crouchButtonReleased = true;
            }

            if (Input.GetKey(KeyCode.LeftControl) && (shootButtonReleased == true))
            {
                shootButtonReleased = false;
                shootButtonPressed = true;
            }
            else
            {
                shootButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.LeftControl) == false)
            {
                shootButtonReleased = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                jumpButtonPressed = true;
                jumpFlag = true;
            }
            else
            {
                jumpButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                upButtonPressed = true;
            }
            else
            {
                upButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                downButtonPressed = true;
            }
            else
            {
                downButtonPressed = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                moveButtonPressed = true;
                moveButtonReleased = false;
            }
            else
            {
                moveButtonPressed = false;
                moveButtonReleased = true;
            }

        }



        void DebugPlayer()
        {
            // reset player position with "R" key
            if (Input.GetKeyDown("r"))
            {
                gameObject.transform.position = new Vector2(-7, 4);
                rb.velocity = new Vector2(0, 0);
                xv = yv = 0;
            }

        }

    }

}