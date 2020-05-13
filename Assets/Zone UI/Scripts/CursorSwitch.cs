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

        private int _cursorIndex = 0;
        public int CursorIndex => _cursorIndex;

        private float _activationTime = 0;
        private bool _awaitingActivation => _activationTime > 0;


        private void OnValidate()
        {
            foreach (Block block in _blocks)
            {
                if (block.Order < 1 || block.Order > _blocks.Count)
                {
                    block.SetOrder(DetermineOrder());
                }

                for (int i = 0; i < _blocks.Count; i++)
                {
                    if (block.Order == _blocks[i].Order && block != _blocks[i])
                    {
                        block.SetOrder(DetermineOrder());
                    }
                }
            }

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

            foreach (Block block1 in _blocks)
            {
                if (block1._Block != null)
                {
                    foreach (Block block2 in _blocks)
                    {
                        if (block2._Block != null)
                        {
                            if (block1 != block2 && block1._Block == block2._Block)
                            {
                                block2.ClearBlock();
                            }
                        }
                    }
                }
            }
        }

        private void Update()
        {
            if (_isActive)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    SwitchBlock(1);
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    SwitchBlock(-1);
                }
            }
            else if(_awaitingActivation)
            {
                _activationTime -= Time.deltaTime;
                
                if (_activationTime <= 0)
                {
                    _activationTime = 0;
                    _isActive = true;
                    SetSelectBlock(true);
                }
            }
        }

        public void ActivationCoursore()
        {
            _isActive = true;

            SetSelectBlock(true);
        }

        public void ActivationCoursore(float time)
        {
            if (time > 0)
            {
                _activationTime = time;
            }
            else
            {
                ActivationCoursore();
            }
        }

        public void DeactivationCoursore()
        {
            _isActive = false;
            SetSelectBlock(false);
            _cursorIndex = 0;
        }

        private int DetermineOrder()
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
                {
                    return order;
                }
            }
            return 0;
        }

        private void SetSelectBlock(bool isSelect)
        {
            _blocks[_cursorIndex].SelectBlock(isSelect);
        }

        private void SwitchBlock(int step)
        {
            if (_isActive)
            {
                SetSelectBlock(false);

                _cursorIndex += step;

                if (_cursorIndex >= _blocks.Count)
                {
                    _cursorIndex -= _blocks.Count;
                }
                else if (_cursorIndex < 0)
                {
                    _cursorIndex += _blocks.Count;
                }

                SetSelectBlock(true);
            }
        }
    }
}