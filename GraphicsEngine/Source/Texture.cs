using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;

namespace GraphicsEngine.Source
{
    public class Texture
    {
        public readonly int Handle;
        public static int[] Handles;
        public TextureTarget Type { get; private set; }

        public static Texture LoadFromFile(string path)
        {
            int handle = GL.GenTexture();
            
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);
            
            StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            
            return new Texture(handle);
        }
        
        public uint[] LoadFromFile(string[] path, int textureCount)
        {
            uint[] handles = new uint[textureCount];
            uint[] textures = new uint[textureCount];
            
            GL.GenTextures(textureCount, handles);

            for (int i = 0; i < textureCount; i++)
            {
                StbImage.stbi_set_flip_vertically_on_load(1);

                using (Stream stream = File.OpenRead(path[i]))
                {
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, Handles[i]);
                    
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                        (int)TextureMinFilter.Nearest);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                        (int)TextureMinFilter.Nearest);

                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                        (int)TextureWrapMode.Repeat);
                    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                        (int)TextureWrapMode.Repeat);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
                        PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                    GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                }

                GL.BindTexture(TextureTarget.Texture2D, 0);
            }

            return handles;
        }

        public Texture(int handle)
        {
            Handle = handle;
        }

        public Texture(int[] handles)
        {
            Handles = handles;
        }

        public void Activate(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        } 
        
        public void Delete()
        {
            GL.DeleteTexture(Handle);
        }
    }
}