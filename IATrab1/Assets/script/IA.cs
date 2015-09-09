using UnityEngine;
using System.Collections;

namespace Assets.script
{
    public class IA : MonoBehaviour
    {
        private bool _firstPlay;
        private Nodo _node;
        private int[][] _valueBoard;
        private void Start()
        {
            _firstPlay = true;
            _valueBoard = new int[15][];
            for (int i = 0; i < 15 ; i++)
            {
                _valueBoard[i] = new int[15];
                for (int j = 0; j < 15; j++)
                {
                    _valueBoard[i][j] = 0;
                }
            }
        }

        public void MarkPlayerTurn(int i, int j)
        {
            _valueBoard[i][j] = 2000;
        }

        public void MakePlay()
        {
            if (_firstPlay)
            {
                FirstPlay();
               _firstPlay = false;
                return;
            }

            //Se a jogada do jogador estiver na previsão da ia n cria nova arvore, 
            //utiliza o filho com o estado correspondente

            _node = new Nodo(Game.Instance.Board, true, null, new Vector2(-1, -1),0,-1000000,1000000);
            
            
            Vector2 move = _node.StartConstructionAndSearch();
            Game.Instance.EndComputerTurn((int)move.x, (int)move.y);
        }

//        public void ChangeBoardValue(int i, int j)
//        {
//            for (int n = -4; n < 5; n++)
//            {
//                if(i + n < 0 || i + n > 14)
//                    continue;
//
//                for (int m = -4; m < 5; m++)
//                {
//                    if (j + m < 0 || j + m > 14)
//                        continue;
//                    
//                }
//            }
//        }
//
//        public void DefinePlaceValue(int i , int j)
//        {
//            
//        }
//
//        private int CheckHorizontally(int i, int j,int v)
//        {
//            int firstCheckSum = 0;
//            int secondCheckSum = 0;
//            for (int m = 0; m <= 5; m++)
//            {
//                firstCheckSum = m;
//                if (i + m >= 15)
//                    break;
//
//                if (_valueBoard[i + m][j] != v)
//                    break;
//            }
//
//            for (int m = 0; m >= -5; m--)
//            {
//                secondCheckSum = -m;
//                if (i + m < 0)
//                    break;
//
//                if (_valueBoard[i + m][j] != v)
//                    break;
//            }
//
//            return (firstCheckSum + secondCheckSum);
//        }
////
//        private bool CheckVertically(int i, int j)
//        {
//            int v = _valueBoard[i][j];
//
//            int firstCheckSum = 0;
//            int secondCheckSum = 0;
//            for (int m = 0; m <= 5; m++)
//            {
//                firstCheckSum = m;
//                if (j + m >= 15)
//                    break;
//
//                if (_valueBoard[i][j + m] != v)
//                    break;
//            }
//
//            for (int m = 0; m >= -5; m--)
//            {
//                secondCheckSum = -m;
//                if (j + m < 0)
//                    break;
//
//                if (_valueBoard[i][j + m] != v)
//                    break;
//
//            }
//
//            return (firstCheckSum + secondCheckSum > 5);
//        }
//
//        private bool CheckDiagonalAsc(int i, int j)
//        {
//            int v = _valueBoard[i][j];
//
//            int firstCheckSum = 0;
//            int secondCheckSum = 0;
//            for (int m = 0; m <= 5; m++)
//            {
//                firstCheckSum = m;
//                if (i + m >= 15 || j + m >= 15)
//                    break;
//
//                if (_valueBoard[i + m][j + m] != v)
//                    break;
//
//            }
//
//
//            for (int m = 0; m >= -5; m--)
//            {
//                secondCheckSum = -m;
//                if (i + m < 0 || j + m < 0)
//                    break;
//
//                if (_valueBoard[i + m][j + m] != v)
//                    break;
//
//            }
//
//            return (firstCheckSum + secondCheckSum > 5);
//        }
//
//        private bool CheckDiagonalDesc(int i, int j)
//        {
//            int v = _valueBoard[i][j];
//
//            int firstCheckSum = 0;
//            int secondCheckSum = 0;
//            for (int m = 0; m <= 5; m++)
//            {
//                firstCheckSum = m;
//                if (i + m >= 15 || j - m < 0)
//                    break;
//
//                if (_valueBoard[i + m][j - m] != v)
//                    break;
//
//            }
//
//
//            for (int m = 0; m >= -5; m--)
//            {
//                secondCheckSum = -m;
//                if (i + m < 0 || j - m >= 15)
//                    break;
//
//                if (_valueBoard[i + m][j - m] != v)
//                    break;
//
//            }
//
//            return (firstCheckSum + secondCheckSum > 5);
//        }

        public void FirstPlay()
        {
            int i = (int)Random.Range(0, 15);
            int j = (int)Random.Range(0, 15);
            for (int c = 0; c < 5; c++)
            {
                i = (int)Random.Range(0, 15);
                j = (int)Random.Range(0, 15);

                if (Game.Instance.Board[i][j] == 0)
                    break;
            }
            
            Game.Instance.EndComputerTurn(i, j);
        }
    }
}
