using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifecycle : MonoBehaviour
{
    private ParticleSystem explodeParticles;
    // Start is called before the first frame update
    void Start()
    {
        explodeParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!explodeParticles.IsAlive()) {            
            Destroy(this.gameObject);
        }
    }
}
