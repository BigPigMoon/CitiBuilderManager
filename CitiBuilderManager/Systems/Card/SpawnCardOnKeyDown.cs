using CitiBuilderManager.Interfaces;
using Engine.Attributes;
using Engine.Interfaces;
using Engine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CitiBuilderManager.Systems;

[AutoInject]
[OnUpdate]
internal class SpawnCardOnKeyDown(ICardSpawner spawner, IKeyboardInput keyboardInput) : ISystem
{
    private readonly ICardSpawner _spawner = spawner;
    private readonly IKeyboardInput _keyboardInput = keyboardInput;

    public void Run(in GameTime gameTime)
    {
        if (_keyboardInput.IsKeyJustPressed(Keys.Space))
        {
            _spawner.SpawnCard();
        }
    }
}
