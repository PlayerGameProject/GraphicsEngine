using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace GraphicsEngine.Source
{
    public class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings
            {
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Core,
                ClientSize = new Vector2i(640, 480),
                Vsync = VSyncMode.Adaptive,
                Title = "Graphics Engine",
                WindowState = WindowState.Normal,
                WindowBorder = WindowBorder.Resizable,
                Flags = ContextFlags.ForwardCompatible,
            };

            using (Engine engine = new Engine(GameWindowSettings.Default, nativeWindowSettings))
            {
                try
                {
                    engine.Run();
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"[Error] {exception.Message}\n" +
                                      $"[Error] Stack Trace:{exception.StackTrace}\n");
                }
            }
        }
    }
}
