using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Transform slotTransform;
    [SerializeField] private Transform slotAnchorTransform;
    [SerializeField] private float convergenceIntensity = 1f;
    [SerializeField] private float divergenceIntensity = 1f;
    [SerializeField] private float maxDistance = 0.3f;
    
    private Vector3 _anchorPreviousPosition;
    
    private void Start()
    {
        _anchorPreviousPosition = slotAnchorTransform.position;
    }

    private void Update()
    {
        var slotPosCached = slotTransform.position;
        var slotAnchorPosCached = slotAnchorTransform.position;

        var slotToAnchorVector = slotAnchorPosCached - slotPosCached;
        var slotToAnchorDistance = slotToAnchorVector.magnitude;

        if (slotToAnchorDistance > maxDistance)
        {
            slotTransform.position = slotAnchorPosCached - slotToAnchorVector.normalized * maxDistance;
            return;
        }

        var fractionFromMaxToSlot = (maxDistance - slotToAnchorDistance) / maxDistance;
        var fractionFromAnchorToSlot = 1 - fractionFromMaxToSlot;

        var anchorPositionDelta = slotAnchorPosCached - _anchorPreviousPosition;

        var convergenceForce = convergenceIntensity * fractionFromAnchorToSlot * fractionFromAnchorToSlot * slotToAnchorVector.normalized;
        var divergenceForce = -divergenceIntensity * fractionFromMaxToSlot * anchorPositionDelta.normalized;
        
        var finalPositionDelta = convergenceForce + divergenceForce;

        slotTransform.position += finalPositionDelta * Time.deltaTime;
        _anchorPreviousPosition = slotAnchorTransform.position;
    }
}