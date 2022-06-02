using Input;
using Motion;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControlls
{
    public class Player : MonoBehaviour, IInput
    {
        [SerializeField] private InputController input;
        public InputController Input { get { return input; } }

        [SerializeField] private WeaponSlot weapon;
        public WeaponSlot Weapon { get { return weapon; } }

        [SerializeField] private Camera view;
        public Camera View { get { return view; } }



        #region IInput
        public void GetAction(out bool value)
        {
            value = this.Input.Action.IsPressed;
        }

        public void GetMove(out Vector2 value, out float pressTime)
        {
            value = this.Input.Move.GetVector2();
            pressTime = this.Input.Move.DownTime;
        }

        public void GetViewDelta(out Vector2 value)
        {
            value = this.Input.ViewDelta.GetVector2();
        }
        #endregion

        public void Update()
        {
            this.Weapon.Update();

            if (this.Input.Action.WasPressed)
            {
                if (this.Weapon.Fire(this.View.transform, out RaycastHit[] hits))
                {

                }
            }
        }
    }
}
