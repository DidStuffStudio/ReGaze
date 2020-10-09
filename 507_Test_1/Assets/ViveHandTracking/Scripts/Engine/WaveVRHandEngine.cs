using System.Collections;
using UnityEngine;

namespace ViveHandTracking {

#if VIVEHANDTRACKING_WAVEVR_HAND && !UNITY_EDITOR

public class WaveVRHandEngine: HandTrackingEngine {
  private wvr.WVR_HandSkeletonData_t skeletonData = new wvr.WVR_HandSkeletonData_t();
  private wvr.WVR_HandPoseData_t poseData = new wvr.WVR_HandPoseData_t();
  private WaveVR_Utils.RigidTransform rigidTransform = WaveVR_Utils.RigidTransform.identity;
  private GestureResultRaw leftHand, rightHand;

  public override bool IsSupported() {
    return true;
  }

  public override IEnumerator Setup() {
    var transform = GestureProvider.Current.transform;
    var gameObject = GestureProvider.Current.gameObject;

    if (WaveVR_GestureManager.Instance == null) {
      gameObject.AddComponent<WaveVR_GestureManager>();
      WaveVR_GestureManager.Instance.EnableHandGesture = false;
      WaveVR_GestureManager.Instance.EnableHandTracking = false;
    }

    leftHand = CreateHand(true);
    rightHand = CreateHand(false);
    yield break;
  }

  public override IEnumerator StartDetection(GestureOption option) {
    if (State.Status == GestureStatus.Starting || State.Status == GestureStatus.Running)
      yield break;

    var gestureStatus = WaveVR_GestureManager.Instance.GetHandGestureStatus();
    if (gestureStatus == WaveVR_Utils.HandGestureStatus.UNSUPPORT) {
      Debug.LogError("WaveVR gesture not supported");
      State.Status = GestureStatus.Error;
      yield break;
    }
    var trackingStatus = WaveVR_GestureManager.Instance.GetHandTrackingStatus();
    if (trackingStatus == WaveVR_Utils.HandTrackingStatus.UNSUPPORT) {
      Debug.LogError("WaveVR tracking not supported");
      State.Status = GestureStatus.Error;
      yield break;
    }

    WaveVR_GestureManager.Instance.EnableHandGesture = true;
    WaveVR_GestureManager.Instance.EnableHandTracking = true;
    State.Mode = GestureMode.Skeleton;

    while (true) {
      yield return null;
      gestureStatus = WaveVR_GestureManager.Instance.GetHandGestureStatus();
      if (gestureStatus == WaveVR_Utils.HandGestureStatus.NOT_START ||
          gestureStatus == WaveVR_Utils.HandGestureStatus.STARTING)
        continue;
      trackingStatus = WaveVR_GestureManager.Instance.GetHandTrackingStatus();
      if (trackingStatus == WaveVR_Utils.HandTrackingStatus.NOT_START ||
          trackingStatus == WaveVR_Utils.HandTrackingStatus.STARTING)
        continue;
      break;
    }

    if (gestureStatus != WaveVR_Utils.HandGestureStatus.AVAILABLE) {
      Debug.LogError("WaveVR gesture start failed: " + gestureStatus);
      State.Status = GestureStatus.Error;
      WaveVR_GestureManager.Instance.EnableHandGesture = false;
      WaveVR_GestureManager.Instance.EnableHandTracking = false;
      yield break;
    }
    if (trackingStatus != WaveVR_Utils.HandTrackingStatus.AVAILABLE) {
      Debug.LogError("WaveVR tracking start failed: " + trackingStatus);
      State.Status = GestureStatus.Error;
      WaveVR_GestureManager.Instance.EnableHandGesture = false;
      WaveVR_GestureManager.Instance.EnableHandTracking = false;
      yield break;
    }
    State.Status = GestureStatus.Running;
  }

