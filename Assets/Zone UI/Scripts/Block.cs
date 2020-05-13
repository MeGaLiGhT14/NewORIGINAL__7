using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Michsky.UI.Zone
{
    [System.Serializable]
    public class Block
    {
        [SerializeField] private int _order;
        public int Order => _order;

        [SerializeField] private UIBlock _block;
        public UIBlock _Block => _block;

        public void SetOrder(int order)
        {
            _order = order;
        }

        public void ClearBlock()
        {
            _block = null;
        }

        public void SelectBlock(bool isSelected)
        {
            _block.SelectBlock(isSelected);
        }
    }
}