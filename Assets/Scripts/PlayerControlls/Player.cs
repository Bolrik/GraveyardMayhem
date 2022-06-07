using Input;
using Misc;
using Misc.AssetVariables;
using Motion;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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



        [SerializeField] private PlayerData data;
        public PlayerData Data { get { return data; } }


        [Header("Variables")]
        [SerializeField] private PlayerVariable playerInstance;
        public PlayerVariable PlayerInstance { get { return playerInstance; } }

        [SerializeField] private TransformVariable viewVariable;
        public TransformVariable ViewVariable { get { return viewVariable; } }

        [SerializeField] private FloatVariable hitPoints;
        public FloatVariable HitPoints { get { return hitPoints; } }

        [SerializeField] private VolumeProfile volume;
        public VolumeProfile Volume { get { return volume; } }


        [SerializeField] private Color vignetteDamageColor;
        public Color VignetteDamageColor { get { return vignetteDamageColor; } }

        [SerializeField] private Color vignetteColor;
        public Color VignetteColor { get { return vignetteColor; } }


        float DamageTime { get; set; }


        void Awake()
        {
            if (this.Volume.TryGet<Vignette>(out Vignette vignette))
            {
                vignette.color.value = this.VignetteColor;
            }

            this.HitPoints.Value = this.Data.Health;

            this.PlayerInstance.Value = this;
            this.ViewVariable.Value = this.View.transform;

            this.Weapon.SetData(this.Weapon.Data);
        }

        public void Update()
        {
            if (this.Input.Quit.WasPressed)
            {
                Application.Quit();
                return;
            }

            if (this.Input.Restart.WasPressed)
            {
                SceneManager.LoadScene(0);
                return;
            }

            this.DamageTime = Mathf.Clamp01(this.DamageTime - Time.deltaTime);

            if (this.DamageTime > 0)
            {
                if (this.Volume.TryGet<Vignette>(out Vignette vignette))
                {
                    vignette.color.value = Color.Lerp(this.VignetteColor, this.VignetteDamageColor, this.DamageTime);
                }
            }

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

        public void Damage(float damage)
        {
            this.HitPoints.Value -= damage;
            this.DamageTime = 1;
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
