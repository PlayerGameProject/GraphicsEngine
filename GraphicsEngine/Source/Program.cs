using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
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
                ClientSize = new Vector2i(640, 480),
                Title = "Graphics Engine",
                API = ContextAPI.OpenGL,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Core,
                Vsync = VSyncMode.Adaptive,
                WindowState = WindowState.Normal,
                WindowBorder = WindowBorder.Resizable,
                StartFocused = true,
                StartVisible = false,
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
