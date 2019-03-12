using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterControll : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed=10;
    //If the position between player and mouse is to small, stop movement
    public float minDistance=1;

    public float maxLive = 100;
    private Rigidbody2D rigidBody;
    private Vector2 moveVelocity;
    private Vector2 rawMousePosition=new Vector2();
    private Vector2 mousePosition=new Vector2();
    private float moveRotation;
    private float rotation;
    private float currentLive = 100;
    private enum Direction
    {
        VERTICAL,HORIZONTAL
    };

    public enum Player
    {
        //Currently only P1 and P2 are working, since there is no P3+P4 input defined in edit->preferences->Input(But this can be simply added).
        P1 = 1,P2=2,P3=3,P4=4
    };
    private Direction direction;
    public Player player = Player.P1;

    
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentLive = maxLive;
        //Update the strings above the player
        BroadcastMessage("updateLive", currentLive);
        BroadcastMessage("updatePlayerName", player);
        

    }
    /**
     * angle in Rad
     * */
    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
    void updateMouse()
    {
        Vector2 difference = mousePosition - rigidBody.position;
        rotation = Mathf.Rad2Deg * Mathf.Atan2(difference.y, difference.x);
        //Check if the mouse was moved.
       //If the mouse was moved, update the walking direction
        if (!rawMousePosition.Equals(Input.mousePosition))
        {

            rawMousePosition = Input.mousePosition;
            var v3 = Input.mousePosition;
            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);

            mousePosition = new Vector2(v3.x, v3.y);
            moveRotation = rotation;
        }
    }

    internal void updateWeapon(MachineGun machineGun)
    {
        BroadcastMessage("updateWeaponState",machineGun);
    }


    void Update()
    {

        //In edit->preferences->Input you can change the keys for the specific input.
        //We have currently 3 inputs Vertical,Horizontal and shoot.
        //The name of each input is simply player_Input.
        //Example for player 1 the vertical movement name is P1_Vertical. This must match with the name of the key in edit->preferences->Input.
        
        string vertical = player + "_Vertical";
        string horizontal = player + "_Horizontal";

        updateMouse();
        checkShot();

        //If the position between player and mouse is to small, stop movement
        if (Vector2.Distance(mousePosition, rigidBody.position) < minDistance)
        {
            moveVelocity = Vector2.zero;
       
        }
        else
        {
           //If you add controller support, probably the whole code can be removed
           Direction newDirection;
            if (Input.GetButton(vertical))
            {
                if (direction != Direction.VERTICAL)
                {
                    moveRotation = rotation;
                }
                direction = Direction.VERTICAL;
                Vector2 moveInput = RotateVector(new Vector2(1, 0) * Input.GetAxis(vertical), moveRotation);
                moveVelocity = moveInput.normalized * speed;
            }
            else if (Input.GetButton(horizontal))
            {
                if (direction != Direction.HORIZONTAL)
                {
                    moveRotation = rotation;
                }
                direction = Direction.HORIZONTAL;

                Vector2 moveInput = RotateVector(new Vector2(0, -1) * Input.GetAxis(horizontal), moveRotation);
                moveVelocity = moveInput.normalized * speed;
            }
            else
            {
                moveVelocity = Vector2.zero;

            }
        }
        if (currentLive <= 0)
        {
            respawn();
        } else
        {
            Vector2 newPosition = rigidBody.position + moveVelocity * Time.deltaTime;
            rigidBody.MovePosition(newPosition);
            rigidBody.MoveRotation(rotation);
        }

    }

    private void respawn()
    {
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");
        if (respawns.Length > 0)
        {
            GameObject respawn = respawns[Random.Range(0, respawns.Length)];
            rigidBody.MovePosition(respawn.GetComponent<Transform>().position);
        }
        setLive(maxLive);
        BroadcastMessage("onPlayerRespawn", this);

    }

    private void checkShot()
    {
        string fire = player + "_Fire";
        if (Input.GetButton(fire))
        {
            BroadcastMessage("TryToFire");
        }
    }


    public void ApplyDamage(float damage)
    {
        setLive(currentLive - damage);

    }

    private void setLive(float newLive)
    {
        currentLive = newLive;
        BroadcastMessage("updateLive", currentLive);
    }
}
