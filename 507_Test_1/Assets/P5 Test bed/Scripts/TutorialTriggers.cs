using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour
{
    public Usability_StoryManager sm;
    private int audioClipIndex = 0;
    private BlinkTransform blinkTransform;

    private void Start()
    {
        blinkTransform = GetComponent<BlinkTransform>();
        blinkTransform.enabled = false;
        StartCoroutine(Wait());
    }

    private void OnTriggerEnter(Collider other)
    {
        // each of the trigger boxes will allow for the use of certain telekinesis interactions.
        // first it only allows for:
        // 1 - Eye
        // 2 - Eye + Gesture
        // 3 - Eye  Gesture + Touchpad

        // we have 4 different trigger boxes
        // 1 - Precise dash
        // 2  Asteroid falling down
        // 3 - Gesture
        // 4 - Trackpad

        Debug.Log("Player has triggered with " + other.name);

        if (other.CompareTag("PreciseDashTriggerBox"))
        {
            Testing.Instance.tutorialInteractionMethods = Testing.TutorialInteractionMethods.EyeOnly;
            //Debug.Log("Moving objects with the eye only");
        }
        else if (other.CompareTag("AsteroidTriggerBox"))
        {
            //Debug.Log("Moving objects with the eye + gestures");
            Testing.Instance.tutorialInteractionMethods = Testing.TutorialInteractionMethods.EyeOnly;
        }
        else if (other.CompareTag("GestureTriggerBox"))
        {
            Testing.Instance.tutorialInteractionMethods = Testing.TutorialInteractionMethods.Gestures;
        }
        else if (other.CompareTag("TouchpadTriggerBox"))
        {
            //Debug.Log("Moving objects with the eye + gestures + touchpad");
            Testing.Instance.tutorialInteractionMethods = Testing.TutorialInteractionMethods.TouchPad;
        }

        PlaySoundAndAnimation(other.gameObject);
    }

    public void BlinkSwitch(bool enabled)
    {
        blinkTransform.enabled = enabled;
    }
    private void PlaySoundAndAnimation(GameObject g)
    {
        var audioSource = g.GetComponent<AudioSource>();
        if (audioSource) audioSource.Play();
        else Debug.LogWarning("Audio file is missing on " + g.name);

        var animation = g.GetComponent<Animation>();
        if (animation) animation.Play();
        else Debug.LogWarning("Animation is missing on " + g.name);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        sm.PlayVoice(0);
        yield return new WaitForSeconds(sm.voiceClips[0].length + 1.0f);
        sm.PlayVoice(1);
        blinkTransform.enabled = true;
    }
}