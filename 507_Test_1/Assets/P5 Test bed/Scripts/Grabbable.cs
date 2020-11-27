using System;
using System.Collections;
using System.Collections.Generic;
using Tobii.G2OM;
using UnityEngine;
using Valve.VR;

public class Grabbable : MonoBehaviour, IGazeFocusable
{
    /*
    Object telekinesis states:
    1 - Default
    2 - On hover / on look / focused
    3 - On select
    4 - Selected
    5 - Inactive / disabled (optional - could be left as default)
    */

    // to vibrate controller onseleect of object

    public SteamVR_Action_Vibration hapticAction;
    
    private Outline outline;

    private Material originalMaterial;
    private Material outlineMaterial;
    private Material selectionMaterial;
    private GameObject particleSystem;

    private Renderer mesh;
    private Material[] matArray;
    public bool isSelected;
    public bool canCollide = true;

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
        isSelected = false;
        Testing.Instance.Telekinesis.focusedObject = gameObject;
    }

    public void OnSelect()
    {
        print("onselect");
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

    // The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        // ref --> https://vr.tobii.com/sdk/develop/unity/documentation/api-reference/core/
        // This object either received or lost focused this frame, as indicated by the hasFocus parameter
        if(Testing.Instance.Telekinesis.grabbedObject) return; // if the user already is grabbing an object, return
        if (hasFocus)
        {
            print(gameObject.name + "  is focused by the user");
            if(isSelected) return;
            Focused();
        }
        else
        {
            Default();
            Testing.Instance.Telekinesis.focusedObject = null;
        }
    }

    private void Vibrate(float secondsFromNow, float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(secondsFromNow, duration, frequency, amplitude, source);
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(WaitToCollideAgain());
    }

    private IEnumerator WaitToCollideAgain()
    {
        canCollide = false;
        yield return new WaitForSeconds(0.05f);
        canCollide = true;
    }
}