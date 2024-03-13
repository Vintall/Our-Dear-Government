﻿using UnityEngine;

namespace InternalAssets.Scripts.Services.InteractionService.InteractionScripts.Act3
{
    public class KeyboardInteractionScript : AbstractObjectInteractionScript
    {
        [SerializeField] private string _interactionText;

        public override void Interact()
        {
            
        }

        public override string InteractionText => _interactionText;
    }
}