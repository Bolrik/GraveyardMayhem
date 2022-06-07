using Input;
using Misc;
using Misc.AssetVariables;
using Motion;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlayerControlls
{
    public class Player : MonoBehaviour, IInput
    {
        [Header("References")]
        [SerializeField] private InputController input;
        public InputController Input { get { return input; } }

        [SerializeField] private WeaponSlot weapon;
        public WeaponSlot Weapon { get { return weapon; } }

        [SerializeField] private Camera view;
        public Camera View { get { return view; } }


        [SerializeField] private MiscManager miscManager;
        public MiscManager MiscManager { get { return miscManager; } }


        [Header("Variables")]
        [SerializeField] private PlayerVariable playerInstance;
        public PlayerVariable PlayerInstance { get { return playerInstance; } }


        void Awake()
        {
            this.PlayerInstance.Value = this;
        }

        public void Update()
        {
            this.Weapon.Update();

            if (this.Input.Action.IsPressed)
            {
                if (this.Weapon.Fire(this.View.transform, out ShotInfo[] infos))
                {
                    Vector3 origin = this.Weapon.BulletOrigin;

                    foreach (var info in infos)
                    {
                        if (info.IsHit)
                        {
                            this.MiscManager.CreateTrail(origin, info.Hit.point);
                        }
                        else
                        {
                            Vector3 target = info.Origin + info.Direction * this.Weapon.Range;
                            Debug.Log(target);
                            this.MiscManager.CreateTrail(origin, target);
                        }
                    }
                }
            }
        }

        public void PickUp(WeaponData data)
        {
            this.Weapon.SetData(data);
        }

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
    }
}
