using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Gazecheek.Scripts
{
    [RequireComponent(typeof(OVRFaceExpressions))]
    public class CheekController : MonoBehaviour
    {
        [SerializeField]
        private float sensitivity = 0.2f;
    
        [SerializeField]
        private float thresholdUp = 0.4f;
    
        [SerializeField]
        private float thresholdDown = 0.4f;

        private Slider slider;
    
        private OVRFaceExpressions expressions;
    
        [SerializeField]
        public Transform eye;
    
        public List<EyeInteractable> interactables;

        void Start()
        {
            expressions = GetComponent<OVRFaceExpressions>();
            interactables = eye.GetComponent<EyeTrackingRay>().Interactables;
        }
    
        void Update()
        {
            float increment = expressions.GetWeight(OVRFaceExpressions.FaceExpression.CheekPuffL);
            float decrement = expressions.GetWeight(OVRFaceExpressions.FaceExpression.CheekSuckL);
        
            Debug.Log("CheekPuff:" + increment.ToString("0.00"));
            Debug.Log("CheekSuck:" + decrement.ToString("0.00"));

            if (increment >= thresholdUp)
            {
                // Debug.Log("Increase");
                foreach (var interactable in interactables)
                {
                    if (interactable.IsSelected)
                    {
                        slider = interactable.GetComponentInChildren<Slider>();
                        slider.enabled = true;
                        slider.value += Time.deltaTime * sensitivity;
                    }
                }
            }
        
            if (decrement >= thresholdDown)
            {
                // Debug.Log("Decrease");
                foreach (var interactable in interactables)
                {
                    if (interactable.IsSelected)
                    {
                        slider = interactable.GetComponentInChildren<Slider>();
                        slider.enabled = true;
                        slider.value -= Time.deltaTime * sensitivity;
                    }
                }
            }
        }
    }
}
