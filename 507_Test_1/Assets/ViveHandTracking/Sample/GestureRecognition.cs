using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveHandTracking;

public class GestureRecognition : MonoBehaviour
{
    public bool gestureRecognized = false;
    private float timer = 0;
    private bool canTeleport = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(CustomGestureProvider.LeftHandState == )    CustomGestureProvider.LeftHandState.index;   
    }

    public void GestureRecognized()
    {
        // Debug.Log("Gesture recognixzed");
        // gestureRecognized = true;
    }

    public void UpdateGestureState(HandState state)
    {
        if (!canTeleport) return;
        StartCoroutine(Timer());
        if(state.thumb == ThumbState.Close && state.index == FingerState.Open && state.middle == FingerState.Close && state.pinky == FingerState.Close && state.ring == FingerState.Close)
        {
            Debug.Log("Like Gesture recognized");
            gestureRecognized = true;
        }
    }

    IEnumerator Timer()
    {
        canTeleport = false;
        yield return new WaitForSeconds(1.5f);
        canTeleport = true;
    }
}
