using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterPickup : MonoBehaviour
{
    public BlasterType blasterType;

    public void ChangeBlaster(BlasterType priorBlaster)
    {
        blasterType = priorBlaster;

        this.GetComponent<MeshFilter>().mesh = blasterType.blasterMesh;
        this.GetComponent<MeshRenderer>().material = blasterType.blasterMaterial;
    }

}
