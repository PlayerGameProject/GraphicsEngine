using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicsEngine.Source
{
    public class Engine : GameWindow
    {
        private readonly float[] _vertices = new float[]
        {
            // position          // color           // texture
            -0.5f,  0.5f, 0.0f,  1.0f, 0.0f, 0.0f,  0.0f, 1.0f,  // Top-left vertex
             0.5f,  0.5f, 0.0f,  1.0f, 1.0f, 0.0f,  1.0f, 1.0f,  // Top-right vertex
             0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f,  // Bottom-right vertex
            -0.5f, -0.5f, 0.0f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  // Bottom-left vertex
        };

        private readonly uint[] _indices = new uint[]
        {
            // first quad
            0, 1, 3,
            1, 2, 3
        };

        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _elementArrayObject;

        private Shader _shader;
        private Texture _texture;
        
        private static readonly string? Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string? _engineDirectory = Directory.GetParent(path: Directory.GetParent(path: Directory.GetParent(Location).FullName).FullName).FullName;
        
        public Engine(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)  : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            Console.WriteLine(" -- Graphics Engine process started! -- " + "\n");
            
            Color aqua = Color.Aqua;
            GL.ClearColor(aqua.R, aqua.G, aqua.B, aqua.A);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementArrayObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementArrayObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            
            _shader = new Shader(_engineDirectory + "/Source/Mesh.vert", _engineDirectory + "/Source/Mesh.frag");
            _shader.Activate();
            
            _texture = Texture.LoadFromFile(_engineDirectory + "/Resource/Texture/Texture.png");
            _texture.Activate(TextureUnit.Texture0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.BindVertexArray(_vertexArrayObject);
            
            _texture.Activate(TextureUnit.Texture0);
            _shader.Activate();
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            Console.WriteLine($"[Debug] FPS: {(1f / args.Time).ToString("0.") + "\0"}");
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs args)
        {
            base.OnKeyDown(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            if (KeyboardState.IsKeyDown(Keys.F11))
            {
                WindowState = WindowState switch
                {
                    WindowState.Fullscreen => WindowState.Normal,
                    _ => WindowState.Fullscreen
                };
            }
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs args)
        {
            base.OnFramebufferResize(args);
            
            GL.Viewport(0, 0, args.Width, args.Height);
        }

        protected override void OnUnload()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            _shader.Deactivate();
            
            _texture.Delete();
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementArrayObject);
            _shader.Delete();
            
            Console.WriteLine("\n" + " -- Graphics Engine process ended! -- ");

            base.OnUnload();
        }
    }
}
