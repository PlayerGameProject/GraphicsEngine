#version 460 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec2 texture;

out vec3 Color;
out vec2 TextureCoordinate;

void main() 
{
    gl_Position = vec4(position, 1.0f);
    Color = color;
    TextureCoordinate = texture;
}
