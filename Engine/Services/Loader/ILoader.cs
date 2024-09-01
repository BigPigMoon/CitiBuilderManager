namespace Engine.Services;

public interface ILoader
{
    T Load<T>(string assetName);
}
