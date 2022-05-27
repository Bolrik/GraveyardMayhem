using Input;
using PlayerControlls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    class Enemy : MonoBehaviour, IInput
    {
        [SerializeField] private Player player;
        public Player Player { get { return player; } }

        public InputButton Move => throw new NotImplementedException();

        public InputState ViewDelta => throw new NotImplementedException();

        public InputButton LeftClick => throw new NotImplementedException();
    }
}