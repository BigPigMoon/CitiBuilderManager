namespace Engine.Interfaces;

public interface ILoader
{
    T Load<T>(string assetName);
}
