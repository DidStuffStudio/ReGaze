using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
  public bool focused = false;
  private Outline outline;

  private void Start()
  {
    outline = GetComponent<Outline>();
    outline.enabled = false;
  }

  private void Update()
  {
    Highlight(focused);
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
}
