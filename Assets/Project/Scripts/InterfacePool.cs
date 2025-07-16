public interface IInteractable
{
    void Interact();
    void EndInteract();    
}

public interface ISolvable
{    
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

public interface ITouchable
{
    void OnTouch();
}

public interface IReadable
{
    void ShowName();
    void HideName();
}

public interface ITellable
{
    void Tell();
}
