#version 330 core

layout (location = 0) in vec3 position;

uniform mat4 Model;
uniform mat4 View;
uniform mat4 Projection;

void main(void)
{
    gl_Position = vec4(position, 1.0f) * Model * View * Projection;
}
