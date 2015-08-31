using UnityEngine;
using System.Collections;
using System.Xml.Linq;

namespace Assets.script
{
    public class IA : MonoBehaviour
    {
        private bool _firstPlay;
        private void Start()
        {
            _firstPlay = true;
        }

        public void MakePlay()
        {
            if (_firstPlay)
            {
                FirstPlay();
                _firstPlay = false;
                return;
            }


            Nodo n = new Nodo(Game.Instance.Board, true, null);
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
