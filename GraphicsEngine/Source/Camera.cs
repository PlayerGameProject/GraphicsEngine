using System;
using OpenTK.Mathematics;

namespace GraphicsEngine.Source
{
    public class Camera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;

        public Vector2 Size;
        
        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }
        
        private float _fov = MathHelper.PiOver2;

        public Vector3 Position;

        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);

                UpdateVectors();
            }
        }

        public float FOV
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 179f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        public Matrix4 GetPerspectiveProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.001f, 1000f);
        }

        public Matrix4 GetOrthographicProjectionMatrix() // For some reason, it doesn't render anything when using this function
        {
            var aspectRatio = Size.X / (float)Size.Y;
            
            return Matrix4.CreateOrthographic(Size.X * aspectRatio, Size.Y * aspectRatio, 0.001f, 1000f);
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}
