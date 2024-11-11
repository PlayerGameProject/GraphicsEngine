using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GraphicsEngine.Source
{
    public class Shader
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocations;
        
        public Shader(string vertexShaderPath, string fragmentShaderPath)
        {
            var vertexShaderSource = File.ReadAllText(vertexShaderPath);
            var fragmentShaderSource = File.ReadAllText(fragmentShaderPath);
            
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            CompileShader(vertexShader);
            
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            CompileShader(fragmentShader);

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            LinkProgram(Handle);
            
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            _uniformLocations = new Dictionary<string, int>();

            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);
                
                _uniformLocations.Add(key, location);
            }
        }

        public void Activate()
        {
            GL.UseProgram(Handle);
        }

        public void Deactivate()
        {
            GL.UseProgram(0);
        }

        public void Delete()
        {
            GL.DeleteProgram(Handle);
        }
        
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }
        
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }
        
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }
        
        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);
            
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occured whilst compiling {shader} shader: {infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error occurred whilst linking Program: {infoLog}");
            }
        }
    }
}

