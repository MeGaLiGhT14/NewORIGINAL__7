using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Michsky.UI.Zone
{
    public class UIBlock : MonoBehaviour
    {
        [Header("RESOURCES")]
        [SerializeField] private Animator _animator;
        [SerializeField] private UIElementSound _elementSound;

        [Header("SETTINGS")]
        [SerializeField] private bool _enableAnimator = false;
        [SerializeField] private bool _enableSounds = false;

        private void OnValidate()
        {
            if (_animator == null)
                _enableAnimator = false;

            if (_elementSound == null)
                _enableSounds = false;
        }

        private bool _isSelected = false;

        public void SelectBlock(bool isSelected)
        {
            _isSelected = isSelected;

            if (_isSelected)
            {
                if (_enableAnimator) {
                    _animator.Play("Highlighted");
                }

                if (_elementSound)
                {
                    _elementSound.PlayHoverSound();
                }
            }
            else
            {
                if (_enableAnimator)
                    _animator.Play("Normal");
            }
        }

    }
}