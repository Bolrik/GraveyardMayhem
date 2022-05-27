using UnityEngine;

namespace Input
{
    public interface IInput
    {
        void GetMove(out Vector2 value, out float pressTime);
        void GetViewDelta(out Vector2 value);

        void GetAction(out bool value);
    }
}