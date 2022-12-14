using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField]
    private Transform follow;
    [SerializeField]
    private Vector3 m_Offset;
    [Range(0.1f, 1f)]
    [SerializeField]
    private float m_smooth = 1f;
    [SerializeField]
    private float rotationspeed = 5f;
    private Vector3 m_ParaElLookAt;
    ControlesPlayer m_ControlesPlayer;
  
    Vector3 pos;
    private void Start()
    {
        pos = transform.position;

    }
    private void Awake()
    {
        m_ControlesPlayer = new ControlesPlayer();
        m_ControlesPlayer.Enable();
    }
 
    private void LateUpdate()
    {
        print(follow.transform.position.y+ "personaje");
        print(transform.position.y + "cam");
        float mousePositionX = m_ControlesPlayer.Player.MouseX.ReadValue<float>();
        mousePositionX /= 20;
        float mousePositionY = m_ControlesPlayer.Player.MouseY.ReadValue<float>();
        mousePositionY /= 20;
        Quaternion camAngleX = Quaternion.AngleAxis(mousePositionX * rotationspeed, Vector3.up); 
        Quaternion camAngleY = Quaternion.AngleAxis(mousePositionY * rotationspeed, Vector3.right); 
        m_Offset = (camAngleX * camAngleY).normalized * m_Offset;
        Vector3 posPlayer = follow.transform.position + m_Offset;
        pos.y = Mathf.Clamp(transform.position.y, follow.transform.position.y + 1.26f, follow.transform.position.y + 1.88f);
        transform.position = pos;
        transform.position = Vector3.Slerp(transform.position, posPlayer, m_smooth); 
        transform.rotation = Quaternion.LookRotation(follow.transform.position - transform.position);                                                                          
        m_ParaElLookAt = new Vector3(follow.position.x, (follow.position.y + 1), follow.position.z);
        transform.LookAt(m_ParaElLookAt);
    }

}



 
















/*
 *  private void Awake()
      {
          m_ParaElLookAt = Vector3.zero;
          m_ControlesPlayer = new ControlesPlayer();
          m_ControlesPlayer.Enable();
      }

      void Update()
      {
          float mousePositionX = m_ControlesPlayer.Player.MouseX.ReadValue<float>();

          mousePositionX /= 20;
          // float hor = mousePosition.x;

          if (mousePositionX != 0)
          {
              angle.x += mousePositionX * Mathf.Deg2Rad * sensitivity.x;

          }

          float mousePositionY = m_ControlesPlayer.Player.MouseY.ReadValue<float>();
          mousePositionY /= 20;

          if (mousePositionY != 0)
          {
              angle.y += mousePositionY * Mathf.Deg2Rad * sensitivity.y;
              angle.y = Mathf.Clamp(angle.y, -80 * Mathf.Deg2Rad, -5 * Mathf.Deg2Rad);
          }
      }

      // Update is called once per frame
      void LateUpdate()
      {
          Vector3 orbit = new Vector3(
              Mathf.Cos(angle.x) * Mathf.Cos(angle.y),
              -Mathf.Sin(angle.y),
              -Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
              );

          transform.position = follow.position + orbit * distance;
          transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
          m_ParaElLookAt = new Vector3(follow.position.x, (follow.position.y + 1), follow.position.z);
          transform.LookAt(m_ParaElLookAt);
      }
*/