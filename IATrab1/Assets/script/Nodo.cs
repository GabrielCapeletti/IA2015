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
        private int[][] _valuedBoard;
        private bool    _isMaxNode;
        private Vector2 _chosenMove;
        private Vector2 _originalMove;
        private int     _level;
        private int     _childPos;
        private bool    _stop;
        private Nodo    _chosenSon;
        private List<Vector2> _testedPlaces; 
        public Nodo(int[][] board, bool isMaxNode, Nodo parent,Vector2 original, int level,float initAlfa,float  initBeta)
        {
            
            _value        = 0;
            _alfa         = initAlfa;
            _beta         = initBeta;
            _board        = new int[15][];
            _valuedBoard = new int[15][];
            for (int i = 0; i < 15; i++)
            {
                _valuedBoard[i] = new int[15];
                _board [i] = new int[15];
                for (int j = 0; j < 15; j++)
                {
                    _board[i][j] = board[i][j];
                }
            }

           
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
            String table = "";
            if (_isMaxNode)
            {
               // Debug.Log(_chosenSon);
                for (int i = 14; i >= 0; i--)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        table = table + " " + _valuedBoard[j][i] + " ";
                    }
                    table += "\n";
                }

                Debug.Log(table);
                //return _chosenSon._originalMove;
            }
           // return Vector2.zero;
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


                    int[][] newBoard = new int[15][];
                    for (int p = 0; p < 15; p++)
                    {
                        newBoard[p] = new int[15];
                        for (int q = 0; q < 15; q++)
                        {
                            newBoard[p][q] = _board[p][q];
                        }
                    }
                   
                    newBoard[(int) move.x][(int) move.y] = 1;
                    Nodo filho = new Nodo(newBoard,  !_isMaxNode, this, move, _level + 1, _alfa ,_beta);
                    filho.StartConstructionAndSearch();
                    _valuedBoard[(int) move.x][(int) move.y] = (int)filho._value;
                    if (_stop)
                    {
                        //Debug.Log("STOP");
                        break;
                    }
                }
            }
            else
            {
                _value = CheckHoriValue((int)_originalMove.x, (int)_originalMove.y) + CheckVertValue((int)_originalMove.x, (int)_originalMove.y)
                    + CheckDiagAscValue((int)_originalMove.x, (int)_originalMove.y) + CheckDiagDescValue((int)_originalMove.x, (int)_originalMove.y);
                _chosenMove = _originalMove;
            }

            if (_parent != null)
            {
                if (_isMaxNode)
                {
                    _parent.SetBeta(_value, _originalMove,this);
                    return;
                }
                _parent.SetAlfa(_value, _originalMove,this);
            }
        }

        private int CheckHoriValue(int i, int j)
        {
            int biggest = 0;
            int seqCount = 0;
            int pieceCount = 0;
            bool firstSimbol = true;
            int turn = 1;

//            String table = "fefefef";
//
//            for (int p = 14; p >= 0; p--)
//            {
//                for (int o = 0; o < 15; o++)
//                {
//                    table = table + " " + _board[p][o] + " ";
//                }
//                table += "\n";
//            }
//            Debug.Log(table);

            for (int n = 1; n < 5 ;n++)
            {
                if (i + n > 14 || i + n < 0)
                    break;

                if (_board[i + n][j] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i + n][j] == 1) ? 2: 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                       
                    }
                    else if (_board[i + n][j] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                       
                    }
                    else
                        break;
                }
               
            }
            

            for (int n = -1; n > -5; n--)
            {
                if (i + n > 14 || i + n < 0)
                    break;

                if (_board[i + n][j] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else 
                {
                    int bonus = (_board[i + n][j] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else if (_board[i + n][j] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else
                        break;
                }
               
            }

            if (seqCount < 4)
                biggest = 0;

            //Debug.Log("Biggest: "+biggest);
           // int sign = (_isMaxNode) ? -1 : 1;
            return biggest;
        }

        private int CheckVertValue(int i, int j)
        {
            int biggest = 0;
            int seqCount = 0;
            int pieceCount = 0;
            bool firstSimbol = true;
            int turn = 1;

            //            String table = "fefefef";
            //
            //            for (int p = 14; p >= 0; p--)
            //            {
            //                for (int o = 0; o < 15; o++)
            //                {
            //                    table = table + " " + _board[p][o] + " ";
            //                }
            //                table += "\n";
            //            }
            //            Debug.Log(table);

            for (int n = 1; n < 5; n++)
            {
                if (j + n > 14 || j + n < 0)
                    break;

                if (_board[i][j+n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i][j + n] == 1) ? 2: 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i ][j + n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else if (_board[i][j + n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else
                        break;
                }

            }


            for (int n = -1; n > -5; n--)
            {
                if (j + n > 14 || j + n < 0)
                    break;

                if (_board[i][j + n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i][j + n] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i][j + n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else if (_board[i][j + n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else
                        break;
                }

            }

            if (seqCount < 4)
                biggest = 0;

            //Debug.Log("Biggest: "+biggest);
            // int sign = (_isMaxNode) ? -1 : 1;
            return biggest;
        }

        private int CheckDiagAscValue(int i, int j)
        {
            int biggest = 0;
            int seqCount = 0;
            int pieceCount = 0;
            bool firstSimbol = true;
            int turn = 1;

            //            String table = "fefefef";
            //
            //            for (int p = 14; p >= 0; p--)
            //            {
            //                for (int o = 0; o < 15; o++)
            //                {
            //                    table = table + " " + _board[p][o] + " ";
            //                }
            //                table += "\n";
            //            }
            //            Debug.Log(table);

            for (int n = 1; n < 5; n++)
            {
                if (i + n > 14 || i + n < 0 || j + n > 14 || j + n < 0)
                    break;

                if (_board[i + n][j + n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i + n][j + n] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j + n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;

                    }
                    else if (_board[i + n][j + n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;

                    }
                    else
                        break;
                }

            }


            for (int n = -1; n > -5; n--)
            {
                if (i + n > 14 || i + n < 0 || j + n > 14 || j + n < 0)
                    break;

                if (_board[i + n][j + n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i + n][j + n] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j + n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else if (_board[i + n][j + n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else
                        break;
                }

            }

            if (seqCount < 4)
                biggest = 0;

            //Debug.Log("Biggest: "+biggest);
            // int sign = (_isMaxNode) ? -1 : 1;
            return biggest;
        }

        private int CheckDiagDescValue(int i, int j)
        {
            int biggest = 0;
            int seqCount = 0;
            int pieceCount = 0;
            bool firstSimbol = true;
            int turn = 1;

            //            String table = "fefefef";
            //
            //            for (int p = 14; p >= 0; p--)
            //            {
            //                for (int o = 0; o < 15; o++)
            //                {
            //                    table = table + " " + _board[p][o] + " ";
            //                }
            //                table += "\n";
            //            }
            //            Debug.Log(table);

            for (int n = 1; n < 5; n++)
            {
                if (i + n > 14 || i + n < 0 || j - n > 14 || j - n < 0)
                    break;

                if (_board[i + n][j - n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i + n][j - n] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j - n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;

                    }
                    else if (_board[i + n][j - n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;

                    }
                    else
                        break;
                }

            }


            for (int n = -1; n > -5; n--)
            {
                if (i + n > 14 || i + n < 0 || j - n > 14 || j - n < 0)
                    break;

                if (_board[i + n][j - n] == 0)
                {
                    biggest++;
                    seqCount++;
                }
                else
                {
                    int bonus = (_board[i + n][j - n] == 1) ? 2 : 0;
                    if (firstSimbol)
                    {
                        firstSimbol = false;
                        turn = _board[i + n][j - n];
                        //biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else if (_board[i + n][j - n] == turn)
                    {
                        biggest++;
                        pieceCount++;
                        biggest += pieceCount * 10 - Math.Abs(n * 2) + bonus;
                        seqCount++;
                    }
                    else
                        break;
                }

            }

            if (seqCount < 4)
                biggest = 0;

            //Debug.Log("Biggest: "+biggest);
            // int sign = (_isMaxNode) ? -1 : 1;
            return biggest;
        }
        
        public void SetBeta(float beta,Vector2 newMove, Nodo nodo)
        {
            if (!_isMaxNode)
            {
                if (beta < _beta)
                {
                    _beta = beta;
                    _value = beta;
                    _chosenMove = newMove;
                    _chosenSon = nodo;
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

        public void SetAlfa(float alfa, Vector2 newMove, Nodo nodo)
        {
            if (_isMaxNode)
            {
                if (alfa > _alfa)
                { 
                    _alfa = alfa;
                    _value = alfa;
                    _chosenMove = newMove;
                    _chosenSon = nodo;
                   //  Debug.Log("Novo alfa "+newMove);
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
