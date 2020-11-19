using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    /*
    Object telekinesis states:
    1 - Default
    2 - On hover / on look / focused
    3 - On select
    4 - Selected
    5 - Inactive / disabled (optional - could be left as default)
    */

    private Outline outline;

    private Material originalMaterial;
    private Material outlineMaterial;
    private Material selectionMaterial;
    private GameObject particleSystem;

    private Renderer mesh;
    private Material[] matArray;
    public bool isSelected;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        originalMaterial = GetComponent<Renderer>().material;
        selectionMaterial = Testing.Instance.Telekinesis.seethrough;
        particleSystem = Testing.Instance.Telekinesis.particles;
        mesh = GetComponent<Renderer>();
        matArray = mesh.materials;
        outlineMaterial = Testing.Instance.Telekinesis.outlineMaterial;
    }

    public void Default()
    {
        print("default");
        outline.enabled = false;
        matArray[0] = originalMaterial;
        matArray[1] = originalMaterial;
        mesh.materials = matArray;
        isSelected = false;
    }

    public void Focused()
    {
        // outline
        print("focused");
        outline.enabled = true;
        /*matArray[0] = originalMaterial;
        matArray[1] = outlineMaterial;
        mesh.materials = matArray;*/
        isSelected = false;
    }

    public void OnSelect()
    {
        print("onselect");
        // vibration

        // outline
        outline.enabled = false;
        // selection shader
        matArray[0] = selectionMaterial;
        matArray[1] = selectionMaterial;
        mesh.materials = matArray;
        isSelected = true;
        // StartCoroutine(OnSelectCoroutine());
    }

    public void Selected() { }

    public void Disabled() { }

    private IEnumerator OnSelectCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        Selected();
    }
}