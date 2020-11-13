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

    public bool focused = false;
    private Outline outline;
    
    private Material originalMaterial;
    private Material outlineMaterial;
    private Material selectionMaterial;
    private GameObject particleSystem;

    private Renderer mesh;
    private Material [] matArray;
    
    public enum ObjectState
    {
        Default,
        Focused,
        OnSelect,
        Selected,
        Disabled
    }

    public ObjectState objectState;
    
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

    private void UpdateObjectState()
    {
        switch (objectState)
        {
            case ObjectState.Default:
            {
                print("default");
                // some indication that that object is interactable (optional for now)
                if (outline.enabled) outline.enabled = false;
                
                if (mesh.material != originalMaterial) mesh.material = originalMaterial;
                //particleSystem.SetActive(false);
                matArray[1] = originalMaterial;
                mesh.materials = matArray;

                break;
            }
            case ObjectState.Focused:
            {
                // outline
                print("focused");
                outline.enabled = true;
                matArray[1] = outlineMaterial;
                mesh.materials = matArray;

                break;
            }
            case ObjectState.OnSelect:
            {
                print("onselect");
                // vibration, outline, selection shader
                // vibration
                
                // outline
                outline.enabled = true;
                // selection shader
                GetComponent<Renderer>().material = selectionMaterial;
                StartCoroutine(OnSelectCoroutine());                
                break;
            }
            case ObjectState.Selected:
            {
                print("selected");
                // selection shader, depth signifier (particle system for now), outline
                // particle system
                //particleSystem.SetActive(true);
                break;
            }
            case ObjectState.Disabled:
            {
                break;
            }
        }
        // Highlight(focused);
    }

    public void Highlight(bool on)
    {
        if (on)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    public void Default()
    {
        objectState = ObjectState.Default;
        UpdateObjectState();
    }
    public void Focused()
    {
        objectState = ObjectState.Focused;
        UpdateObjectState();
    }

    public void OnSelect()
    {
        objectState = ObjectState.OnSelect;
        UpdateObjectState();
    }

    public void Selected()
    {
        objectState = ObjectState.Selected;
        UpdateObjectState();
    }

    public void Disabled()
    {
        objectState = ObjectState.Disabled;
        UpdateObjectState();
    }

    private IEnumerator OnSelectCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        objectState = ObjectState.Selected;
        Selected();
    }

}