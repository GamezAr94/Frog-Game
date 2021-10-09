using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsParticlesBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float delayToDestroyDropParticles = 1.0f;
    
    // Function to instantiate a particle effect and destroy it after some defined time
    public void DropParticles(Vector3 positionToInstantiate)
    {
        GameObject instanceDropParticle = Instantiate(this.gameObject, positionToInstantiate, Quaternion.identity);
        Destroy(instanceDropParticle, delayToDestroyDropParticles);
    }
}
