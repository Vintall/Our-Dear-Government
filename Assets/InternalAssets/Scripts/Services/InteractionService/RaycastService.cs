using System;
using QuickOutline.Scripts;
using UnityEngine;

namespace InternalAssets.Scripts.Services.InteractionService
{
    public class RaycastService : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private float _interactionDistance;
        public event Action<RaycastHit> _onHit;
        private InteractableObject markedToDisableOutline;

        public void MarkDisableOutline(InteractableObject interactableObject)
        {
            markedToDisableOutline = interactableObject;
        }
        
        private void Update()
        {
            if (markedToDisableOutline != null)
            {
                markedToDisableOutline.Outline.OutlineWidth = 0;
                markedToDisableOutline = null;
            }

            Raycast();
        }

        //private Outline cachedOutline;
        private void Raycast()
        {
            var forwardRay = new Ray(_camera.position, _camera.forward);

            RaycastHit raycastHit;

            //if (cachedOutline != null)
            //    cachedOutline.enabled = false;
                
            //FindFirstInteractable(results);
            
            if (Physics.Raycast(forwardRay, out raycastHit, _interactionDistance))
                _onHit?.Invoke(raycastHit);
        }

        private void FindFirstInteractable(RaycastHit[] results)
        {
            foreach (var raycastHit in results)
            {
                if (raycastHit.transform.tag.Equals("Interactable"))
                {
                    var outlineComponent = raycastHit.transform.GetComponent<Outline>();
                    outlineComponent.enabled = true;
                    //cachedOutline = outlineComponent;
                    break;
                }
            }
        }
    }
}