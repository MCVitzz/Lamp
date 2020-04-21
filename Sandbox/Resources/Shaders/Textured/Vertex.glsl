#version 330 core

layout(location = 0) in vec3 position;
layout(location = 1) in vec2 textureCoords;
layout(location = 2) in vec3 normal;

out vec2 p_TextureCoordinates;
out vec3 p_Normal;
out vec3 p_FragmentPosition;

uniform mat4 modelMatrix;
uniform mat4 viewMatrix;
uniform mat4 projectionMatrix;

void main() {
	gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(position, 1.0);
	p_TextureCoordinates = textureCoords;
	p_Normal = normal;
	p_FragmentPosition = vec3(modelMatrix * vec4(position, 1.0));
}
