using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LessThanOk.GameData.GameObjects.Units;
using LessThanOk.Selecter.Events;
using LessThanOk.GameData.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using LessThanOk.Sprites;

namespace LessThanOk.UI.Frames.UIElements
{
    class StatPanel : UIElement
    {
        private Unit _selected;
        private String _text;
        private bool _draw;
        private SpriteFont _font;

        public StatPanel(String name, SpriteFont font, int x, int y)
        {
            _font = font;
            _posx = x;
            _posy = y;
            _name = name;
        }
        public void UnitSelectedEventHandler(object sender, SelectedEventArgs args)
        {
            if (args.Objects == null || args.Objects.Count <= 0)
            {
                _selected = null;
                return;
            }
            if (args.Objects.Count == 1 && args.Objects[0] is Unit)
            {
                _selected = (Unit)args.Objects[0];
            }
            else
                _selected = null;
        }
        public override void update(GameTime time)
        {
            if (_selected == null)
            {
                _draw = false;
                return;
            }
            _text = "ID:\t" + _selected.ID.ToString();
            _text += "Type:\t" + _selected.Type.Name;
            _text += "Pos:\t" + _selected.getPosition().ToString();
            _text += "HP:\t" + _selected.Health.ToString();
            if (_selected.Target is Unit)
                _text += "Target:\t" + ((Unit)_selected.Target).Type.Name;
            _text += "Engine:\t" + _selected.Engine.Type.Name;
        }
        public override void draw(SpriteBatch batch)
        {
            if (_draw)
                batch.DrawString(_font, _text, new Vector2((float)_posx, (float)_posy), Color.White);
        }
    }
}
