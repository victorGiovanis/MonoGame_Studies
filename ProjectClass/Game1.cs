using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectClass
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Color _bgColor;
        private double _time;

        // Temporizador
        private SpriteFont _font;
        private string _timeText;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            _bgColor = Color.Black;
            _time = 0.0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("arial24");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _timeText = string.Format("Tempo: {0}", gameTime.TotalGameTime.TotalSeconds);

            _time = _time + gameTime.ElapsedGameTime.TotalSeconds;
            if (_time > 2.0)
            {
                _time = 0.0;
                if (_bgColor == Color.White)
                {
                    _bgColor = Color.Black;
                }
                else
                {
                    _bgColor = Color.White;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_bgColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, _timeText, Vector2.Zero, Color.Purple);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
    