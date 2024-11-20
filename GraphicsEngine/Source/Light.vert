// This file was directly copied from Mesh.vert, so don't be surprised when you see texture stuffs (Do this because bugs and errors)
#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec2 texture;

out vec3 Color;
out vec2 TextureCoordinate;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

void main()
{
    gl_Position = vec4(position, 1.0f) * Model * View * Projection;
    Color = color;
    TextureCoordinate = texture;
}
