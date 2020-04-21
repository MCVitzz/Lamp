#version 330 core

out vec4 o_Colour;

uniform vec4 colour;

void main() {
	o_Colour = colour;
}
