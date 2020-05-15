﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Michsky.UI.Zone
{
    [Serializable]
    public class Block
    {
        [SerializeField] private int _order;
        public int Order => _order;

        [SerializeField] private UIBlock _block;
        public UIBlock UIObject => _block;

        public void SetOrder(int order)
        {
            _order = order;
        }

        public void ClearBlock()
        {
            _block = null;
        }

        public void SetSelected(bool isSelected)
        {
            _block.SetSelected(isSelected);
        }
    }
}