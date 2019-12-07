using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int base_value;

    public int BaseValue
    {
        get { return this.base_value; }
        set { this.base_value = value; }
    }
}
