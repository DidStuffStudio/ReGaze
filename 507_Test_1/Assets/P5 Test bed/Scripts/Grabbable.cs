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
    private Material selectionMaterial;
    private GameObject particleSystem;
    
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
    }

    private void Update()
    {
        switch (objectState)
        {
            case ObjectState.Default:
            {
                print("default");
                // some indication that that object is interactable (optional for now)
                if (outline.enabled) outline.enabled = false;
                if (GetComponent<Renderer>().material != originalMaterial)
                    GetComponent<Renderer>().material = originalMaterial;
                //particleSystem.SetActive(false);
                break;
            }
            case ObjectState.Focused:
            {
                // outline
                print("focused");
                outline.enabled = true;
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

    public void Default() => objectState = ObjectState.Default;
    public void Focused() => objectState = ObjectState.Focused;
    public void OnSelect() => objectState = ObjectState.OnSelect;
    public void Selected() => objectState = ObjectState.Selected;
    public void Disabled() => objectState = ObjectState.Disabled;
    private IEnumerator OnSelectCoroutine()
    {
        yield return new WaitForSeconds(0.05f);
        objectState = ObjectState.Selected;
        Selected();
    }

}