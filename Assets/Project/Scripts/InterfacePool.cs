public interface IInteractable
{
    void Interact();
    void EndInteract();
    bool IsActive();
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

public interface INotes
{
    string ID { get; }
    public void ClearNote();
}

public interface IClearable
{
    string ID { get; }
    public void Clear();
}

public interface IMuzzles
{
    string ID { get; }
    public void Solve();
}

public interface IItems
{
    string ID { get; }
    public void Read();
}
