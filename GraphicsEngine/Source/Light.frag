#version 330 core

out vec4 FragColor;

uniform vec3 LightColor;

void main(void)
{
    FragColor = vec4(LightColor, 1.0f);
}
