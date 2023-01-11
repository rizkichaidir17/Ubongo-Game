using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CekCollide : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Item.isDrop)
        {
            if (collision.tag == "Grid")
            {
                transform.position = collision.transform.position;
                Debug.Log("Posisi collidenya ada di " + collision.name);
            }

        }
    }
}
