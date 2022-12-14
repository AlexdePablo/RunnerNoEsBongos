using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLatforms : MonoBehaviour
{
    Rigidbody m_RigidBody;
    [SerializeField]
    int m_Velocity;
    private Vector3 Max;
    private Vector3 Min;
    private Vector3 direccion = Vector3.left;
    // Start is called before the first frame update
    void Start()
    {
        direccion = Vector3.left;
        Max =new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
        Min = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
        m_RigidBody=GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //Si alcanza el límite superior, dirección bajada
        if (transform.position.x < Min.x)
        {
            direccion = Vector3.right;
        }

        //Si alcanza el límite inferior, dirección subida
        if (transform.position.x > Max.x)
        {
            direccion = Vector3.left;
        }

        //Traslada la plataforma en cada frame a la velocidad y dirección indicadas
        transform.Translate(direccion * Time.deltaTime * m_Velocity);

       
    }
}
