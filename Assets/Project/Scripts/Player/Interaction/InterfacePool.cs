public interface IInteractable
{
    void Interact();
    void EndInteract();
    void OnMuzzleSolve();
}

public interface IClickable
{
    void SetValue();
}

public interface IResetable
{
    void Reset();
}
