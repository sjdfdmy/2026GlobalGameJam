using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "CreateData/Monster", order = 1)]
public class CreateMonster : ScriptableObject
{
    public string monstername;
    public float health;
    public float damage;
}
