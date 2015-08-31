using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.script
{
    class Nodo
    {

        private float   _beta;
        private float   _alfa;
        private Nodo    _parent;
        private float   _value;
        private int[][] _board;
        private bool    _isMaxNode;
    
        public Nodo(int[][] board, bool isMaxNode, Nodo parent)
        {

            _value      = (_isMaxNode) ? -10000000 : 10000000;
            _alfa       = -10000000;
            _board      = board;
            _isMaxNode  = isMaxNode;
            _parent     = parent;

            StartConstructionAndSearch();
        }

        private void StartConstructionAndSearch()
        {
            
        }

        public void SetBeta(float beta)
        {
            _beta = beta;

            if (!_isMaxNode)
                if (_value < beta)
                    _value = beta;
        }

        public void SetAlfa(float alfa)
        {
            _alfa = alfa;

            if (_isMaxNode)
                if (_value < alfa)
                    _value = alfa;
                
            
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
