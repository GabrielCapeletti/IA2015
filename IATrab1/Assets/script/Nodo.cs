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
        private bool    _isMaxNode;
        private Vector2 _chosenMove;
        private Vector2 _originalMove;
        

        private List<Vector2> _testedPlaces; 
        public Nodo(int[][] board, bool isMaxNode, Nodo parent,Vector2 original)
        {

            _value        = (_isMaxNode) ? -10000000 : 10000000;
            _alfa         = -10000000;
            _board        = board;
            _isMaxNode    = isMaxNode;
            _parent       = parent;
            _originalMove = original;
        }

        //Inicia busca por profundidade mais a esq/
        public Vector2 StartConstructionAndSearch()
        {
            CreateChild();

            return _chosenMove;
        }

        private void CreateChild()
        {
            Vector2 move = ChooseNextOption();
            int[][] newBoard = _board;
            _board[(int)move.x][(int)move.y] = 1;

            Nodo filho = new Nodo(newBoard, !_isMaxNode, this, move);

            //if (heuristica De Continuacao e de Poda)
            //    CreateChild();
            //else Obtem um valor total baseado no maior ponto possível no ponto play
            //  se for max considera as peças brancas [ 0, 5]
            //  se for min considera as peças pretas  [-5, 0]
            
            //Se n for raiz
            if (_parent != null)
            {
                if (_isMaxNode)
                {
                    _parent.SetBeta(_value,move);
                    return;
                }
                _parent.SetAlfa(_value,move);
            }

        }

        //Heuristica de escolha da jogada retornando posição a testar
        private Vector2 ChooseNextOption()
        {
            return new Vector2(1, 1);
        }

        public void SetBeta(float beta,Vector2 newMove)
        {
            if (!_isMaxNode)
                if (_value < beta)
                {
                    _value = beta;
                    _chosenMove = newMove;
                }
        }

        public void SetAlfa(float alfa, Vector2 newMove)
        {
            if (_isMaxNode)
                if (_value < alfa)
                {
                    _value = alfa;
                    _chosenMove = newMove;
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
