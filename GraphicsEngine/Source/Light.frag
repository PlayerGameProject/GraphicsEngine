#version 330 core

out vec4 FragColor;

in vec3 Color;

uniform vec3 LightColor;

void main()
{
    FragColor = vec4(Color * LightColor, 1.0f);
}
