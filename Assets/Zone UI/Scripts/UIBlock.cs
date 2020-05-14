using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Michsky.UI.Zone
{
    public class UIBlock : MonoBehaviour
    { 
        [Header("EVENTS")]
        [SerializeField] private UnityEvent _selected;
        [SerializeField] private UnityEvent _deselected;

        private bool _isSelected = false;

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;

            if (_isSelected)
                _selected.Invoke();
            else
                _deselected.Invoke();
        }
    }
}