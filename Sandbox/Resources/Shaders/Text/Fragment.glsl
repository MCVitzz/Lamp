#version 330 core

in vec2 o_TextureCoords;

out vec4 o_Colour;

uniform sampler2D texturino;

void main() {
    vec4 vertexColour = texture(texturino, o_TextureCoords);
	o_Colour = vertexColour;
}