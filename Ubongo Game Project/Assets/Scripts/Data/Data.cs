using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Grid")
        {
            
            RectTransform rect = GetComponent<RectTransform>();
            RectTransform otherRect = other.gameObject.GetComponent<RectTransform>();
            rect.position = otherRect.position;
            
        }
    }
}
