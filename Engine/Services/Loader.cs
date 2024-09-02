using Engine.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace Engine.Services;

public class Loader : ILoader
{
    private readonly ContentManager _content;

    public Loader(ContentManager content)
    {
        _content = content;
    }

    public T Load<T>(string assetName)
    {
        return _content.Load<T>(assetName);
    }
}
