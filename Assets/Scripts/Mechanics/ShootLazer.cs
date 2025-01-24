using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLazer : MonoBehaviour
{
    public Material material;
    LazerBeam beam;

    void Update()
    {
        Destroy(GameObject.Find("Lazer"));
        beam = new LazerBeam(transform.position, transform.forward, material);
    }
}
