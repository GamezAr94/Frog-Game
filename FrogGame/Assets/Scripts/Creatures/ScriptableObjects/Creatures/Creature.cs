
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureDetails", menuName = "Creature/CreatureDetails")]
public class Creature : ScriptableObject
{
    [Range(0, 10)]
    public float movementSpeed;

    public CreatureType creatureType;

    
    [Range(0, 50)]
    public float lifeTime;

    
    [Range(0, 50)]
    public int value;

    public CreatureBoundaries creatureBoundaries;

}
