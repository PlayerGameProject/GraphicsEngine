using System.Reflection;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicsEngine.Source
{
    public class Engine : GameWindow
    {
	    private Scene _scene;
        
        public Engine(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)  : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            Console.WriteLine(" -- Graphics Engine process has started! -- " + "\n");

            _scene = new Scene(Size.X / (float)Size.Y);
            CursorState = CursorState.Grabbed;
            
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            
            _scene.Render(args);
            
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            Console.WriteLine($"[Debug] FPS: {(1f / args.Time).ToString("0.") + "\0"}");
            
            if (!IsFocused) return;

            KeyboardState keyInput = KeyboardState;
            MouseState mouseInput = MouseState;
            _scene.Update(args, keyInput, mouseInput);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs args)
        {
            base.OnMouseWheel(args);

            _scene.FOV(args);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs args)
        {
	        base.OnKeyDown(args);

	        if (KeyboardState.IsKeyDown(Keys.Escape)) Close(); // Window close
	        
	        if (KeyboardState.IsKeyPressed(Keys.F11)) // Window fullscreen
	        {
		        WindowState = WindowState switch
		        {
			        WindowState.Fullscreen => WindowState.Normal,
			        _ => WindowState.Fullscreen
		        };
	        }

	        if (KeyboardState.IsKeyPressed(Keys.F2)) // Window V-sync
	        {
		        if (VSync == VSyncMode.On)
		        {
			        VSync = VSyncMode.Adaptive;
			        Console.WriteLine("[Debug] VSync: Adaptive");
		        }

		        if (VSync == VSyncMode.On)
		        {
			        VSync = VSyncMode.Adaptive;
			        Console.WriteLine("[Debug] VSync: Adaptive");
		        }
		        else if (VSync == VSyncMode.Adaptive)
		        {
			        VSync = VSyncMode.Off;
			        Console.WriteLine("[Debug] VSync: Off");
		        }
		        else
		        {
			        VSync = VSyncMode.On;
			        Console.WriteLine("[Debug] VSync: On");
		        }
	        }

	        KeyboardState keyInput = KeyboardState;
	        _scene.KeyDown(keyInput);
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            
            GL.Viewport(0, 0, args.Width, args.Height);
            
            _scene.Resize(args, Size.X / (float)Size.Y, new Vector2(Size.X, Size.Y));
        }

        protected override void OnUnload()
        {
	        Console.WriteLine("\n" + " -- Graphics Engine process has ended! -- ");

	        _scene.Unload();
            base.OnUnload();
        }
    }
}
