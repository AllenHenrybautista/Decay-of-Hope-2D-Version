using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "armor", menuName = "Inventory/armor")]
public class Armor : ScriptableObject
{
    public string name;
    public string description;

    public Sprite icon;

    public int cost;
    public int sellvalue;

}
