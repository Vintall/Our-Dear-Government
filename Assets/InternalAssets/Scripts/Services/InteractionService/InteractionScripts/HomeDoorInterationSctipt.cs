﻿using DG.Tweening;
using UnityEngine;

namespace InternalAssets.Scripts.Services.InteractionService.InteractionScripts
{
    public class HomeDoorInterationSctipt : AbstractObjectInteractionScript
    {
        private bool _isOpen = false;
        private Sequence doorSequence;
        [SerializeField] private Vector3 closedAngle;
        [SerializeField] private Vector3 openedAngle;
        [SerializeField] private float animationDuration;
        [SerializeField] private Collider physicalCollider;
        
        public override void Interact()
        {
            if(_isOpen)
                CloseDoor();
            else
                OpenDoor();

            _isOpen = !_isOpen;
        }

        private void OpenDoor()
        {
            KillSequence();

            physicalCollider.enabled = false;
            doorSequence = DOTween.Sequence()
                .Append(transform.DORotate(openedAngle, animationDuration)
                    .SetEase(Ease.OutCubic));
        }

        private void CloseDoor()
        {
            KillSequence();

            doorSequence = DOTween.Sequence()
                .Append(transform.DORotate(closedAngle, animationDuration)
                    .SetEase(Ease.InCubic))
                .AppendCallback(() => physicalCollider.enabled = true);
        }

        private void KillSequence()
        {
            if(doorSequence == null)
                return;

            doorSequence.Kill();
            doorSequence = null;
        }
    }
}