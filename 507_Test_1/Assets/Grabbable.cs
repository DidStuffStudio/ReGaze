using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
  public bool focused = false;
  private Color originalColor;

  private void Start()
  {
    originalColor = GetComponent<Renderer>().material.color;
  }

  private void Update()
  {
    Highlight(focused);
  }

  public void Highlight(bool on)
  {
    if (on)
    {
      GetComponent<Renderer>().material.color=Color.cyan;
    }
    else
    {
      GetComponent<Renderer>().material.color = originalColor;
    }
  }
}
