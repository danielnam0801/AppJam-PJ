using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite abilityImage;
    public string abilityName;
    public string abilityDescription;
    public int abilityLevel;
    public int abilityPrice; // 

}
