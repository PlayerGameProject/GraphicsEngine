#version 330 core

out vec4 FragColor;

in vec3 Color;
in vec2 TextureCoordinate;

uniform sampler2D Texture;

void main() 
{
    FragColor = texture(Texture, TextureCoordinate); // Texture rendering
    //FragColor = vec4(Color, 1.0f); // Color rendering
}