  public override void UpdateResult() {
    if (State.Status != GestureStatus.Running)
      return;
    if (!WaveVR_GestureManager.Instance.GetHandSkeletonData(ref skeletonData)) {
      Debug.LogError("Get skeleton data failed");
      State.Status = GestureStatus.Error;
      State.Error = GestureFailure.Internal;
      return;
    }
    if (!WaveVR_GestureManager.Instance.GetHandPoseData(ref poseData)) {
      Debug.LogError("Get pose data failed");
      State.Status = GestureStatus.Error;
      State.Error = GestureFailure.Internal;
      return;
    }

    if (skeletonData.left.wrist.IsValidPose) {
      var gesture = (WaveVR_GestureManager.EStaticGestures)WaveVR_GestureManager.Instance.GetCurrentLeftHandStaticGesture();
      leftHand.gesture = MapGesture(gesture);
      SetHandPoints(leftHand, skeletonData.left, poseData.left);
      State.SetRaw(leftHand);
    } else
      State.LeftHand = null;
    if (skeletonData.right.wrist.IsValidPose) {
      var gesture = (WaveVR_GestureManager.EStaticGestures)WaveVR_GestureManager.Instance.GetCurrentRightHandStaticGesture();
      rightHand.gesture = MapGesture(gesture);
      SetHandPoints(rightHand, skeletonData.right, poseData.right);
      State.SetRaw(rightHand);
    } else
      State.RightHand = null;
  }

  public override void StopDetection() {
    WaveVR_GestureManager.Instance.EnableHandGesture = false;
    WaveVR_GestureManager.Instance.EnableHandTracking = false;
  }

  GestureResultRaw CreateHand(bool left) {
    var hand = new GestureResultRaw();
    hand.isLeft = left;
    hand.points = new Vector3[21];
    return hand;
  }

  GestureType MapGesture(WaveVR_GestureManager.EStaticGestures gesture) {
    switch (gesture) {
    case WaveVR_GestureManager.EStaticGestures.FIST:
      return GestureType.Fist;
    case WaveVR_GestureManager.EStaticGestures.FIVE:
      return GestureType.Five;
    case WaveVR_GestureManager.EStaticGestures.OK:
      return GestureType.OK;
    case WaveVR_GestureManager.EStaticGestures.THUMBUP:
      return GestureType.Like;
    case WaveVR_GestureManager.EStaticGestures.INDEXUP:
      return GestureType.Point;
    default:
      return GestureType.Unknown;
    }
  }

  void SetHandPoints(GestureResultRaw hand, wvr.WVR_HandSkeletonState_t skeleton, wvr.WVR_HandPoseState_t pose) {
    if (hand == null || !skeleton.wrist.IsValidPose)
      return;
    rigidTransform.update(skeleton.wrist.PoseMatrix);
    hand.points[0] = rigidTransform.pos;
    SetFingerPoints(hand, skeleton.thumb, 1);
    SetFingerPoints(hand, skeleton.index, 5);
    SetFingerPoints(hand, skeleton.middle, 9);
    SetFingerPoints(hand, skeleton.ring, 13);
    SetFingerPoints(hand, skeleton.pinky, 17);

    // calculate pinch level
    if (pose.state.type == wvr.WVR_HandPoseType.WVR_HandPoseType_Pinch)
      hand.pinchLevel = pose.pinch.strength;
    else
      hand.pinchLevel = 0;
    hand.confidence = skeleton.confidence;

    // apply camera offset to hand points
    var transform = GestureProvider.Current.transform;
    if (transform.parent != null) {
      for (int i = 0; i < 21; i++)
        hand.points[i] = transform.parent.rotation * hand.points[i] + transform.parent.position;
    }
  }

  void SetFingerPoints(GestureResultRaw hand, wvr.WVR_FingerState_t finger, int startIndex) {
    hand.points[startIndex] = WaveVR_Utils.GetPosition(finger.joint1);
    hand.points[startIndex + 1] = WaveVR_Utils.GetPosition(finger.joint2);
    hand.points[startIndex + 2] = WaveVR_Utils.GetPosition(finger.joint3);
    hand.points[startIndex + 3] = WaveVR_Utils.GetPosition(finger.tip);
  }
}

#else

public class WaveVRHandEngine: HandTrackingEngine {
  public override bool IsSupported() {
    return false;
  }

  public override IEnumerator Setup() {
    yield break;
  }

  public override IEnumerator StartDetection(GestureOption option) {
    yield break;
  }

  public override void UpdateResult() {}

  public override void StopDetection() {}

  public override string Description() {
#if VIVEHANDTRACKING_WAVEVR_HAND
    return "[Experimental] Requires real WaveVR device";
#else
    return "[Experimental] Requires WaveVR 3.1.95+";
#endif
  }
}

#endif

}
