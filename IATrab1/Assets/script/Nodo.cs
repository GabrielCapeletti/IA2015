using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.script
{
    /**
     * Nodo representantando cada estado.
     * O estado do tabuleiro é representado por uma matrix de inteiros
     * @ Gabriel Capeletti 
     */
    class Nodo
    {
        private float   _beta;
        private float   _alfa;
        private Nodo    _parent;
        private float   _value;
        private int[][] _board;
        private int[][] _valoedBoard;
        private bool    _isMaxNode;
        private Vector2 _chosenMove;
        private Vector2 _originalMove;
        private int     _level;
        private int     _childPos;
        private bool    _stop;

        private List<Vector2> _testedPlaces; 
        public Nodo(int[][] board, bool isMaxNode, Nodo parent,Vector2 original, int level,float initAlfa,float  initBeta)
        {
            
            _value        = 0;
            _alfa         = initAlfa;
            _beta         = initBeta;
            _board        = new int[15][];

            for (int i = 0; i < 15; i++)
            {
                _board [i] = new int[15];
                for (int j = 0; j < 15; j++)
                {
                    _board[i][j] = board[i][j];
                }
            }

            _valoedBoard  = new int[15][];
            _isMaxNode    = isMaxNode;
            _parent       = parent;
            _originalMove = original;
            _level        = level;
            _childPos     = 0;
            _stop         = false;
        }

        //Inicia busca por profundidade mais a esq/
        public Vector2 StartConstructionAndSearch()
        {
            CreateChild();
          //  Debug.Log("is father "+_isMaxNode+" move" +_chosenMove);
            return _chosenMove;
        }

        private void CreateChild()
        {
           
            if (_level < 1)
            {
                for (int i = 0; i < 225; i++)
                {
                    Vector2 move = new Vector2((int)i % 15, (int)(i/15) % 15);
                    if (_board[(int)move.x][(int)move.y] != 0)
                        continue;
                    
                    int[][] newBoard = _board;
                    newBoard[(int) move.x][(int) move.y] = 1;
                    Nodo filho = new Nodo(newBoard,  !_isMaxNode, this, move, _level + 1, _alfa ,_beta);
                    filho.StartConstructionAndSearch();
                    if (_stop)
                        break;
                }
            }
            else
            {
                int possibleValue = 0;
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (_board[i][j] != 0)
                            continue;


                        possibleValue = CheckHoriValue((int) _originalMove.x, (int) _originalMove.y);
                                // CheckVertValue((int) _originalMove.x, (int) _originalMove.y);

                        //  Debug.Log("Value : " + possibleValue);
                      //  Debug.Log("x : " + _originalMove.x);
                      //  Debug.Log("y : " + _originalMove.y); 
                        if (possibleValue > _value)
                        {
                            _value = possibleValue;
                        }
                    }
                }
                _chosenMove = _originalMove;
            }

            if (_parent != null)
            {
                if (_isMaxNode)
                {
                    _parent.SetBeta(_value, _originalMove);
                    return;
                }
                _parent.SetAlfa(_value, _originalMove);
            }
        }

        private int CheckHoriValue(int i, int j)
        {
            int biggest = 0;
            int turn = (_isMaxNode) ? 2 : 1;

            for (int n = -4; n < 5 ;n++)
            {
                if (i + n > 14 || i + n < 0)
                    continue;

                if (_board[i + n][j] == 0)
                    biggest++;
                else if (_board[i + n][j] == turn)
                    biggest += 2;
                else
                    return biggest;
            }
            
            return biggest;
        }

        private int CheckVertValue(int i, int j)
        {
            int biggest = 0;
            int turn = (_isMaxNode) ? 1 : 2;

            for (int n = 0; n < 5; n++)
            {
                if (j + n > 14)
                    continue;

                if (_board[i][j+n] == 0)
                    biggest++;
                else if (_board[i][j+n] == turn)
                    biggest += 2;
                else
                    return 0;
            }

            return biggest;
        }
        
        public void SetBeta(float beta,Vector2 newMove)
        {
            if (!_isMaxNode)
            {
                if (beta < _beta)
                {
                    _beta = beta;
                    _value = beta;
                    _chosenMove = newMove;
                    if (_beta < _alfa)
                    {
                        _stop = true;
                    }
                }
            }
            else
            {
                if(beta < _beta)
                    _beta = beta;
            }
        }

        public void SetAlfa(float alfa, Vector2 newMove)
        {
            if (_isMaxNode)
            {
                if (alfa > _alfa)
                {
                    _alfa = alfa;
                    _value = alfa;
                    _chosenMove = newMove;
                    if (_beta < _alfa)
                    {
                        _stop = true;
                    }
                }
            }
            else
            {
                if (alfa > _alfa)
                    _alfa = alfa;
            }
        }

        public float Beta
        {
            get { return _beta; }
        }

        public float Alfa
        {
            get { return _alfa; }
        }

    }
}
