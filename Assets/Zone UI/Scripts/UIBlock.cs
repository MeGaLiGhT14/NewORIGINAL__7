using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Michsky.UI.Zone
{
    public class UIBlock : MonoBehaviour
    { 
        [Header("EVENTS")]
        [SerializeField] private UnityEvent _selectedEvent;
        [SerializeField] private UnityEvent _deselectedEvent;

        private bool _isSelected = false;

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;

            if (_isSelected)
                _selectedEvent.Invoke();
            else
                _deselectedEvent.Invoke();
        }
    }
}