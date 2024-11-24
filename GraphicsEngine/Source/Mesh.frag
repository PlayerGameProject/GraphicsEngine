#version 330 core

out vec4 FragColor;

in vec3 Color;
in vec2 TextureCoordinate;

uniform sampler2D Texture;
uniform vec3 LightColor;

void main(void) 
{
    FragColor = texture(Texture, TextureCoordinate) * vec4(LightColor, 1.0f); // Texture rendering
    //FragColor = vec4(Color * LightColor, 1.0f); // Color rendering
}
