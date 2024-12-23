using System.Reflection;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicsEngine.Source
{
    public class Scene
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
        
        private readonly float[] _lightVertices = new float[]
        {
            // Position
            -1.0f,  1.0f,  1.0f,  // Front Face Top Left
             1.0f,  1.0f,  1.0f,  // Front Face Top Right
             1.0f, -1.0f,  1.0f,  // Front Face Bottom Right
            -1.0f, -1.0f,  1.0f,  // Front Face Bottom Left
             			   
            -1.0f,  1.0f, -1.0f,  // Right Face Top Left
            -1.0f,  1.0f,  1.0f,  // Right Face Top Right
            -1.0f, -1.0f,  1.0f,  // Right Face Bottom Right
            -1.0f, -1.0f, -1.0f,  // Right Face Bottom Left
								  
             1.0f,  1.0f, -1.0f,  // Back Face Top Left
            -1.0f,  1.0f, -1.0f,  // Back Face Top Right
            -1.0f, -1.0f, -1.0f,  // Back Face Bottom Right
             1.0f, -1.0f, -1.0f,  // Back Face Bottom Left
								  
             1.0f,  1.0f,  1.0f,  // Left Face Top Left
             1.0f,  1.0f, -1.0f,  // Left Face Top Right
             1.0f, -1.0f, -1.0f,  // Left Face Bottom Right
             1.0f, -1.0f,  1.0f,  // Left Face Bottom Left
								  
            -1.0f,  1.0f, -1.0f,  // Top Face Top Left
             1.0f,  1.0f, -1.0f,  // Top Face Top Right
             1.0f,  1.0f,  1.0f,  // Top Face Bottom Right
            -1.0f,  1.0f,  1.0f,  // Top Face Bottom Left
								  
            -1.0f, -1.0f,  1.0f,  // Bottom Face top Left
             1.0f, -1.0f,  1.0f,  // Bottom Face top Right
             1.0f, -1.0f, -1.0f,  // Bottom Face Bottom Right
            -1.0f, -1.0f, -1.0f   // Bottom Face Bottom Left
        };
        
        private readonly uint[] _lightIndices = new uint[]
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
        
        // Model
        private Vector3 _meshPosition = new Vector3(0f, 0f, 0f);
        
        private int _vao;
        private int _vbo;
        private int _ebo;

        private Shader _shader;
        private Texture _texture;
        private int[] _textures;
        private Camera _camera;
        
        // Light
        private Vector3 _lightColor = new Vector3(1f, 1f, 1f);
        private readonly Vector3 _lightPosition = new Vector3(2.5f, 2.5f, 2.5f);
        
        private int _vaoLight;
        private int _vboLight;
        private int _eboLight;

        private Shader _lightShader;
        
        // Camera
        private bool _moved = true;
        private Vector2 _lastPos;
        private double _time;
        
        // Utility
        private bool _wireframe = true;
        
        private static readonly string? Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string? EngineDirectory = Directory.GetParent(path: Directory.GetParent(path: Directory.GetParent(Location).FullName).FullName).FullName;
        
        public Scene(float aspectRatio)
        {
	        Color4 aqua = Color4.Aqua;
	        GL.ClearColor(aqua.R, aqua.G, aqua.B, aqua.A);
            
	        GL.Enable(EnableCap.DepthTest);
	        
            // Model
            {
	            _shader = new Shader(EngineDirectory + "/Source/Mesh.vert", EngineDirectory + "/Source/Mesh.frag");
	            
	            _vao = GL.GenVertexArray();
	            GL.BindVertexArray(_vao);

	            _vbo = GL.GenBuffer();
	            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
	            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices,
		            BufferUsageHint.StaticDraw);

	            _ebo = GL.GenBuffer();
	            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
	            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices,
		            BufferUsageHint.StaticDraw);

	            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
	            GL.EnableVertexAttribArray(0);
	            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float),
		            3 * sizeof(float));
	            GL.EnableVertexAttribArray(1);
	            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float),
		            6 * sizeof(float));
	            GL.EnableVertexAttribArray(2);

	            _texture = new Texture();
	            _textures = Texture.LoadFile(_texturePath, _texturePath.Length, TextureTarget.Texture2D,
		            TextureUnit.Texture0);
	            _texture.TextureUnit(_shader, "Texture", 0);
            }

            // Light
            {
	            _lightShader = new Shader(EngineDirectory + "/Source/Light.vert", EngineDirectory + "/Source/Light.frag");
	            
	            _vaoLight = GL.GenVertexArray();
	            GL.BindVertexArray(_vaoLight);
	            
	            _vboLight = GL.GenBuffer();
	            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboLight);
	            GL.BufferData(BufferTarget.ArrayBuffer, _lightVertices.Length * sizeof(float), _lightVertices, BufferUsageHint.StaticDraw);

	            _eboLight = GL.GenBuffer();
	            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboLight);
	            GL.BufferData(BufferTarget.ElementArrayBuffer, _lightIndices.Length * sizeof(uint), _lightIndices, BufferUsageHint.StaticDraw);
	            
	            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
	            GL.EnableVertexAttribArray(0);
            }

            _camera = new Camera(Vector3.UnitZ * 4, aspectRatio);
            
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Back);
        }

        public void Render(FrameEventArgs args)
        {
	        _time += 4.0 * args.Time;
	        
	        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

	        GL.BindVertexArray(_vao);
	        _texture.Bind();
	        _shader.Activate();

	        _lightColor = new Vector3(1f, 1f, 1f);

	        var model = Matrix4.Identity * Matrix4.CreateTranslation(_meshPosition) * Matrix4.CreateScale(1f);
	        _shader.SetMatrix4("Model", model);
	        _shader.SetMatrix4("View", _camera.GetViewMatrix());
	        _shader.SetMatrix4("Projection", _camera.GetPerspectiveProjectionMatrix());
            
	        _shader.SetVector3("LightColor", _lightColor);

	        for (int i = 0; i < _texturePath.Length; i++)
	        {
		        GL.BindTexture(TextureTarget.Texture2D, _textures[i]);
		        GL.DrawElementsInstanced(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, i * 6 * sizeof(uint), 1);
	        }

	        GL.BindVertexArray(_vaoLight);
	        _lightShader.Activate();

	        var lightMatrix = Matrix4.CreateScale(0.25f) * Matrix4.CreateTranslation(_lightPosition);
            
	        _lightShader.SetMatrix4("Model", lightMatrix);
	        _lightShader.SetMatrix4("View", _camera.GetViewMatrix());
	        _lightShader.SetMatrix4("Projection", _camera.GetPerspectiveProjectionMatrix());
            
	        _lightShader.SetVector3("LightColor", _lightColor);
            
	        GL.DrawElementsInstanced(PrimitiveType.Triangles, _lightIndices.Length, DrawElementsType.UnsignedInt, 0, 1);
        }

        public void Update(FrameEventArgs args, KeyboardState keyState, MouseState mouseState)
        {
	        float cameraSpeed = 5f * (keyState.IsKeyDown(Keys.LeftShift) ? 2.5f : 1);
	        const float sensitivity = 0.15f;

	        if (keyState.IsKeyDown(Keys.W))
	        {
		        _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
	        }
	        if (keyState.IsKeyDown(Keys.S))
	        {
		        _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
	        }
	        if (keyState.IsKeyDown(Keys.A))
	        {
		        _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
	        }
	        if (keyState.IsKeyDown(Keys.D))
	        {
		        _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
	        }
	        if (keyState.IsKeyDown(Keys.Space))
	        {
		        _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
	        }
	        if (keyState.IsKeyDown(Keys.LeftControl))
	        {
		        _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
	        }

	        if (_moved)
	        {
		        _lastPos = new Vector2(mouseState.X, mouseState.Y);
		        _moved = false;
	        }
	        else
	        {
		        var deltaX = mouseState.X - _lastPos.X;
		        var deltaY = mouseState.Y - _lastPos.Y;
		        _lastPos = new Vector2(mouseState.X, mouseState.Y);
                
		        _camera.Yaw += deltaX * sensitivity;
		        _camera.Pitch -= deltaY * sensitivity;
	        }
        }

        public void KeyDown(KeyboardState keyState)
        {
	        if (keyState.IsKeyPressed(Keys.F3)) // Debug mode (Wireframe)
	        {
		        if (_wireframe)
		        {
			        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
			        Console.WriteLine($"[Debug] Wireframe rendering: Enabled");
			        _wireframe = false;
		        }
		        else
		        {
			        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			        Console.WriteLine($"[Debug] Wireframe rendering: Disabled");
			        _wireframe = true;
		        }
	        }
        }

        public void FOV(MouseWheelEventArgs args)
        {
	        _camera.FOV -= args.OffsetY;
        }

        public void Resize(FramebufferResizeEventArgs args, Vector2 size)
        {
	        GL.Viewport(0, 0, args.Width, args.Height);

	        _camera.AspectRatio = size.X / (float)size.Y;
	        _camera.Size = size;
        }

        public void Unload()
        {
	        GL.BindVertexArray(0);
	        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
	        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
	        _lightShader.Deactivate();
	        _shader.Deactivate();
            
	        GL.DeleteVertexArray(_vaoLight);
	        GL.DeleteBuffer(_vboLight);
	        GL.DeleteBuffer(_eboLight);
	        _lightShader.Delete();
            
	        _texture.Delete();
	        GL.DeleteVertexArray(_vao);
	        GL.DeleteBuffer(_vbo);
	        GL.DeleteBuffer(_ebo);
	        _shader.Delete();
        }
    }
}

