namespace Engine.Systems;

public interface ISystem<T>
{
    void Run(in T state);
}
