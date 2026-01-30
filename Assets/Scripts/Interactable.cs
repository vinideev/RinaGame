using UnityEngine;

public interface IControllable
{
    void OnMove(Vector2 input);
    void OnChange();

    void InvokeCat();
}
