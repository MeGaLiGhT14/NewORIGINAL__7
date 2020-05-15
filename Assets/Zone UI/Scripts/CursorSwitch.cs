using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Michsky.UI.Zone
{
    public class CursorSwitch : MonoBehaviour
    {
        [SerializeField] private bool _isActive = false;
        [SerializeField] private List<Block> _blocks;

        public int CursorIndex { get; private set; }

        private float _activationTime = 0;

        private bool _awaitingActivation => _activationTime > 0;

        private void OnValidate()
        {
            if (_blocks != null)
            {
                foreach (Block block in _blocks)
                {
                    ClampOrder(block);

                    if (CheckDuplicateOrder(block))
                        Debug.LogError("Block order cannot be repeated in one list");
                }

                SortBlocks();

                if (CheckDuplicateBlock())
                    Debug.LogError("The same block can be in the same list only once");
            }
        }

        private void Start()
        {
            if(CheckEmptyBlocks())
                Debug.LogError("Block cannot be empty");
        }

        private void Update()
        {
            if (_isActive)
            {
                if (Input.GetKeyDown(KeyCode.D))
                    SwitchBlock(1);
                else if (Input.GetKeyDown(KeyCode.A))
                    SwitchBlock(-1);
            }
            else if (_awaitingActivation)
            {
                _activationTime -= Time.deltaTime;

                if (_activationTime <= 0)
                {
                    _activationTime = 0;
                    ActivateCursor();
                }
            }
        }

        public void ActivateCursor()
        {
            _isActive = true;
            SetSelectBlock(true);
        }

        public void ActivateCursorOverTime(float time)
        {
            if (time > 0)
                _activationTime = time;
            else
                ActivateCursor();
        }

        public void DeactivateCursor()
        {
            _isActive = false;
            SetSelectBlock(false);
            CursorIndex = 0;
        }

        private int GetFreeOrder()
        {
            bool orderIsFree;

            for (int order = 1; order <= _blocks.Count; order++)
            {
                orderIsFree = true;
                foreach (Block block in _blocks)
                {
                    if (block.Order == order)
                    {
                        orderIsFree = false;
                        break;
                    }
                }

                if (orderIsFree)
                    return order;
            }
            return 0;
        }

        private void ClampOrder(Block block)
        {
            if (block.Order < 1 || block.Order > _blocks.Count)
            {
                Debug.LogWarning("Order cannot be outside the list");
                block.SetOrder(GetFreeOrder());
            }
        }

        private bool CheckDuplicateOrder(Block block)
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                if (block.Order == _blocks[i].Order && block != _blocks[i])
                {
                    return true;
                }
            }
            return false;
        }

        private void SortBlocks()
        {
            for (int i = 1; i <= _blocks.Count; i++)
            {
                for (int j = 0; j < _blocks.Count - i; j++)
                {
                    if (_blocks[j].Order > _blocks[j + 1].Order)
                    {
                        Block transit = _blocks[j];
                        _blocks[j] = _blocks[j + 1];
                        _blocks[j + 1] = transit;
                    }
                }
            }
        }

        private bool CheckDuplicateBlock()
        {
            foreach (Block block1 in _blocks)
            {
                if (block1.UIObject != null)
                {
                    foreach (Block block2 in _blocks)
                    {
                        if (block2.UIObject != null)
                        {
                            if (block1 != block2 && block1.UIObject == block2.UIObject)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool CheckEmptyBlocks()
        {
            foreach (Block block in _blocks)
            {
                if (block.UIObject == null)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetSelectBlock(bool isSelect)
        {
            _blocks[CursorIndex].SetSelected(isSelect);
        }

        private void SwitchBlock(int step)
        {
            if (_isActive)
            {
                SetSelectBlock(false);

                CursorIndex += step;

                if (CursorIndex >= _blocks.Count)
                    CursorIndex -= _blocks.Count;
                else if (CursorIndex < 0)
                    CursorIndex += _blocks.Count;

                SetSelectBlock(true);
            }
        }
    }
}