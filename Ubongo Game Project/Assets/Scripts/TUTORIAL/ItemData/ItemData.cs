using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tetromino
{
    I,J,L,O,S,T,Z
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int widht = 1;
    public int height = 1;

    public Sprite itemIcon;

    public Tetromino tetrominoes;
}
