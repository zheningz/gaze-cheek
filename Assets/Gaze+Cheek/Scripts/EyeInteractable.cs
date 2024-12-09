using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gazecheek.Scripts
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    
    public class EyeInteractable : MonoBehaviour
    {
        [Range(0f, 1f)] private float value = 0.5f;
        private bool isSelected = false;
        
        [SerializeField] private Material onHoverSelectedMaterial;
        [SerializeField] private Material onHoverInActiveMaterial;
        
        private Slider slider;
        private MeshRenderer meshRenderer;
        
        public float Value => value;
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }

        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            slider = GetComponentInChildren<Slider>();
            slider.value = value;
        }
    
        void Update()
        {
            if (isSelected)
            {
                meshRenderer.material = onHoverSelectedMaterial;
                value = slider.value;
                slider.gameObject.SetActive(true);
            }
            else
            {
                meshRenderer.material = onHoverInActiveMaterial;
                slider.gameObject.SetActive(false);
            }
        }
    }
}
