using System.Drawing;
using System.Reflection;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicsEngine.Source
{
    public class Engine : GameWindow
    {
        private readonly float[] _vertices = new float[]
        {
            // Position           // Color           // Texture
	        -1.0f,  1.0f,  1.0f,  0.0f, 0.0f, 1.0f,  0.0f, 1.0f,  // Front Face Top Left
	         1.0f,  1.0f,  1.0f,  1.0f, 1.0f, 1.0f,  1.0f, 1.0f,  // Front Face Top Right
	         1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 0.0f,  1.0f, 0.0f,  // Front Face Bottom Right
	        -1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 1.0f,  0.0f, 0.0f,  // Front Face Bottom Left
	         			   
	        -1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 1.0f,  0.0f, 1.0f,  // Right Face Top Left
	        -1.0f,  1.0f,  1.0f,  0.0f, 0.0f, 1.0f,  1.0f, 1.0f,  // Right Face Top Right
	        -1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 1.0f,  1.0f, 0.0f,  // Right Face Bottom Right
	        -1.0f, -1.0f, -1.0f,  0.0f, 0.0f, 0.0f,  0.0f, 0.0f,  // Right Face Bottom Left
	         			   
	         1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 0.0f,  0.0f, 1.0f,  // Back Face Top Left
	        -1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 1.0f,  1.0f, 1.0f,  // Back Face Top Right
	        -1.0f, -1.0f, -1.0f,  0.0f, 0.0f, 0.0f,  1.0f, 0.0f,  // Back Face Bottom Right
	         1.0f, -1.0f, -1.0f,  1.0f, 1.0f, 0.0f,  0.0f, 0.0f,  // Back Face Bottom Left
	         			   
	         1.0f,  1.0f,  1.0f,  1.0f, 1.0f, 1.0f,  0.0f, 1.0f,  // Left Face Top Left
	         1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 0.0f,  1.0f, 1.0f,  // Left Face Top Right
	         1.0f, -1.0f, -1.0f,  1.0f, 1.0f, 0.0f,  1.0f, 0.0f,  // Left Face Bottom Right
	         1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 0.0f,  0.0f, 0.0f,  // Left Face Bottom Left
	         			   
	        -1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 1.0f,  0.0f, 1.0f,  // Top Face Top Left
	         1.0f,  1.0f, -1.0f,  1.0f, 0.0f, 0.0f,  1.0f, 1.0f,  // Top Face Top Right
	         1.0f,  1.0f,  1.0f,  1.0f, 1.0f, 1.0f,  1.0f, 0.0f,  // Top Face Bottom Right
	        -1.0f,  1.0f,  1.0f,  0.0f, 0.0f, 1.0f,  0.0f, 0.0f,  // Top Face Bottom Left
	         			   
	        -1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 1.0f,  0.0f, 1.0f,  // Bottom Face top Left
	         1.0f, -1.0f,  1.0f,  0.0f, 1.0f, 0.0f,  1.0f, 1.0f,  // Bottom Face top Right
	         1.0f, -1.0f, -1.0f,  1.0f, 1.0f, 0.0f,  1.0f, 0.0f,  // Bottom Face Bottom Right
	        -1.0f, -1.0f, -1.0f,  0.0f, 0.0f, 0.0f,  0.0f, 0.0f   // Bottom Face Bottom Left
        };

        private readonly uint[] _indices = new uint[]
        {
            // Front Face
	         0,  1,  3,
	         1,  2,  3,

	        // Right Face
	         4,  5,  7,
	         5,  6,  7,

	        // Back Face
	         8,  9, 11,
	         9, 10, 11,
            
	        // Left Face
	        12, 13, 15,
	        13, 14, 15,
            
	        // Top Face
	        16, 17, 19,
	        17, 18, 19,
            
	        // Bottom Face
	        20, 21, 23,
	        21, 22, 23
        };

        private readonly string[] _texturePath = new string[]
        {
	        EngineDirectory + "/Resource/Texture/Grass Texture Side.png",
	        EngineDirectory + "/Resource/Texture/Grass Texture Side.png",
	        EngineDirectory + "/Resource/Texture/Grass Texture Side.png",
	        EngineDirectory + "/Resource/Texture/Grass Texture Side.png",
	        EngineDirectory + "/Resource/Texture/Grass Texture Top.png",
	        EngineDirectory + "/Resource/Texture/Grass Texture Bottom.png"
        };

        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _elementArrayObject;

        private Shader _shader;
        private Texture _texture;
        private Camera _camera;

        private bool _firstMove = true;
        private Vector2 _lastPos;
        private double _time;
        
        private static readonly string? Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string? EngineDirectory = Directory.GetParent(path: Directory.GetParent(path: Directory.GetParent(Location).FullName).FullName).FullName;
        
        public Engine(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)  : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            Console.WriteLine(" -- Graphics Engine process started! -- " + "\n");
            
            Color aqua = Color.Aqua;
            GL.ClearColor(aqua.R, aqua.G, aqua.B, aqua.A);
            
            GL.Enable(EnableCap.DepthTest);

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
            
            _shader = new Shader(EngineDirectory + "/Source/Mesh.vert", EngineDirectory + "/Source/Mesh.frag");
            _shader.Activate();
            
            _texture = Texture.LoadFromFile(EngineDirectory + "/Resource/Texture/Texture.png");
            _texture.Activate(TextureUnit.Texture0);

            _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            CursorState = CursorState.Grabbed;
            
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Back);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            _time += 4.0 * args.Time;
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.BindVertexArray(_vertexArrayObject);
            
            _texture.Activate(TextureUnit.Texture0);
            _shader.Activate();

            var model = Matrix4.Identity;
            _shader.SetMatrix4("Model", model);
            _shader.SetMatrix4("View", _camera.GetViewMatrix());
            _shader.SetMatrix4("Projection", _camera.GetProjectionMatrix());
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			GL.DrawElementsInstanced(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0, 1);
            
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            
            Console.WriteLine($"[Debug] FPS: {(1f / args.Time).ToString("0.") + "\0"}");
            
            if (!IsFocused) return;

            var input = KeyboardState;
            float cameraSpeed = 3.75f * ((input.IsKeyDown(Keys.LeftShift)) ? 2.5f : 1);
            const float sensitivity = 0.175f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }

            var mouse = MouseState;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs args)
        {
            base.OnMouseWheel(args);

            _camera.FOV -= args.OffsetY;
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

            _camera.AspectRatio = Size.X / (float)Size.Y;
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
