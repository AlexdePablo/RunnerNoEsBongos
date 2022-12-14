using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luces : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform Objeto;
    private void Update()
    {
        transform.position = new Vector3(Objeto.position.x, (float)(Objeto.position.y + 5.11), Objeto.position.z);
    }
}
