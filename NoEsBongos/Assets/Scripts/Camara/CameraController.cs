using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform follow;
    private Vector2 angle = new Vector2(90 * Mathf.Deg2Rad, 0);
    [SerializeField]
    private Vector3 m_RelativePositionFromPlayer;
    private Vector3 m_ParaElLookAt;
    private ControlesPlayer m_ControlesPlayer;
    Vector3 cameraPositionX;
    Vector3 cameraPositionY;
    Vector3 cameraPosition;
    Vector3 gameObjectPosition;

    public Vector2 sensitivity;









    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Awake()
    {
        cameraPositionX= Vector3.zero;
        cameraPositionY = Vector3.zero;
        cameraPosition = Vector3.zero;
        m_ParaElLookAt = Vector3.zero;
        gameObjectPosition = Vector3.zero;
        m_ControlesPlayer = new ControlesPlayer();
        m_ControlesPlayer.Enable();
    }

    void Update()
    {
        float mousePositionX = m_ControlesPlayer.Player.MouseX.ReadValue<float>();
      
       // float hor = mousePosition.x;

        if (mousePositionX != 0)
        {
            angle.x += mousePositionX * Mathf.Deg2Rad * sensitivity.x;
        }

        float mousePositionY = m_ControlesPlayer.Player.MouseY.ReadValue<float>();

        if (mousePositionY != 0)
        {
            angle.y += mousePositionY * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
            
        }
      

/*
        Vector2 mousePosition = m_ControlesPlayer.Player.Raton.ReadValue<Vector2>();
        print(mousePosition);
        cameraPositionX = new Vector3(mousePosition.x,0,mousePosition.x).normalized;
        cameraPositionY = new Vector3(0, mousePosition.y, mousePosition.y).normalized;
       // cameraPosition= new Vector3(m_RelativePositionFromPlayer.x, m_RelativePositionFromPlayer.y, m_RelativePositionFromPlayer.z);
        //gameObjectPosition = cameraPosition;
        transform.position += cameraPositionX + cameraPositionY;
        //objetoDeJuegoKekw.transform.position = gameObjectPosition;
        m_ParaElLookAt = new Vector3(m_Target[m_Index].transform.position.x, (m_Target[m_Index].transform.position.y+1), m_Target[m_Index].transform.position.z);
        transform.LookAt(m_ParaElLookAt);
       

        if (Input.GetKey(KeyCode.C))
        {
            m_ParaElLookAt = new Vector3(m_Target[m_Index].transform.position.x, (m_Target[m_Index].transform.position.y + 1), -m_Target[m_Index].transform.position.z);
            transform.LookAt(m_ParaElLookAt);
        }*/
    }


    private void FixedUpdate()
    {
        Vector3 orbit = new Vector3(
            Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
            -Mathf.Sin(angle.y),
            -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
            );

        transform.position = new Vector3 (follow.position.x + orbit.x * m_RelativePositionFromPlayer.x, follow.position.y + orbit.y * m_RelativePositionFromPlayer.y, follow.position.z + orbit.z * m_RelativePositionFromPlayer.z);
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
        m_ParaElLookAt = new Vector3(follow.position.x, (follow.position.y + 1), -follow.position.z);
        transform.LookAt(m_ParaElLookAt);
    }

}
