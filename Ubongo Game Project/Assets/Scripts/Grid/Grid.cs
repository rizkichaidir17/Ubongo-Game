using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] ItemGrid[] ItemGrid;
    // Start is called before the first frame update
    void Start()
    {
        ItemGrid = GetComponentsInChildren<ItemGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
