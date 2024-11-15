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
        public int Handle;
        public static TextureTarget Type;

        public static int[] Handles;

        public void LoadFile(string path, TextureTarget textureType, TextureUnit unit)
        {
            Handle = GL.GenTexture();
            Type = textureType;

            GL.ActiveTexture(unit);
            GL.BindTexture(textureType, Handle);

            StbImage.stbi_set_flip_vertically_on_load(1);

            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                GL.TexImage2D(textureType, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public static int[] LoadFile(string[] path, int textureCount, TextureTarget textureType, TextureUnit unit)
        {
            Handles = new int[textureCount];
            Type = textureType;
            GL.GenTextures(textureCount, Handles);

            for (int i = 0; i < textureCount; i++)
            {
                GL.ActiveTexture(unit);
                GL.BindTexture(textureType, Handles[i]);
                
                StbImage.stbi_set_flip_vertically_on_load(1);

                using (Stream stream = File.OpenRead(path[i]))
                {
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    GL.TexImage2D(textureType, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                }

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.NearestMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            return Handles;
        }

        public void TextureUnit(Shader shader, string uniform, uint unit)
        {
            int textureUniform = GL.GetUniformLocation(shader.Handle, uniform);
            shader.Activate();
            GL.Uniform1(textureUniform, unit);
        }

        public void Bind()
        {
            GL.BindTexture(Type, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(Type, 0);
        }

        public void Delete()
        {
            GL.DeleteTexture(Handle);
        }
    }
}