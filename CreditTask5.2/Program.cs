using System;
using SplashKitSDK;


namespace ShapeDrawer
{

    public class Program
    {
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line
        }
        public static void Main()
        {
            ShapeKind kindToAdd = ShapeKind.Rectangle;
            Point2D lineStart = new Point2D(){X = 0, Y = 0 };
            Point2D lineEnd;
            new Window("Shape Drawer", 800, 600);
            Drawing drawObject = new Drawing();
            do
            {
                SplashKit.ProcessEvents(); //Check user inputs - should only call once
                SplashKit.ClearScreen(); //Clear screen to white
                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;
                }
                else if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }else if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                    lineStart.X = SplashKit.MouseX();
                    lineStart.Y = SplashKit.MouseY();
                }
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape;


                    if (kindToAdd == ShapeKind.Circle)
                    {
                        MyCircle newCircle = new MyCircle();
                        newCircle.X = SplashKit.MouseX();
                        newCircle.Y = SplashKit.MouseY();
                        newShape = newCircle;
                    }
                    else if (kindToAdd == ShapeKind.Rectangle)
                    {
                        MyRectangle newRectangle = new MyRectangle();
                        newRectangle.X = SplashKit.MouseX();
                        newRectangle.Y = SplashKit.MouseY();
                        newShape = newRectangle;
                    }
                    else
                    {
                        lineEnd.X = SplashKit.MouseX();
                        lineEnd.Y = SplashKit.MouseY();
                        Line tempLine;
                        tempLine.StartPoint = lineStart;
                        tempLine.EndPoint = lineEnd;
                        MyLine newLine = new MyLine(tempLine);
                        newShape = newLine;
                    }

                    //No good way to instantiate a shape with different params directly in method?
                    //Shape constructor needs ability to accept X/Y paramters, I guess?
                    //newShape.X = SplashKit.MouseX();
                    //newShape.Y = SplashKit.MouseY();
                    drawObject.AddShape(newShape);

                }
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    drawObject.Background = SplashKit.RandomRGBColor(255); //Alpha set to 255
                }
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    drawObject.SelectSchapesAt(SplashKit.MousePosition());
                }
                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    foreach (Shape s in drawObject.SelectedShapes)
                        drawObject.RemoveShape(s);
                }
                drawObject.Draw();
                SplashKit.RefreshScreen(); //Target FPS? Should have an uint argument?
            } while (!SplashKit.WindowCloseRequested("Shape Drawer"));
        }
    }
}