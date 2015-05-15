/**
 * File : PlayerControl.cs
 * Author : Balakumar Marimuthu
 * Use : The script in the file manages the player's activities
 **/

using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public enum DirectionInput{
        Null, Left, Right, Up, Down, LongJump
    }

    public enum Position
    {
        Middle, Left, Right
    }

    public float gravity = 9.89f;
    public float speedMove = 10;
    public float jumpHeight = 8;

    public Transform groundControl;
        
    private bool activeInput;

    [HideInInspector]
    public CharacterController characterController;

    private InfiniteGenerator ground;

    private AnimationManager animationManager;

    private Vector3 amountMove;
    private Vector3 direction;

    private bool playerDead;

    [HideInInspector]
    public bool isRoll;
    [HideInInspector]
    public bool isJumping;
    [HideInInspector]
    public bool isLongJump;
    
    private bool isRunning;

    private float distanceMoved;

    public bool keyInput;
    public bool touchInput;

    private Vector2 currentPos;

    private DirectionInput directInput;

    private Position positionStand;

    [HideInInspector]
    public static PlayerControl instance;

    void Start()
    {
        instance = this;

        ground = groundControl.GetComponent("InfiniteGenerator") as InfiniteGenerator;
        animationManager = this.GetComponent<AnimationManager>();
        characterController = this.GetComponent<CharacterController>();

        isJumping = false;
        isRoll = false;
        isRunning = true;
        distanceMoved = 0.0f;

        playerDead = false;

        speedMove = GameAttribute.gameAttribute.speed;
        
        Invoke("WaitStart", 0.2f);
    }

    void WaitStart()
    {
        StartCoroutine(UpdateAction());
    }


    IEnumerator UpdateAction()
    {
        while (GameAttribute.gameAttribute.life > 0 && GameAttribute.gameAttribute.healthVal > 0)
        {
            if (!InfiniteGenerator.instance.isLoadingComplete)
                continue;

            if (!GameAttribute.gameAttribute.pause && GameAttribute.gameAttribute.isPlaying)
            {
                KeyInput();
                TouchInput();
                CheckLane();
                MoveForward();
            }
            else
            {
                animation.Stop();
            }
            yield return 0;
        }

        animationManager.animationState = animationManager.Dead;
        GameAttribute.gameAttribute.isPlaying = false;

        this.transform.position = new Vector3(-100, -100, -100);

        Application.LoadLevel("GameOver");
        
        yield return new WaitForSeconds(2);
    }
	

    private void MoveForward()
    {
        speedMove = GameAttribute.gameAttribute.speed;

        if (characterController.isGrounded)
        {
            if (!isRunning)
            {
                SoundManager.instance.resumeRunningSound();
                animationManager.animationState = animationManager.Run;
                isRunning = true;
                isJumping = false;
                isLongJump = false;
            }
            amountMove = Vector3.zero;
            if (directInput == DirectionInput.Up)
            {
                Jump();
            }
            else if (directInput == DirectionInput.LongJump)
            {
                LongJump();
            }
        }
        amountMove.z = 0;
        amountMove += this.transform.TransformDirection(Vector3.forward * speedMove);
        amountMove.y -= gravity * Time.deltaTime;

        distanceMoved += (amountMove.z + direction.z) * Time.deltaTime;
        if (distanceMoved > 1.0f)
        {
            GameAttribute.gameAttribute.updateScore(distanceMoved);
            distanceMoved = 0.0f;
        }
        
        characterController.Move((amountMove + direction) * Time.deltaTime);
    }

    void Jump()
    {
        if (isRunning)
        {
            SoundManager.instance.PlaySound("jump");
            isRunning = false;
            isLongJump = false;
            isRoll = false;
            isJumping = true;

            animationManager.animationState = animationManager.Jump;

            amountMove.y += jumpHeight;
        }
    }

    void LongJump()
    {
        if (isRunning) {
            SoundManager.instance.PlaySound("jump");
            isRunning = false;
            isLongJump = true;
            isRoll = false;

            animationManager.animationState = animationManager.LongJump;

            amountMove.y += jumpHeight;
        }
    }

    //Key input method
    private void KeyInput()
    {
        if (Input.GetKeyDown("left"))
        {
            directInput = DirectionInput.Left;
        }
        else if (Input.GetKeyDown("right"))
        {
            directInput = DirectionInput.Right;
        }
        else if (Input.GetKeyDown("up"))
        {
            directInput = DirectionInput.Up;
        }
        /*else if (Input.GetKeyDown("down"))
        {
            directInput = DirectionInput.Down;
        }*/
        else if (Input.GetKeyDown("space"))
        {
            directInput = DirectionInput.LongJump;
        }
        else
        {
            directInput = DirectionInput.Null;
        }
    }

    private float GetAngle(Vector3 form, Vector3 to)
    {
        Vector3 nVector = Vector3.zero;
        nVector.x = to.x;
        nVector.y = form.y;
        float a = to.y - nVector.y;
        float b = nVector.x - form.x;
        float tan = a / b;
        return RadToDegree(Mathf.Atan(tan));
    }

    private float RadToDegree(float radius)
    {
        return (radius * 180) / Mathf.PI;
    }

    private void TouchInput()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            currentPos = Input.mousePosition;
            activeInput = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (activeInput)
            {
                float ang = GetAngle(currentPos, Input.mousePosition);
                if ((Input.mousePosition.x - currentPos.x) > 30)
                {
                    if (ang < 45 && ang > -45)
                    {
                        directInput = DirectionInput.Right;
                        activeInput = false;
                    }
                    else if (ang >= 45)
                    {
                        directInput = DirectionInput.Up;
                        activeInput = false;
                    }
                    /*else if (ang <= -45)
                    {
                        directInput = DirectionInput.Down;
                        activeInput = false;
                    }*/
                }
                else if ((Input.mousePosition.x - currentPos.x) < -30)
                {
                    if (ang < 45 && ang > -45)
                    {
                        directInput = DirectionInput.Left;
                        activeInput = false;
                    }
                    /*else if (ang >= 45)
                    {
                        directInput = DirectionInput.Down;
                        activeInput = false;
                    }*/
                    else if (ang <= -45)
                    {
                        directInput = DirectionInput.Up;
                        activeInput = false;
                    }
                }
                else if ((Input.mousePosition.y - currentPos.y) > 20)
                {
                    if ((Input.mousePosition.x - currentPos.x) > 0)
                    {
                        if (ang > 45 && ang <= 90)
                        {
                            directInput = DirectionInput.Up;
                            activeInput = false;
                        }
                        else if (ang <= 45 && ang >= -45)
                        {
                            directInput = DirectionInput.Right;
                            activeInput = false;
                        }
                    }
                    else if ((Input.mousePosition.x - currentPos.x) < 0)
                    {
                        if (ang < -45 && ang >= -89)
                        {
                            directInput = DirectionInput.Up;
                            activeInput = false;
                        }
                        else if (ang >= -45)
                        {
                            directInput = DirectionInput.Left;
                            activeInput = false;
                        }
                    }
                }
                else if ((Input.mousePosition.y - currentPos.y) < -10)
                {
                    if ((Input.mousePosition.x - currentPos.x) > 0)
                    {
                        /*if (ang > -89 && ang < -45)
                        {
                            directInput = DirectionInput.Down;
                            activeInput = false;
                        }*/
                        if (ang >= -45)
                        {
                            directInput = DirectionInput.Right;
                            activeInput = false;
                        }
                    }
                    else if ((Input.mousePosition.x - currentPos.x) < 0)
                    {
                        /*if (ang > 45 && ang < 89)
                        {
                            directInput = DirectionInput.Down;
                            activeInput = false;
                        }*/
                        if (ang <= 45)
                        {
                            directInput = DirectionInput.Left;
                            activeInput = false;
                        }
                    }

                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            directInput = DirectionInput.Null;
            activeInput = false;
        }
    }

    private void CheckLane()
    {
        if (positionStand == Position.Middle)
        {
            if (directInput == DirectionInput.Right)
            {
                positionStand = Position.Right;
                //Play sfx when step
                SoundManager.instance.PlaySound("Step");
            }
            else if (directInput == DirectionInput.Left)
            {
                positionStand = Position.Left;
                //Play sfx when step
                SoundManager.instance.PlaySound("Step");
            }

            if (transform.position.x > 0.05f)
            {
                direction = Vector3.Lerp(direction, Vector3.left * 6, Time.deltaTime * 500);
            }
            else if (transform.position.x < -0.05f)
            {
                direction = Vector3.Lerp(direction, Vector3.right * 6, Time.deltaTime * 500);
            }
            else
            {
                direction = Vector3.zero;
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, (transform.position.z)), 6 * Time.deltaTime);
            }
        }
        else if (positionStand == Position.Left)
        {
            if (directInput == DirectionInput.Right)
            {
                positionStand = Position.Middle;
                //Play sfx when step
                SoundManager.instance.PlaySound("Step");
            }
            if (transform.position.x > -2.0f)
            {
                direction = Vector3.Lerp(direction, Vector3.left * 6, Time.deltaTime * 500);
            }
            else
            {
                direction = Vector3.zero;
                transform.position = Vector3.Lerp(transform.position, new Vector3(-2.0f, transform.position.y, (transform.position.z)), 6 * Time.deltaTime);
            }
        }
        else if (positionStand == Position.Right)
        {
            if (directInput == DirectionInput.Left)
            {
                positionStand = Position.Middle;
                //Play sfx when step
                SoundManager.instance.PlaySound("Step");
            }
            if (transform.position.x <= 2.0f)
            {
                direction = Vector3.Lerp(direction, Vector3.right * 6, Time.deltaTime * 500);
            }
            else
            {
                direction = Vector3.zero;
                transform.position = Vector3.Lerp(transform.position, new Vector3(2.0f, transform.position.y, (transform.position.z)), 6 * Time.deltaTime);
            }
        }

        if (directInput == DirectionInput.Down)
        {
            if (isRunning)
            {
                SoundManager.instance.PlaySound("Roll");
                isJumping = false;
                isLongJump = false;
                isRunning = false;
                isRoll = true;

                animationManager.animationState = animationManager.LongJump;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Item")
        {
            col.GetComponent<GameItem>().ItemGet();
        }
    }
}
