using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovimientoZombie : MonoBehaviour
{

    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_RotationSpeed;
    [SerializeField]
    private float m_JumpForce;
    Vector3 m_Movement = Vector3.zero;
    private Rigidbody m_Rigidbody;
    Animator m_Animator;
    private ControlesPlayer m_ControlesPlayer;
    private bool m_Aterra;
    public static bool m_OnAir;
    public FSM m_FSM;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    public LayerMask LayermaskMGround;
    private bool vivo;
    private bool ganador;


    private void Awake()
    {

        ganador = false;
        vivo = true;
        transform.position = new Vector3(0.14f, 3.39f, 2.91f);
        m_ControlesPlayer=new ControlesPlayer();
        m_ControlesPlayer.Player.Salto.started += YoDigoSaltaConmigo;
        m_ControlesPlayer.Player.Movimiento.canceled += StopMovement;
        m_ControlesPlayer.Enable();
        m_Aterra = true;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_FSM = new FSM(gameObject);
        m_FSM.AddState(new IdleState(m_FSM, "Z_Idle"));
        m_FSM.AddState(new Saltar(m_FSM, "Z_Attack"));
        m_FSM.AddState(new CaminarLento(m_FSM, "Z_Walk1_InPlace"));
        m_FSM.ChangeState<IdleState>();
    }
    private void StopMovement(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            m_Movement = Vector3.zero;
        }
    }

    private void YoDigoSaltaConmigo(InputAction.CallbackContext obj)
    {
        bool pepe = CheckGround();
        print(pepe + " ete e sech");
        if (pepe)
        {
            Debug.Log("Jumping");
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce, ForceMode.Impulse);
        }
    }
    public bool CheckGround()
    {
        //transform.GetComponent<SphereCollider>().radius * 1.1f


        bool raycast = Physics.Raycast(transform.position, -transform.up, this.GetComponent<CapsuleCollider>().radius * 1.2f);
        //RaycastHit hit ;
        Debug.DrawRay(transform.position-new Vector3(0,1,0), -transform.up*1.2f, Color.red, 3f);
        print(raycast + "bjxacvadswhbj");
        //Debug.DrawLine(hit.point,Color.red);
        return raycast;
    }
    void Update()
    {
        m_Movement = Vector3.zero;
        m_FSM.Update();
        if (!vivo)
        {
            m_ControlesPlayer.Player.Salto.started -= YoDigoSaltaConmigo;
            m_ControlesPlayer.Player.Movimiento.canceled -= StopMovement;
            m_ControlesPlayer.Player.Disable();
            SceneManager.LoadScene("Muerto");
        }
        if (ganador)
        {
            m_ControlesPlayer.Player.Salto.started -= YoDigoSaltaConmigo;
            m_ControlesPlayer.Player.Movimiento.canceled -= StopMovement;
            m_ControlesPlayer.Player.Disable();
            SceneManager.LoadScene("Ganador");
        }


    }

    private void FixedUpdate()
    {
        Vector3 movement = m_ControlesPlayer.Player.Movimiento.ReadValue<Vector3>();
        int rotation = m_ControlesPlayer.Player.Rotate.ReadValue<int>();

        if (movement.x > 0)
        {
            transform.Rotate(Vector3.up * m_RotationSpeed * Time.deltaTime);
           // m_Movement += transform.right;
        }
        else if (movement.x < 0)
        {
            transform.Rotate(-Vector3.up * m_RotationSpeed * Time.deltaTime);
            //m_Movement -= transform.right;
        }
        if (movement.z > 0)
        {
              m_Movement += transform.forward;
              //transform.Rotate(Vector3.up );
            
        /*    float hor = movement.z;
            float ver = movement.x;
            float movementSpeed = 0;
            Vector3 forward = camera.transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = camera.transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 direction = forward * ver + right * hor;
            movementSpeed = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();

            m_Movement = direction * m_Speed * movementSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0f);*/


        }
        else if (movement.z < 0)
        {
            m_Movement -= transform.forward;
        }
        /*
        if (rotation > 0)
        {
            m_Animator.Play("Z_Walk1_InPlace");
            transform.Rotate(Vector3.up * m_RotationSpeed * Time.deltaTime);
        }
        else if (rotation < 0)
        {
            m_Animator.Play("Z_Walk1_InPlace");
            transform.Rotate(-Vector3.up * m_RotationSpeed * Time.deltaTime);
        }
        */
        m_Movement.Normalize();
        m_Rigidbody.MovePosition(transform.position + m_Movement * m_Speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "suelo")
        {
            print("auch");
            vivo= false;
            m_Aterra = true;
        }
        if (collision.gameObject.tag == "final")
        {
            ganador = true;
        }
    }
}
