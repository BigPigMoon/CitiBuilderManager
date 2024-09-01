using Arch.Core;
using Engine.Attributes;
using Engine.Services;
using Engine.Systems;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitiBuilderManager;

public class Application : Game
{
    private ServiceProvider _serviceProvider;
    private readonly GraphicsDeviceManager _graphics;

    private readonly List<ISystem<int>> _startupSystems = [];
    private readonly List<ISystem<GameTime>> _updateSystems = [];
    private readonly List<ISystem<GameTime>> _drawSystems = [];

    public Application()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(World.Create());

        services.AddSingleton<IWindowManager>(new WindowManager(GraphicsDevice));
        services.AddSingleton<IDrawer>(new Drawer(new SpriteBatch(GraphicsDevice)));
        services.AddSingleton<ILoader>(new Loader(Content));
    }

    protected override void Initialize()
    {
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();

        InitializeAutoInjectComponents(_serviceProvider);

        base.Initialize();
    }

    protected override void BeginRun()
    {
        base.BeginRun();

        foreach (var system in _startupSystems)
        {
            system.Run(0);
        }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        foreach (var system in _updateSystems)
        {
            system.Run(in gameTime);
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        float baseColor = 0.2f;
        GraphicsDevice.Clear(new Color(baseColor, baseColor, baseColor));

        foreach (var system in _drawSystems)
        {
            system.Run(in gameTime);
        }

        base.Draw(gameTime);
    }

    private void InitializeAutoInjectComponents(IServiceProvider serviceProvider)
    {
        var autoInjectableTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.GetCustomAttributes(typeof(AutoInjectAttribute), true).Length != 0);

        foreach (var type in autoInjectableTypes)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor != null)
            {
                var parameters = constructor.GetParameters();
                var dependencies = parameters.Select(p => serviceProvider.GetService(p.ParameterType)).ToArray();
                var system = constructor.Invoke(dependencies);

                if (type.GetCustomAttributes(typeof(OnStartupAttribute), true).Length != 0)
                {
                    _startupSystems.Add((ISystem<int>)system);
                }
                else if (type.GetCustomAttributes(typeof(OnDrawAttribute), true).Length != 0)
                {
                    _drawSystems.Add((ISystem<GameTime>)system);
                }
                else if (type.GetCustomAttributes(typeof(OnUpdateAttribute), true).Length != 0)
                {
                    _updateSystems.Add((ISystem<GameTime>)system);
                }
            }
        }
    }
}
