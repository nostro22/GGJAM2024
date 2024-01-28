using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class HandTrackingGenerator : MonoBehaviour
{
    public KeyCode keyToTakeSnapshotTrack;
    public Transform leftHand;
    public Transform rightHand;
    public HandsPoserSO handsPoserSo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToTakeSnapshotTrack))
        {
            handsPoserSo.leftHand.position = leftHand.position;
            handsPoserSo.leftHand.rotation = leftHand.rotation;
            handsPoserSo.rightHand.position = rightHand.position;
            handsPoserSo.rightHand.rotation = rightHand.rotation;
        }
    }
}
