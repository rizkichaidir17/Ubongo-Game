using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Grid[] grids;

    private void Awake()
    {
        grids = GetComponentsInChildren<Grid>();
    }
}
