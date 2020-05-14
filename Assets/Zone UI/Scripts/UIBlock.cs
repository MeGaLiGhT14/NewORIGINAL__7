using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Michsky.UI.Zone
{
    public class UIBlock : MonoBehaviour
    { 
        [Header("EVENTS")]
        [SerializeField] private UnityEvent _toSelected;
        [SerializeField] private UnityEvent _toDeselected;

        private bool _isSelected = false;

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;

            if (_isSelected)
                _toSelected.Invoke();
            else
                _toDeselected.Invoke();
        }
    }
}