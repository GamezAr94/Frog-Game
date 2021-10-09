using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    int combo = 0;
    int points = 0;
    int tries = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        points = 0;
        // List<Creatures> creatures = new List<Creatures>();
        if (other.CompareTag("TongTip"))
        {
            combo = other.transform.childCount;
            
            tries++;
        }
        if (other.CompareTag("Collectibles"))
        {
            points = other.transform.GetComponent<Creatures>().Creature.value;
            // creatures.Add(other.transform.GetComponent<Creatures>());
        }
        gameManager.UpdateScore(combo, points, tries);
    }
}
