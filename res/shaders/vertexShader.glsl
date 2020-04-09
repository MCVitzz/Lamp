#version 400 core

layout(location = 0) in vec3 position;

out vec3 o_position;

void main() {
    gl_Position = vec4(position, 1.0);
    o_position = position;
}