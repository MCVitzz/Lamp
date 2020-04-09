#version 330 core

in vec3 o_position;

layout(location = 0) out vec4 colour;

void main() {
    colour = vec4(o_position * 0.5 + 0.75, 1.0);
}