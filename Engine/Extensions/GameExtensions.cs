using Arch.Core;
using Engine.Interfaces;
using Engine.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CitiBuilderManager;

public static class GameExtensions
{
    public static void ConfigureEngineServices(this Game app, IServiceCollection services)
    {
        services.AddSingleton(World.Create());

        services.AddSingleton<IWindowManager>(new WindowManager(app.GraphicsDevice));
        services.AddSingleton<IDrawer>(new Drawer(new SpriteBatch(app.GraphicsDevice)));
        services.AddSingleton<ILoader>(new Loader(app.Content));
        services.AddSingleton<IKeyboardInput, KeyboardInput>();
        services.AddSingleton<IMouseInput, MouseInput>();
        services.AddSingleton<ICamera2D, Camera2D>();
    }
}
