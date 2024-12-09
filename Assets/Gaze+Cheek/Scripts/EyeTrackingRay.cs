using System.Collections.Generic;
using UnityEngine;

namespace Gazecheek.Scripts
{
    [RequireComponent(typeof(OVREyeGaze))]
    [RequireComponent(typeof(LineRenderer))]
    public class EyeTrackingRay : MonoBehaviour
    {
        [SerializeField] private float rayDistance = 1.0f;
        [SerializeField] private float rayWidth = 1.0f;
    
        [SerializeField] private LayerMask layerMask;
    
        [SerializeField] private Color rayColorDefault = Color.white;
        [SerializeField] private Color rayColorHover = Color.yellow;
    
        private LineRenderer _lineRenderer;

        private List<EyeInteractable> interactables = new List<EyeInteractable>();
        public List<EyeInteractable> Interactables => interactables;

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            SetupRay();
        }

        void SetupRay()
        {
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.positionCount = 2;
            _lineRenderer.startWidth = rayWidth;
            _lineRenderer.endWidth = rayWidth;
            _lineRenderer.startColor = rayColorDefault;
            _lineRenderer.endColor = rayColorDefault;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, new Vector3(transform.position.x, transform.position.y, transform.position.z + rayDistance));
        }

        void FixedUpdate()
        {
            RaycastHit hit;
        
            Vector3 rayCastDirection = transform.TransformDirection(Vector3.forward) * rayDistance;

            if (Physics.Raycast(transform.position, rayCastDirection, out hit, Mathf.Infinity, layerMask))
            {
                _lineRenderer.startColor = rayColorHover;
                _lineRenderer.endColor = rayColorHover;
            
                var eyeInteractable = hit.transform.GetComponent<EyeInteractable>();
                if (!interactables.Contains(eyeInteractable))
                {
                    eyeInteractable.IsSelected = true;
                    UnSelect();
                    interactables.Add(eyeInteractable);
                }
            }
            else
            {
                _lineRenderer.startColor = rayColorDefault;
                _lineRenderer.endColor = rayColorDefault;
                
                UnSelect();
            }

            void UnSelect()
            {
                foreach (var interactable in interactables)
                {
                    interactable.IsSelected = false;
                }
                
                interactables.Clear();
            }
        }
    }
}
