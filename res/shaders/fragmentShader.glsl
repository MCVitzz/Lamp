#version 400 core

layout(location = 0) out vec4 colour;

in vec3 o_position;

void main() {
    colour = vec4(o_position, 1.0);
}