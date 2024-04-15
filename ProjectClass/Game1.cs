using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClass;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private Texture2D _ball;
    private Vector2 _ballPosition;
    private Vector2 _ballDirection;
    private float _ballSpeed;

    private Texture2D _bar;
    private Vector2 _bar1Position;
    private Vector2 _bar2Position;
    private float _barSpeed;

    Random _random;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        base.Initialize();

        _random = new Random();

        _ballPosition = new Vector2((_graphics.PreferredBackBufferWidth - _ball.Width) / 2.0f, (_graphics.PreferredBackBufferHeight - _ball.Height) / 2.0f);
        _ballSpeed = 5.0f;
        _ballDirection = new Vector2(1.0f, GetRandomY());
        _ballDirection.Normalize();

        _bar1Position = new Vector2(0.0f, (_graphics.PreferredBackBufferHeight - _bar.Height) / 2.0f);
        _bar2Position = new Vector2(_graphics.PreferredBackBufferWidth - _bar.Width, (_graphics.PreferredBackBufferHeight - _bar.Height) / 2.0f);
        _barSpeed = 5.0f;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _ball = Content.Load<Texture2D>("ball");
        _background = Content.Load<Texture2D>("background");
        _bar = Content.Load<Texture2D>("bar");
    }

    protected override void Update(GameTime gameTime)
    {
        // Comandos de Entrada
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }
        //

        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.W))
        {
            _bar1Position.Y -= _ballSpeed;
        }

        if (keyboardState.IsKeyDown(Keys.S))
        {
            _bar1Position.Y += _ballSpeed;
        }

        _bar2Position.Y = Mouse.GetState().Y - (_bar.Height / 2.0f);
        //

        // Colisão das barras com os limites verticais
        if (_bar1Position.Y < 0)
        {
            _bar1Position.Y = 0.0f;
        }
        else if (_bar1Position.Y > _graphics.PreferredBackBufferHeight - _bar.Height)
        {
            _bar1Position.Y = _graphics.PreferredBackBufferHeight - _bar.Height;
        }

        if (_bar2Position.Y < 0)
        {
            _bar2Position.Y = 0.0f;
        }
        else if (_bar2Position.Y > _graphics.PreferredBackBufferHeight - _bar.Height)
        {
            _bar2Position.Y = _graphics.PreferredBackBufferHeight - _bar.Height;
        }
        //

        // Movimentação da bola
        _ballPosition = _ballPosition + (_ballDirection * _ballSpeed);
        //

        // Colisão da bola com as barras
        if (_ballPosition.X < _bar.Width)
        {
            if ((_ballPosition.Y + _ball.Height > _bar1Position.Y) && (_ballPosition.Y < _bar1Position.Y + _bar.Height))
            {
                _ballPosition.X = _bar.Width;
                _ballDirection = new Vector2(1.0f, GetRandomY());
                _ballDirection.Normalize();
            }
        }
        else if (_ballPosition.X + _ball.Width > _graphics.PreferredBackBufferWidth - _bar.Width)
        {
            if ((_ballPosition.Y + _ball.Height > _bar2Position.Y) && (_ballPosition.Y < _bar2Position.Y + _bar.Height))
            {
                _ballPosition.X = _graphics.PreferredBackBufferWidth - _bar.Width - _ball.Width;
                _ballDirection = new Vector2(-1.0f, GetRandomY());
                _ballDirection.Normalize();
            }
        }
        //

        // Colisão da bola com os limites horizontais
        if ((_ballPosition.X + _ball.Width < 0.0f) || (_ballPosition.X > _graphics.PreferredBackBufferWidth))
        {
            _ballPosition = new Vector2((_graphics.PreferredBackBufferWidth - _ball.Width) / 2.0f, (_graphics.PreferredBackBufferHeight - _ball.Height) / 2.0f);
        }
        //

        //Colisão da bola com os limites verticais
        if (_ballPosition.Y < 0)
        {
            _ballPosition.Y = 0.0f;
            _ballDirection.Y = -_ballDirection.Y;
        }
        else if (_ballPosition.Y > _graphics.PreferredBackBufferHeight - _ball.Height)
        {
            _ballPosition.Y = _graphics.PreferredBackBufferHeight - _ball.Height;
            _ballDirection.Y = -_ballDirection.Y;
        }
        //

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_ball, _ballPosition, Color.White);
        _spriteBatch.Draw(_bar, _bar1Position, Color.White);
        _spriteBatch.Draw(_bar, _bar2Position, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private float GetRandomY()
    {
        return (_random.NextSingle() * 2.0f) - 1.0f;
    }
}
