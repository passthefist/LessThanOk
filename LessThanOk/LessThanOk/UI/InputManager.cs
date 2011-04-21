using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using LessThanOk.GameData.GameObjects;

namespace LessThanOk.UI
{
    public sealed class InputManager
    {
        public static ActiveGameObject Selected { get { return _selected; } }
        public static ActiveGameObject Target { get { return _target; } }

        private static ButtonState leftClick;
        private static ButtonState rightClick;
        private static KeyboardState keyboardLastState;
        
        private static ActiveGameObject _selected;
        private static ActiveGameObject _objectHover;
        private static ActiveGameObject _target;
        private static UIElement _elementHover;

        static readonly InputManager the = new InputManager();
        static InputManager() 
        {
            leftClick = Mouse.GetState().LeftButton;
            rightClick = Mouse.GetState().RightButton;
            //selected = new Dictionary<UInt16, ActiveGameObject>();
            keyboardLastState = Keyboard.GetState();
        }
        public static InputManager The { get { return the; } }
        public void init()
        {

        }

        public void update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            KeyboardState curKeyboardState = Keyboard.GetState();
            Vector2 mousePos = new Vector2(curMouseState.X, curMouseState.Y);

            UIElement curElement = UIManager.The.Root.getElementAt(mousePos);
            ActiveGameObject curObject = null;

            if (UIManager.The.Root is Frame_Game)
            {
                Frame_Game f = (Frame_Game)UIManager.The.Root;
                curObject = f.getObjectAt(mousePos);
            }

            setHover(curElement, curObject);

            // left click
            if (curMouseState.LeftButton.Equals(ButtonState.Pressed))
                leftClick = ButtonState.Pressed;
            else if (leftClick.Equals(ButtonState.Pressed))
            {
                // Left click detected
                leftClick = ButtonState.Released;
                
                if (curElement != null)
                {
                    curElement.LeftClickEvent.click(curElement);
                }
                else if (curObject != null)
                {
                    _selected = curObject;
                }
            }

            // right click
            if (curMouseState.RightButton.Equals(ButtonState.Pressed))
                rightClick = ButtonState.Pressed;

            else if (rightClick.Equals(ButtonState.Pressed))
            {
                // Right click detected
                rightClick = ButtonState.Released;
                if (curElement != null)
                {
                    curElement.RightClickEvent.click(curElement);
                }
                else if (curObject != null)
                {
                    if (_selected != null)
                    {
                        //move logic
                    }
                }
            }
        }

        private void setHover(UIElement curElement, ActiveGameObject curObject)
        {
            if (_elementHover == null)
                _elementHover = curElement;
            else if (curElement != null)
            {
                if (curElement.Name != _elementHover.Name)
                {
                    curElement.Image.Hover = true;
                    _elementHover.Image.Hover = false;
                }
            }
            if(_objectHover == null)
                _objectHover = curObject;
            else if (curObject != null)
            {
                if (curObject.ID != _objectHover.ID)
                {
                    //curObject.Sprite.Hover = true;
                    _elementHover.Image.Hover = false;
                }
            }
        }
    }
}
