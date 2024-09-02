using Engine.Interfaces;
using Microsoft.Xna.Framework.Content;

namespace Engine.Services;

public class Loader(ContentManager content) : ILoader
{
    private readonly ContentManager _content = content;

    public T Load<T>(string assetName)
    {
        return _content.Load<T>(assetName);
    }
}
