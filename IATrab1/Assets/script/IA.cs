using UnityEngine;
using System.Collections;
using System.Xml.Linq;

namespace Assets.script
{
    public class IA : MonoBehaviour
    {
        private bool _firstPlay;
        private Nodo _node;
        private void Start()
        {
            _firstPlay = true;
        }

        public void MakePlay()
        {
            if (_firstPlay)
            {
                FirstPlay();
               // _firstPlay = false;
                return;
            }

            //Se a jogada do jagador estiver na previsão da ia n cria nova arvore, 
            //utiliza o filho com o estado correspondente

            _node = new Nodo(Game.Instance.Board, true, null, new Vector2(-1, -1));
            

            Vector2 move = _node.StartConstructionAndSearch();
            Game.Instance.EndComputerTurn((int)move.x, (int)move.y);
        }

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
