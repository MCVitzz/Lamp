#version 330 core

layout(location = 0) in vec2 position;

uniform vec4 transform;

void main() {
    vec2 screenPosition = position * transform.zw + transform.xy;
	screenPosition.x = screenPosition.x * 2.0 - 1.0;
	screenPosition.y = screenPosition.y * -2.0 + 1.0;
    gl_Position = vec4(screenPosition, 0.0, 1.0);
}