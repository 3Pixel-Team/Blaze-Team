using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Initializers

    public NavMeshAgent agent;
    public float touchPlayerRotation = 0.2f;

    public float speed = .5f;
    public float rotationSpeed = 10f;
    public new Camera camera;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    private Vector3 KeyboardInputMovement;
    public FieldOFView fieldOFView;

    public bool keyboardPlay;
    UIGameplayManager uiGameplay => UIGameplayManager.Instance;

    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        camera = Camera.main;

        fieldOFView = GetComponent<FieldOFView>();
    }

    void Update()
    {


        #region Joystick and Movement Controllers
        //gets the movement of the joystick
        if (!keyboardPlay) {
            if(uiGameplay == null) return;
            if (uiGameplay.joystick.Horizontal >= 0.3f)
            {
                horizontalMove = speed;
            }
            else if (uiGameplay.joystick.Horizontal <= -0.3f)
            {
                horizontalMove = -speed;
            }
            else
            {
                horizontalMove = 0f;
            }

            if (uiGameplay.joystick.Vertical >= 0.3f)
            {
                verticalMove = speed;
            }
            else if (uiGameplay.joystick.Vertical <= -0.3f)
            {
                verticalMove = -speed;
            }
            else
            {
                verticalMove = 0f;
            }

            //gets the movement and moves the agent
            Vector3 movement = new Vector3(horizontalMove, 0f, verticalMove);
            Vector3 moveDestination = transform.position + movement;
            agent.destination = moveDestination;
            
            if (movement != Vector3.zero)
            {
                Vector3 dir = moveDestination - transform.position;
                
                if (fieldOFView.getClosestEnemy() == null)
                {
                    dir.y = 0;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
                }
                else if (fieldOFView.getClosestEnemy())
                {
                    transform.LookAt(fieldOFView.getClosestEnemy().transform.position);
                }
            }
            if(fieldOFView.getClosestEnemy() != null && !uiGameplay.touchField.Pressed)
            {
                transform.LookAt(fieldOFView.getClosestEnemy().transform.position);
            }
        }




        #endregion

        #region Mouse and Keyboard Controllers
        // I dont have a smartphone, have to use keyboard input.
        if (keyboardPlay)
        {
            KeyboardInputMovement.x = (Input.GetAxisRaw("Horizontal"));
            KeyboardInputMovement.z = (Input.GetAxisRaw("Vertical"));

            Vector3 moveDestination = transform.position + KeyboardInputMovement;
            agent.destination = moveDestination;

            Ray CameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLenght;

            if (!fieldOFView.getClosestEnemy())
            {
                if (groundPlane.Raycast(CameraRay, out rayLenght))
                {
                    Vector3 pointToLook = CameraRay.GetPoint(rayLenght);

                    transform.LookAt(pointToLook);
                }
            }
            else if (fieldOFView.getClosestEnemy())
            {
               
                transform.LookAt(fieldOFView.getClosestEnemy().transform.position);
            }
        }
       
        
        #endregion

        #region Rotation

        if (uiGameplay.touchField.Pressed)
        {
            Vector3 dir = uiGameplay.touchField.TouchDist;
            Vector3 rot = new Vector3(0, dir.x);
            transform.Rotate(rot);
        }


        #endregion
    }
}
